/*----------------------------------------------------------------------------
* This file (CK.LLHook.Native\Src\Impl\ShellHook.cpp) is part of CiviKey. 
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

#include "../stdafx.h"
#include "../SharedHooks.h"
#include "../CriticalLogError.h"

HookConfig _shellConfig = {};

static LRESULT CALLBACK Callback( int code, WPARAM wparam, LPARAM lparam )
{
	if( code >= 0 ) 
	{
		if( GetHookConfig( CKShellHookIdx, _shellConfig ) )
		{
			switch( code )
			{
				case HSHELL_RUDEAPPACTIVATED:
				case HSHELL_WINDOWACTIVATED: 
					{
						PostMessage( _shellConfig.Target, WM_APP+1, wparam, 0 );
					}
			}
		}
	}
	return CallNextHookEx( _shellConfig.HookId, code, wparam, lparam );
}

HWND WINAPI GetShellHookTarget()
{	
	if( !GetHookConfig( CKShellHookIdx, _shellConfig ) ) return NULL;
	return _shellConfig.Target;
}

BOOL WINAPI ActivateShellHook( HWND targetWnd )
{	
	return ActivateSharedHook( CKShellHookIdx, targetWnd, (HOOKPROC)Callback );
}

BOOL WINAPI SetShellHookOptions( ShellHookOption options )
{
	return TRUE;
}
