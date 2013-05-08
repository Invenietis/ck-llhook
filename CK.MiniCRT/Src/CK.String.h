#pragma once
#include <windows.h>

namespace CK
{
	namespace String
	{
		inline size_t StrLen( const char* str ) { return lstrlenA( str ); }
		inline size_t StrLen( const wchar_t* str ) { return lstrlenW( str ); }
				
		inline int CompareInvariant( const char* s1, const char* s2 ) { return CompareStringA( LOCALE_INVARIANT, 0, s1, -1, s2, -1 ) - 2; }
		inline int CompareInvariant( const wchar_t* s1, const wchar_t* s2 ) { return CompareStringW( LOCALE_INVARIANT, 0, s1, -1, s2, -1 ) - 2; }

		inline char* UnsafeCopy( char* dest, const char* src ) { return lstrcpyA( dest, src ); }
		inline wchar_t* UnsafeCopy( wchar_t* dest, const wchar_t* src ) { return lstrcpyW( dest, src ); }

		inline char* UnsafeConcat( char* dest, const char* src ) { return lstrcatA( dest, src ); }
		inline wchar_t* UnsafeConcat( wchar_t* dest, const wchar_t* src ) { return lstrcatW( dest, src ); }
		

		inline char* ToUpper( char* s ) { CharUpperBuffA( s, lstrlenA(s) ); return s; }
		inline wchar_t* ToUpper( wchar_t* s ) { CharUpperBuffW( s, lstrlenW(s) ); return s; }

		inline char* ToLower( char* s ) { CharLowerBuffA( s, lstrlenA(s) ); return s; }
		inline wchar_t* ToLower( wchar_t* s ) { CharLowerBuffW( s, lstrlenW(s) ); return s; }
	}
}
