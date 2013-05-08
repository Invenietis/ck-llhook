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
