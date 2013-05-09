/*----------------------------------------------------------------------------
* This file (CK.LLHook.Native\Src\BasicLog.cpp) is part of CiviKey. 
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
#include <CK.String.h>

void WINAPI BasicLogWrite( LPCWSTR line )
{
	// Any log mechanism should come here... If required once.
	// (Note: I failed making SysInternals DebugViewer catch OutputDebugString!)
	// OutputDebugString( line );
}
