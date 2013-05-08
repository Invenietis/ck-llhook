#pragma once


namespace CK
{
	namespace Memory
	{
		int Compare( const void* b1, const void* b2, size_t n );

		void* Set( void* dst, char value, size_t size );
		
		inline void* Zero( void* dst, size_t size ) { return Set( dst, 0, size ); }

		/*
			Handles memory overlapping correclty.
		*/
		void* Copy( void* dst, const void* src, size_t size );
	}
}