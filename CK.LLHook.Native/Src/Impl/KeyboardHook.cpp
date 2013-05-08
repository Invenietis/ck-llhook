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
