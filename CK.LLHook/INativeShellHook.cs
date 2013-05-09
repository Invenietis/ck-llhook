#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.LLHook\INativeShellHook.cs) is part of CiviKey. 
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
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CK.Windows
{
    /// <summary>
    /// Describes any window change.
    /// </summary>
    public class ShellHookWindowChanged : EventArgs
    {
        /// <summary>
        /// The window handle.
        /// </summary>
        public readonly IntPtr WindowHandle;

        internal ShellHookWindowChanged( IntPtr hWnd )
        {
            WindowHandle = hWnd;
        }
    }

    /// <summary>
    /// Exposes <see cref="ShellHookWindowChanged"/> event for top-level window activation.
    /// </summary>
    public interface INativeShellHook : INativeGlobalHook
    {
        /// <summary>
        /// Fires whenever a top-level window activates.
        /// </summary>
        event EventHandler<ShellHookWindowChanged> TopLevelWindowActivated;
    }
}
