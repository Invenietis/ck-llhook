#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.LLHook\INativeMouseHook.cs) is part of CiviKey. 
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
    /// Defines the different available buttons on a mouse.
    /// </summary>
    public enum MouseButton
    {
        /// <summary>
        /// No button.
        /// </summary>
        None = 0,
        /// <summary>
        /// Left button.
        /// </summary>
        LeftButton = 1,
        /// <summary>
        /// Middle button.
        /// </summary>
        MiddleButton = 2,
        /// <summary>
        /// Right button.
        /// </summary>
        RightButton = 3,
        /// <summary>
        /// Primary extended button.
        /// </summary>
        XButton1 = 4,
        /// <summary>
        /// Secondary extended button.
        /// </summary>
        XButton2 = 5,
        /// <summary>
        /// The mouse wheel can be clicked.
        /// </summary>
        Wheel = 6
    }

    /// <summary>
    /// Possible button actions.
    /// </summary>
    public enum MouseButtonAction
    {
        /// <summary>
        /// Not an action.
        /// </summary>
        None = 0,
        /// <summary>
        /// Button is pressed.
        /// </summary>
        Down = 1,
        /// <summary>
        /// Button is up.
        /// </summary>
        Up = 2,
        /// <summary>
        /// Double click.
        /// </summary>
        DblClick = 3
    }

    /// <summary>
    /// Describes a mouse button event. 
    /// </summary>
    public class NativeMouseButtonEventArgs : EventArgs
    {
        /// <summary>
        /// Mouse button that has been sollicitated.
        /// </summary>
        public readonly MouseButton Button;

        /// <summary>
        /// Action (up, down, or double click).
        /// </summary>
        public readonly MouseButtonAction Action;

        internal NativeMouseButtonEventArgs( int w )
        {
            Button = (MouseButton)(w & 0xF);
            Action = (MouseButtonAction)( w >> 16);
        }

        public override string ToString()
        {
            return String.Format( "{0} - {1}", Button, Action );
        }
    }

    /// <summary>
    /// Describes a mouse wheel event (<see cref="Delta"/>).
    /// </summary>
    public class NativeMouseWheelEventArgs : EventArgs
    {
        /// <summary>
        /// The delta of the wheel. Can be positive or negative.
        /// </summary>
        public readonly int Delta;

        internal NativeMouseWheelEventArgs( int delta )
        {
            Delta = delta;
        }

        /// <summary>
        /// Overriden to display the <see cref="Delta"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format( "Wheel delta: {0}", Delta );
        }
    }

    /// <summary>
    /// Exposes the 2 events <see cref="NativeMouseButtonEventArgs"/> and <see cref="NativeMouseWheelEventArgs"/>.
    /// </summary>
    public interface INativeMouseHook : INativeGlobalHook
    {
        /// <summary>
        /// Fires for button actions.
        /// </summary>
        event EventHandler<NativeMouseButtonEventArgs> ButtonAction;
        
        /// <summary>
        /// Fires wheel event.
        /// </summary>
        event EventHandler<NativeMouseWheelEventArgs> WheelAction;
    }
}
