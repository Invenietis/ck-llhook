/*----------------------------------------------------------------------------
* This file (CK.LLHook.Native\Src\SharedHooks.h) is part of CiviKey. 
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

struct HookConfig
{
	volatile LONG VersionTag;
	volatile HHOOK HookId;
	volatile HWND Target;
};
typedef struct HookConfig HookConfig;

/*Number of hook types implemented.*/
#define CKHookCount			3

#define CKMouseHookIdx	0
#define CKKeyboardHookIdx 1
#define CKShellHookIdx		2

/*
Updates a process config from the shared one.
Returns FALSE if synchronization went wrong: the configuration should not be used and the 
hook shoould be considered deactivated.
*/
extern BOOL WINAPI GetHookConfig( int ckHookIdx, HookConfig& config );

/*
Updates the shared configuration for a hook.
If the targetWnd is null, the hook is disabled.
If the targetWnd is already associated to another hook, the previous hook is disabled.
*/
extern BOOL WINAPI ActivateSharedHook( int ckHookIdx, HWND targetWnd, HOOKPROC hookProc );
