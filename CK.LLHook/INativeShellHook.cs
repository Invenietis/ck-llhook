using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CK.Windows
{
    public class ShellHookWindowChanged : EventArgs
    {
        public readonly IntPtr WindowHandle;

        internal ShellHookWindowChanged( IntPtr hWnd )
        {
            WindowHandle = hWnd;
        }
    }

    public interface INativeShellHook : INativeGlobalHook
    {
        event EventHandler<ShellHookWindowChanged> TopLevelWindowActivated;
    }
}
