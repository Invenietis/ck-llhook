/*----------------------------------------------------------------------------
* This file (CK.LLHook.Native\Src\Impl\KeyboardHook.cpp) is part of CiviKey. 
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

HookConfig _keyboardConfig = {};

static LRESULT CALLBACK Callback( int code, WPARAM wparam, LPARAM lparam )
{
	// Any code below 0 must be ignored (and CallNextHookEx must be called directly).
	// When code = HC_NOREMOVE (3) the message is NOT removed from the queue (PeekMessage function, 
	// specifying the PM_NOREMOVE flag). We just ignore this and handles only code = 0 (HC_ACTION).
	if( code == 0 ) 
	{
		if( GetHookConfig( CKKeyboardHookIdx, _keyboardConfig ) ) 
		{
			PostMessage( _keyboardConfig.Target, WM_APP+1, wparam, lparam );
		}
	}
	return CallNextHookEx( _keyboardConfig.HookId, code, wparam, lparam );
}

HWND WINAPI GetKeyboardHookTarget()
{	
	if( !GetHookConfig( CKKeyboardHookIdx, _keyboardConfig ) ) return NULL;
	return _keyboardConfig.Target;
}

BOOL WINAPI ActivateKeyboardHook( HWND targetWnd )
{	
	return ActivateSharedHook( CKKeyboardHookIdx, targetWnd, (HOOKPROC)Callback );
}
