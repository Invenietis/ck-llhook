/*----------------------------------------------------------------------------
* This file (CK.LLHook.Native\Src\HappyHooks.h) is part of CiviKey. 
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

#include <Windows.h>

/*
	Base structure of all hook options.
*/
typedef struct CommonHookOption
{
	BOOL RawIntercept;

} CommonHookOption;

extern void WINAPI GetBasicLogFileName( WCHAR* nameMaxPath );

extern void WINAPI DeactivateAllHooks();

extern HWND WINAPI GetKeyboardHookTarget();

extern BOOL WINAPI ActivateKeyboardHook( HWND targetWnd );

extern HWND WINAPI GetMouseHookTarget();

extern BOOL WINAPI ActivateMouseHook( HWND targetWnd );

extern HWND WINAPI GetShellHookTarget();

extern BOOL WINAPI ActivateShellHook( HWND targetWnd );

typedef struct ShellHookOption : public CommonHookOption
{
} ShellHookOption;

extern BOOL WINAPI SetShellHookOptions( ShellHookOption options );

