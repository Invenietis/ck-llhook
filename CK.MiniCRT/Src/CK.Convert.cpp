#include "stdafx.h"
#include "CK.Convert.h"
#include "CK.Char.h"

int CK::Convert::DecimalStringToInt( const char* str )
{
	char cur = *str++;
	// Save the negative sign, if it exists.
    char neg = cur;
    if( cur == '-' || cur == '+' ) cur = *str++;

    // While digits, sum.
	int total = 0;
    while( CK::Char::IsDigit( cur ) )
    {
        total = 10*total + (cur-'0');
        cur = *str++;
    }
	return neg == '-' ? -total : total;
}

int CK::Convert::DecimalStringToInt( const wchar_t* str )
{
	wchar_t cur = *str++;
	// Save the negative sign, if it exists.
    wchar_t neg = cur;
    if( cur == '-' || cur == '+' ) cur = *str++;

    // While digits, sum.
	int total = 0;
    while( CK::Char::IsDigit( cur ) )
    {
        total = 10*total + (cur-'0');			
        cur = *str++;					
    }
	return neg == '-' ? -total : total;
}

