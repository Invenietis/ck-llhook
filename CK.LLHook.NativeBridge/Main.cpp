// Main.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdlib.h>
#include <CK.Char.h>
#include <CK.Convert.h>
#include <CK.String.h>

class TokenBuffer
{
	private:
		static const int _lenBuf = 256;
		char _buf[_lenBuf];
		HANDLE _input;
		int _lineLen;
		int _nbToken;
		char* _curToken;

public:
	TokenBuffer( HANDLE input )
	{
		_input = input;
		_lineLen = -1;
	}

	bool ReadNextLine()
	{
		DWORD lenRead;
		ReadFile( _input, _buf, _lenBuf, &lenRead, NULL );
		_lineLen = (int)lenRead;
		_curToken = nullptr;
		_nbToken = 0;
		
		bool wasSpace = true;
		for( int i = 0; i < _lineLen; ++i ) 
		{
			if( CK::Char::IsSpace( _buf[i] ) ) 
			{
				if( _buf[i] == '\r' ) 
				{
					_buf[i] = 0;
					break;
				}
				_buf[i] = 0;
				if( !wasSpace ) 
				{
					++_nbToken;
					wasSpace = true;
				}
			}
			else 
			{
				if( _curToken == nullptr ) _curToken = _buf + i;
				wasSpace = false;
			}
		}
		return _curToken != nullptr;
	}

	int NbTokenLeft() { return _nbToken; }

	char* Current() { return _curToken; }

	char* MoveNext()
	{
		if( _nbToken == 0 ) _curToken = nullptr; 
		else
		{
			--_nbToken;
			while( *_curToken ) ++_curToken;
			while( *_curToken == 0 ) ++_curToken;
			return _curToken;
		}
		return _curToken;
	}

};

void SendStartStop( HWND w, BOOL start )
{
	PostMessage( w, WM_APP, start, sizeof(size_t) );
}


int ConsoleMain( int, WCHAR** )
{
	using namespace CK::Convert;
	using namespace CK::String;

	TokenBuffer in( GetStdHandle( STD_INPUT_HANDLE ) );
	while( in.ReadNextLine() )
	{
		if( CompareInvariant( in.Current(), "START" ) == 0 ) 
		{
			char* hook = in.MoveNext();
			char* hwnd = in.MoveNext();
			if( hook != nullptr && hwnd != nullptr )
			{
				HWND h =  (HWND)DecimalStringToInt( hwnd );
				if( h != NULL )
				{
					if( CompareInvariant( hook, "MOUSE" ) == 0 ) 
					{
						PostMessage( h, WM_APP+1, FALSE, sizeof(size_t) );
						ActivateMouseHook( h ); 
						PostMessage( h, WM_APP+1, TRUE, sizeof(size_t) );
					}
					else if( CompareInvariant( hook, "KEYBOARD" ) == 0 ) ActivateKeyboardHook( h ); 
					else if( CompareInvariant( hook, "SHELL" ) == 0 ) ActivateShellHook( h ); 
				}
			}
		}
		else if( CompareInvariant( in.Current(), "STOP" ) == 0 ) 
		{
			char* hook = in.MoveNext();
			if( hook != nullptr )
			{
				if( CompareInvariant( hook, "MOUSE" ) == 0 ) 
				{
						ActivateMouseHook( NULL ); 
				}
				else if( CompareInvariant( hook, "KEYBOARD" ) == 0 ) ActivateKeyboardHook( NULL ); 
				else if( CompareInvariant( hook, "SHELL" ) == 0 ) ActivateShellHook( NULL ); 
			}
		}
		else if( CompareInvariant( in.Current(), "LEAVEWITHOUTDEACTIVATION" ) == 0 )
		{
			return 0;
		}
	}
	DeactivateAllHooks();
	return 0;
}

