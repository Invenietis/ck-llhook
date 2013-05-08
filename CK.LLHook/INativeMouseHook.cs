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

    public class NativeMouseWheelEventArgs : EventArgs
    {
        /// <summary>
        /// The delta of the wheel.
        /// When a click occured, it is 0.
        /// </summary>
        public readonly int Delta;

        internal NativeMouseWheelEventArgs( int delta )
        {
            Delta = delta;
        }

        public override string ToString()
        {
            return String.Format( "Wheel delta: {0}", Delta );
        }
    }

    public interface INativeMouseHook : INativeGlobalHook
    {
        event EventHandler<NativeMouseButtonEventArgs> ButtonAction;
        event EventHandler<NativeMouseWheelEventArgs> WheelAction;
    }
}
