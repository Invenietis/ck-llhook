/*----------------------------------------------------------------------------
* This file (CK.LLHook\CK.MiniCRT\Src\CK.Char.h) is part of CiviKey. 
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

#pragma once
#include <Windows.h>

namespace CK
{
	namespace Char
	{
		namespace WCharMask
		{
			const unsigned short UPPER = 0x1;		/* upper case letter */
			const unsigned short LOWER = 0x2;		/* lower case letter */
			const unsigned short DIGIT = 0x4;		/* digit[0-9] */
			const unsigned short SPACE = 0x8;		/* tab, carriage return, newline, vertical tab or form feed */
			const unsigned short PUNCT = 0x10;		/* punctuation character */
			const unsigned short CONTROL = 0x20;    /* control character */
			const unsigned short BLANK = 0x40;		/* space char */
			const unsigned short HEX = 0x80;		/* hexadecimal digit */
			const unsigned short ALPHA = (0x0100|_UPPER|_LOWER);  /* alphabetic character */
		}

		inline bool IsWCharMask( wchar_t c, unsigned short mask )
		{
			unsigned short ret;
			GetStringTypeW( CT_CTYPE1, (LPCWSTR)&c, 1, &ret);
			return (ret & mask) != 0;
		}

		inline bool IsSpace( char c )			{ return ((c >= 0x09 && c <= 0x0D) || (c == 0x20)); }
		inline bool IsSpace( wchar_t c )		{ return IsWCharMask( c, WCharMask::BLANK ); }
		inline bool IsUpper( char c )			{ return (c >= 'A' && c <= 'Z'); }
		inline bool IsUpper( wchar_t c )		{ return IsWCharMask( c, WCharMask::UPPER ); }
		inline bool IsAlpha( char c )			{ return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'); }
		inline bool IsAlpha( wchar_t c )		{ return IsWCharMask( c, WCharMask::ALPHA ); }
		inline bool IsDigit( char c )			{ return (c >= '0' && c <= '9'); }
		inline bool IsDigit( wchar_t c )		{ return IsWCharMask( c, WCharMask::DIGIT ); }
		inline bool IsAlphaNum( char c )		{ return IsAlpha( c ) || IsDigit( c ); }
		inline bool IsAlphaNum( wchar_t c )	{ return IsWCharMask( c, WCharMask::ALPHA|WCharMask::DIGIT ); }
		inline bool IsPrintable( char c )		{ return c >= ' '; }
		inline bool IsPrintable( wchar_t c )	{ return IsWCharMask( c, (unsigned short)~WCharMask::CONTROL ); }

		inline char ToUpper( char c ) { if( c < 'a' || c > 'z' ) return c; return c-0x20; }
		inline wchar_t ToUpper( wchar_t c ) { return (wchar_t)CharUpperW((LPWSTR)c); /*Handles chars directly (no &address of required).*/ }

		inline char ToLower( char c ) { if( c < 'A' || c > 'Z' ) return c; return c+0x20; }
		inline wchar_t ToLower( wchar_t c ) { return (wchar_t)CharLowerW((LPWSTR)c); }
	}
}