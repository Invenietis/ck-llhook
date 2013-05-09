#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.LLHook\INativeKeyboardHook.cs) is part of CiviKey. 
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
using System.Windows.Forms;

namespace CK.Windows
{

    /// <summary>
    /// Describes up/down transitions.
    /// </summary>
    public enum KeyTransition
    {
        /// <summary>
        /// Non applicable.
        /// </summary>
        None = 0,
        /// <summary>
        /// Key message has been generated while the key was down (typical effect of the repeater).
        /// </summary>
        DownDown = 1+1,
        /// <summary>
        /// Key message has been generated on the down to up transition (standard keystrokes work like this).
        /// </summary>
        DownUp = 3+1,
        /// <summary>
        /// Key message has been generated on the up to down transition (typically control keys uses this).
        /// </summary>
        UpDown = 0+1,
        /// <summary>
        /// Key message has been generated while the key was up. This should rarely happen.
        /// </summary>
        UpUp = 2+1
    }

    /// <summary>
    /// Describes a key event.
    /// </summary>
    public class NativeKeyboardEventArgs : EventArgs
    {
        public readonly int WParam;
        public readonly int LParam;

        internal NativeKeyboardEventArgs( int wParam, int lParam )
        {
            WParam = wParam;
            LParam = lParam;
        }

        /// <summary>
        /// Gets the Virtual Key Code of the key.
        /// </summary>
        public Keys VirtualKeyCode { get { return (Keys)WParam; } }

        /// <summary>
        /// Gets the repeat count. 
        /// The value is the number of times the keystroke is repeated as a result of the user's holding down the key.
        /// </summary>
        /// <remarks>
        /// You can check the repeat count to determine whether a keystroke message represents more than one keystroke. 
        /// The system increments the count when the keyboard generates WM_KEYDOWN or WM_SYSKEYDOWN messages faster than 
        /// an application can process them. 
        /// This often occurs when the user holds down a key long enough to start the keyboard's automatic repeat feature. 
        /// Instead of filling the system message queue with the resulting key-down messages, the system combines the 
        /// messages into a single key down message and increments the repeat count.
        /// </remarks>
        public int RepeatCount { get { return LParam & 0x7FFF; } }

        /// <summary>
        /// Gets the scan code. 
        /// The scan code is the value that the keyboard hardware generates when the user presses a key. 
        /// It is a device-dependent value that identifies the key pressed, as opposed to the character represented by the key. 
        /// An application typically ignores scan codes. 
        /// Instead, it uses the device-independent virtual-key codes to interpret keystroke messages.
        /// </summary>
        public int ScanCode { get { return (LParam >> 15) & 0x7F; } }
        
        /// <summary>
        /// Gets whether the key is an extended key. 
        /// The key is one of the additional keys on the enhanced keyboard: the ALT and CTRL keys on the right-hand side of the keyboard; the 
        /// INS, DEL, HOME, END, PAGE UP, PAGE DOWN, and arrow keys in the clusters to the left of the numeric keypad; 
        /// the NUM LOCK key; the BREAK (CTRL+PAUSE) key; the PRINT SCRN key; and the divide (/) and ENTER keys in the numeric keypad. 
        /// </summary>
        public bool IsExtendedKey { get { return (LParam&(1<<22)) != 0; } }

        /// <summary>
        /// Gets whether the ALT key was down when the keystroke message was generated. 
        /// </summary>
        public bool IsAltPressed { get { return (LParam & (1 << 29)) != 0; } }

        /// <summary>
        /// Gets whether the key that generated the keystroke message was previously down. 
        /// You can use this flag to identify keystroke messages generated by the keyboard's automatic repeat feature.
        /// This flag is set to 1 for WM_KEYDOWN and WM_SYSKEYDOWN keystroke messages generated by the automatic repeat feature.
        /// </summary>
        public bool WasKeyDown { get { return (LParam & (1 << 30)) != 0; } }

        /// <summary>
        /// Gets whether the key is up or down. 
        /// </summary>
        public bool IsKeyDown { get { return LParam > 0; } }

        /// <summary>
        /// Gets the <see cref="KeyTransition"/>.
        /// </summary>
        public KeyTransition KeyTransition { get { return (KeyTransition)( ((uint)LParam) >> 30 )+1; } }

        /// <summary>
        /// Overridden to return the description of the event properties.
        /// </summary>
        /// <returns>
        /// String that describes the key event.
        /// </returns>
        public override string ToString()
        {
            string r = String.Format( "VK='{0}', ScanCode={1}, Transition={2}", VirtualKeyCode, ScanCode, KeyTransition );
            if( RepeatCount > 1 )
            {
                r += String.Format( ", RepeatCount={0}", RepeatCount );
            }
            if( IsExtendedKey )
            {
                r += ", IsExtendedKey";
            }
            if( IsAltPressed )
            {
                r += ", IsAltPressed";
            }
            return r;
        }
    }

    /// <summary>
    /// Exposes the <see cref="NativeKeyboardEventArgs"/> event.
    /// </summary>
    public interface INativeKeyboardHook : INativeGlobalHook
    {
        event EventHandler<NativeKeyboardEventArgs> Event;
    }
}
