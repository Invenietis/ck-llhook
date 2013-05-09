/*----------------------------------------------------------------------------
* This file (CK.LLHook\CK.MiniCRT\Src\CK.Memory.cpp) is part of CiviKey. 
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
#include "CK.Memory.h"

int CK::Memory::Compare( const void* b1, const void* b2, size_t n )
{
	const unsigned char *p1 = (const unsigned char*)b1;
	const unsigned char *p2 = (const unsigned char*)b2;
	int r = 0;
	for( size_t i = 0; i < n; i++ )
	{
		if( (r = (p1[i]-p2[i])) != 0 ) break;
	}
	return r;
}

void* CK::Memory::Set( void* dst, char value, size_t size )
{
	char* p = (char*)dst;
	while( size-- ) *(p++) = value;
	return dst;
}

void* CK::Memory::Copy( void* dst, const void* src, size_t size )
{
	char* dp = (char*)dst;
	char* sp = (char*)src;
	if( dp <= sp || dp >= (sp + size) )
	{
		// Non-Overlapping Buffers
		while( size-- ) *(dp++) = *(sp++);
	}
	else
	{
		// copy from higher addresses to lower addresses
		dp += size - 1;
		sp += size - 1;
		while( size-- ) *(dp--) = *(sp--);
	}
	return dst;
}
