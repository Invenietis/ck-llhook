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
