/*----------------------------------------------------------------------------
* This file (CK.LLHook\CK.MiniCRT\Src\CK.Memory.h) is part of CiviKey. 
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