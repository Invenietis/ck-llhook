#include "stdafx.h"

void SendStartStop( HWND w, BOOL start )
{
	PostMessage( w, WM_APP, start, sizeof(size_t) );
}

