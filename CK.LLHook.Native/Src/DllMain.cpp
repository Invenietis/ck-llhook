/*----------------------------------------------------------------------------
* This file (CK.LLHook.Native\Src\DllMain.cpp) is part of CiviKey. 
*  
* CiviKey is free software: you can redistribute it and/or modify 
* it under the terms of the GNU Lesser General Public License as published 
* by the Free Software Foundation, either version 3 of the License, or 
* (at your option) any later version. 
*  
* CiviKey is distributed in the hope that it will be useful, 
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
* GNU Lesser General Public License for more details. 
* You should have received a copy of the GNU Lesser General Public License 
* along with CiviKey.  If not, see <http://www.gnu.org/licenses/>. 
*  
* Copyright © 2007-2013, 
*     Invenietis <http://www.invenietis.com>,
* All rights reserved. 
*-----------------------------------------------------------------------------*/

#include "stdafx.h"
#include "SharedHooks.h"
#include "BasicLog.h"
#include "CriticalLogError.h"
#include "SendInfo.h"

// Store the application instance of this module to pass to
// hook initialization. This instance is per process (since the base address
// of a dll can change across processes).
HINSTANCE globalHInstanceModule = NULL;

// Named mutex to protect the access to Shared Section across processes and threads 
// in processes.
HANDLE SharedMutex;
//
// NOTE: 
// - all variables must be initialized in the declaration, or else they won't be shared.
// - Since we only store handles (HHOOK, HWND), counters and configuration but no pointers to any code here 
//	 the security risk is mitgated.
//   Worst case would be for us to call UnhookWindowsEx with an invalid handle,
//   to post a message to an invalid or unexpected window (this can be done from nearly any program), or to use
//   modified configuration...
//
#pragma data_seg(".shared")
	LONG SharedVersionTag[CKHookCount] = { 0, 0, 0 };
	HWND SharedTarget[CKHookCount] = { NULL, NULL, NULL };
	HHOOK SharedHook[CKHookCount] = { 0, 0, 0 };
	//char SharedSpecificConfig[0 + 0 + sizeof(ShellHookOption)] = { 0 }; // All elements are initialized to 0;
#pragma data_seg()

int HookTypes[CKHookCount] = { WH_MOUSE, WH_KEYBOARD, WH_SHELL };
LPCWSTR HookNames[CKHookCount] = { L"[WH_MOUSE]", L"[WH_KEYBOARD]", L"[WH_SHELL]" };

BOOL APIENTRY DllMain(HINSTANCE hinstDLL, DWORD ul_reason_for_call, LPVOID lpReserved)
{
	if( ul_reason_for_call == DLL_PROCESS_ATTACH )
	{
		// Captures the library instance of this module to pass to hook initialization.
		if( globalHInstanceModule == NULL ) globalHInstanceModule = hinstDLL;
		wchar_t mutexName[MAX_PATH];
		lstrcpy( mutexName, L"Local\\BAE66726BB124F8A8D4721A59063C4CF-" );
		lstrcat( mutexName, TARGETFILENAME );

		// SharedMutex has default security attributes and is not initially owned.
		SharedMutex = CreateMutex( NULL, FALSE, mutexName );     
		if( SharedMutex == NULL ) CriticalLogErrorWithLastError( L"Create Mutex failed:" );

		// Optimization: avoid entering this dll for each thread creation/destruction.
		// We can do it since we do not use statically linked CRT (http://support.microsoft.com/kb/555563/EN-US).
		// 
		// In debug, we dynamically link to MSVCRTD.dll (or MSVCR80D.dll) thanks to msvcrtd.lib, and, of course, 
		// the compiler configuration > C/C++ > Code Generation > Runtime library = Multi-threaded Debug DLL (/MDd).
		//
		// In release, the Linker option /NODEFAULTLIB removes this dependency: the release versions only depends on kernel32.lib and user32.lib.
		//
		if( !DisableThreadLibraryCalls( hinstDLL ) ) CriticalLogErrorWithLastError( L"DisableThreadLibraryCalls failed:" );
	}
	else if( ul_reason_for_call == DLL_PROCESS_DETACH )
	{
		if( !CloseHandle( SharedMutex ) ) CriticalLogErrorWithLastError( L"CloseHandle failed:" );
	}
	return TRUE;
}

BOOL WINAPI GetHookConfig( int ckHookIdx, HookConfig& config )
{
	if( config.VersionTag == SharedVersionTag[ckHookIdx] ) return TRUE;
	DWORD waitResult = WaitForSingleObject( SharedMutex, INFINITE ); 
    if( waitResult == WAIT_OBJECT_0 ) 
	{
		config.VersionTag = SharedVersionTag[ckHookIdx];
		config.HookId = SharedHook[ckHookIdx];
		config.Target = SharedTarget[ckHookIdx];
		if( ReleaseMutex( SharedMutex ) ) return TRUE;
		CriticalLogErrorWithLastError( HookNames[ckHookIdx], L"Read: SharedMutex release failed:" );
	}
	else if( waitResult == WAIT_ABANDONED ) CriticalLogError( HookNames[ckHookIdx], L"Read: abandonned." );
	else CriticalLogErrorWithLastError( HookNames[ckHookIdx], L"Read: SharedMutex acquire failed:" );
	return FALSE;
}

void WINAPI DeactivateAllHooks()
{
	DWORD waitResult = WaitForSingleObject( SharedMutex, INFINITE ); 
    if( waitResult == WAIT_OBJECT_0 ) 
	{
		for( int i = 0; i < CKHookCount; ++i )
		{
			if( SharedHook[i] != 0 )
			{
				InterlockedIncrement( &(SharedVersionTag[i]) );
				BOOL r = UnhookWindowsHookEx( SharedHook[i] );
				if( !r ) CriticalLogErrorWithLastError( HookNames[i], L"UnhookWindowsHookEx failed:" );
				SendStartStop( SharedTarget[i], FALSE );
				SharedHook[i] = 0;
				SharedTarget[i] = NULL;
			}
		}
	}
	else if( waitResult == WAIT_ABANDONED ) CriticalLogError( L"DeactivateAll: abandonned." );
	else CriticalLogErrorWithLastError( L"DeactivateAll: SharedMutex acquire failed:" );
}

BOOL WINAPI ActivateSharedHook( int ckHookIdx, HWND targetWnd, HOOKPROC hookProc )
{	
	BOOL ret = FALSE;
	DWORD waitResult = WaitForSingleObject( SharedMutex, INFINITE ); 
    if( waitResult == WAIT_OBJECT_0 ) 
	{
		// If the Hook is already active, returns true if the target is the same, false otherwise.
		HWND currentTarget = SharedTarget[ckHookIdx];
		// If the Hook is deactivated and we are asked to deactivate, or if we are asked to target the same window, returns true.
		if( targetWnd == currentTarget ) ret = TRUE;
		else 
		{
			// Blocks any readers to avoid a race condition: The hook may trigger before we 
			// updated the SharedConfiguration.
			InterlockedIncrement( &(SharedVersionTag[ckHookIdx]) );
			if( targetWnd != NULL )
			{
				// If a hook already exists but bound to another window, we
				// do not set a new hook: we simply must change the target.
				HHOOK h = SharedHook[ckHookIdx];
				if( h == NULL )
				{
					h = SetWindowsHookEx( HookTypes[ckHookIdx], hookProc, globalHInstanceModule, 0 );
					if( h == NULL ) CriticalLogErrorWithLastError( HookNames[ckHookIdx], L"SetWindowsHookEx failed:" );
				}
				if( h != NULL )
				{
					// If the hook is bound to another widow, we must send a Stop message to the previous target.
					if( currentTarget != NULL )
					{
						SendStartStop( currentTarget, FALSE );
						// We are now done with the current target.
					}
					// If another hook is bound to the new target window, we deactivate it.
					for( int i = 0; i < CKHookCount; ++i )
					{
						if( SharedTarget[i] == targetWnd )
						{
							// Even if UnhookWindowsHookEx failed (higly improbable), we clear it.
							// This means that a hook may continue to fire even if it sould not: this edge case 
							// has to be tested in the callback functions.
							InterlockedIncrement( &(SharedVersionTag[i]) );
							BOOL r = UnhookWindowsHookEx( SharedHook[i] );
							if( !r ) CriticalLogErrorWithLastError( HookNames[i], L"UnhookWindowsHookEx failed for previous hook:" );
							SendStartStop( SharedTarget[i], FALSE );
							SharedHook[i] = 0;
							SharedTarget[i] = NULL;
						}
					}
					SendStartStop( targetWnd, TRUE );
					SharedHook[ckHookIdx] = h;
					SharedTarget[ckHookIdx] = targetWnd;
					ret = TRUE;
				}
			}
			else
			{
				//assert( targetWnd == NULL && SharedHook[ckHookIdx] != 0 );
				ret = UnhookWindowsHookEx( SharedHook[ckHookIdx] );
				if( ret ) 
				{
					SendStartStop( SharedTarget[ckHookIdx], FALSE );
					SharedHook[ckHookIdx] = 0;
					SharedTarget[ckHookIdx] = NULL;
				}
				else CriticalLogErrorWithLastError( HookNames[ckHookIdx], L"UnhookWindowsHookEx failed:" );
			}
			// If the set or unset failed, by decrementing the count,
			// we can avoid useless resynchronizations for other processes.
			if( !ret ) InterlockedDecrement( &(SharedVersionTag[ckHookIdx]) );
		}
		if( !ReleaseMutex( SharedMutex ) ) CriticalLogErrorWithLastError( HookNames[ckHookIdx], L"Write SharedMutex release failed:" );
	}
	else if( waitResult == WAIT_ABANDONED ) CriticalLogError( HookNames[ckHookIdx], L"Write SharedMutex abandonned." );
	else CriticalLogErrorWithLastError( HookNames[ckHookIdx], L"Write SharedMutex acquire failed:" );
	return ret;
}


