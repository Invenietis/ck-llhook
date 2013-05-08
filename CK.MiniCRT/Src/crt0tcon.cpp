// crt0tcon.cpp

// based on:
// LIBCTINY - Matt Pietrek 2001
// MSDN Magazine, January 2001

// 08/12/06 (mv)

#include <windows.h>
#include "libct.h"
#include <stdio.h>

int ConsoleMain( int, WCHAR** );    // In user's code

extern "C" void __cdecl mainCRTStartup()
{
    int argc = _init_args();
    _init_atexit();
    _initterm(__xc_a, __xc_z);

    int ret = 0; //_tmain(argc, _argv, 0);

	ret = ConsoleMain( argc, _argv );

	_doexit();
	_term_args();
    ExitProcess(ret);
}