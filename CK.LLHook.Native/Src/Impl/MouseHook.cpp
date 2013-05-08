#include "../stdafx.h"
#include "../SharedHooks.h"
#include "../CriticalLogError.h"

HookConfig _mouseConfig = {};

// WM_APP + 1 = Down/Up/Double click
const int LeftButton = 1;
const int MiddleButton = 2;
const int RightButton = 3;
const int XButton1 = 4;
const int XButton2 = 5;

const int Down = 1<<16;
const int Up = 2<<16;
const int DblClick = 3<<16;

// WM_APP + 2 = Whell rotate


static LRESULT CALLBACK Callback(int code, WPARAM wparam, LPARAM lparam)
{
	// Any code below 0 must be ignored (and CallNextHookEx must be called directly).
	// When code = HC_NOREMOVE (3) the message is NOT removed from the queue (PeekMessage function, 
	// specifying the PM_NOREMOVE flag). We just ignore this and handles only code = 0 (HC_ACTION).
	if( code == 0 ) 
	{
		if( GetHookConfig( CKMouseHookIdx, _mouseConfig ) )
		{
			MOUSEHOOKSTRUCTEX* info = (MOUSEHOOKSTRUCTEX*)(void*)lparam;

			LPARAM commonLParam = info->wHitTestCode;

			if( wparam == WM_MOUSEWHEEL )
			{
				WORD w = HIWORD(info->mouseData);
				PostMessage( _mouseConfig.Target, WM_APP+2, w, commonLParam );
			}
			else if( wparam == WM_LBUTTONUP || wparam == WM_NCLBUTTONUP )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, LeftButton+Up, commonLParam );
			}
			else if( wparam == WM_LBUTTONDOWN || wparam == WM_NCLBUTTONDOWN )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, LeftButton+Down, commonLParam );
			}
			else if( wparam == WM_LBUTTONDBLCLK || wparam == WM_NCLBUTTONDBLCLK )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, LeftButton+DblClick, commonLParam );
			}
			else if( wparam == WM_RBUTTONUP || wparam == WM_NCRBUTTONUP )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, RightButton+Up, commonLParam );
			}
			else if( wparam == WM_RBUTTONDOWN || wparam == WM_NCRBUTTONDOWN )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, RightButton+Down, commonLParam );
			}
			else if( wparam == WM_RBUTTONDBLCLK || wparam == WM_NCRBUTTONDBLCLK )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, RightButton+DblClick, commonLParam );
			}
			else if( wparam == WM_MBUTTONUP || wparam == WM_NCMBUTTONUP )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, MiddleButton+Up, commonLParam );
			}
			else if( wparam == WM_MBUTTONDOWN || wparam == WM_NCMBUTTONDOWN )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, MiddleButton+Down, commonLParam );
			}
			else if( wparam == WM_MBUTTONDBLCLK || wparam == WM_NCMBUTTONDBLCLK )
			{
				PostMessage( _mouseConfig.Target, WM_APP+1, MiddleButton+DblClick, commonLParam );
			}
			else if( wparam == WM_XBUTTONUP || wparam == WM_NCXBUTTONUP )
			{
				WORD x = HIWORD(info->mouseData);
				if( x == XBUTTON1 ) PostMessage( _mouseConfig.Target, WM_APP+1, XButton1+Up, commonLParam );
				else if( x == XBUTTON2 ) PostMessage( _mouseConfig.Target, WM_APP+1, XButton2+Up, commonLParam );
			}
			else if( wparam == WM_XBUTTONDOWN || wparam == WM_NCXBUTTONDOWN )
			{
				WORD x = HIWORD(info->mouseData);
				if( x == XBUTTON1 ) PostMessage( _mouseConfig.Target, WM_APP+1, XButton1+Down, commonLParam );
				else if( x == XBUTTON2 ) PostMessage( _mouseConfig.Target, WM_APP+1, XButton2+Down, commonLParam );
			}
			else if( wparam == WM_XBUTTONDBLCLK || wparam == WM_NCXBUTTONDBLCLK )
			{
				WORD x = HIWORD(info->mouseData);
				if( x == XBUTTON1 ) PostMessage( _mouseConfig.Target, WM_APP+1, XButton1+DblClick, commonLParam );
				else if( x == XBUTTON2 ) PostMessage( _mouseConfig.Target, WM_APP+1, XButton2+DblClick, commonLParam );
			}
		}
	}
	return CallNextHookEx( _mouseConfig.HookId, code, wparam, lparam );
}

HWND WINAPI GetMouseHookTarget()
{	
	if( !GetHookConfig( CKMouseHookIdx, _mouseConfig ) ) return NULL;
	return _mouseConfig.Target;
}

BOOL WINAPI ActivateMouseHook( HWND targetWnd )
{	
	return ActivateSharedHook( CKMouseHookIdx, targetWnd, (HOOKPROC)Callback );
}

