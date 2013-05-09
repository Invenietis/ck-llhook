/*----------------------------------------------------------------------------
* This file (CK.LLHook\CK.MiniCRT\Src\CK.Convert.A.h) is part of CiviKey. 
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

