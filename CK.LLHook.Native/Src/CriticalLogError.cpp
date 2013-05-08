#include "stdafx.h"
#include "BasicLog.h"
#include <CK.String.h>

void WINAPI DoWriteError( LPCWSTR errorMessage )
{
	BasicLogWrite( errorMessage );
}

LPTSTR WINAPI FormatAllocatedSystemLastError()
{
	LPTSTR lastErrorText = NULL;
	FormatMessage(
					// use system message tables to retrieve error text and allocate buffer on local heap for error text.
					FORMAT_MESSAGE_FROM_SYSTEM|FORMAT_MESSAGE_ALLOCATE_BUFFER
					// Important! will fail otherwise, since we're not (and CANNOT) pass insertion parameters.
					|FORMAT_MESSAGE_IGNORE_INSERTS,  
					NULL,    // unused with FORMAT_MESSAGE_FROM_SYSTEM
					GetLastError(),
					MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
					(LPTSTR)&lastErrorText, 
					0, // minimum size for output buffer
					NULL );   // No arguments.
	return lastErrorText;
}


void WINAPI CriticalLogError( LPCWSTR errorMessage )
{
	if( errorMessage != NULL ) DoWriteError( errorMessage );
}

void WINAPI CriticalLogError( LPCWSTR prefix, LPCWSTR errorMessage )
{
	if( prefix != NULL )
	{
		if( errorMessage != NULL )
		{
			// Use 720 here to avoid the "unresolved external symbol __chkstk"
			// This (small) size on the stack does not trigger the insertion of __chkstk
			// function prologue.
			wchar_t fullMessage[720];
			CK::String::UnsafeCopy( fullMessage, prefix );
			CK::String::UnsafeConcat( fullMessage, errorMessage );
			DoWriteError( fullMessage );
		}
		else DoWriteError( prefix );
	}
	else if( errorMessage != NULL ) DoWriteError( errorMessage );
}

void WINAPI CriticalLogErrorWithLastError( LPCWSTR errorMessage )
{
	LPTSTR lastErrorText = FormatAllocatedSystemLastError();
	CriticalLogError( errorMessage );
	if( lastErrorText != NULL )
	{
		DoWriteError( lastErrorText );
		LocalFree( lastErrorText );
	}
}

void WINAPI CriticalLogErrorWithLastError( LPCWSTR prefix, LPCWSTR errorMessage )
{
	LPTSTR lastErrorText = FormatAllocatedSystemLastError();
	CriticalLogError( prefix, errorMessage );
	if( lastErrorText != NULL )
	{
		DoWriteError( lastErrorText );
		LocalFree( lastErrorText );
	}
}


