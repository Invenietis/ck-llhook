using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CK.Windows
{
#if DEBUG
    [CK.Interop.NativeDll( DefaultDllName32 = "CK.LLHook.Native.32.dbg.dll", DefaultDllName64 = "CK.LLHook.Native.64.dbg.dll" )]
#else
    [CK.Interop.NativeDll( DefaultDllName32 = "CK.LLHook.Native.32.dll", DefaultDllName64 = "CK.LLHook.Native.64.dll" )]
#endif
    public interface INativeGlobalHookInterop
    {
        [CK.Interop.DllImport( SetLastError = true )]
        IntPtr DeactivateAllHooks();
        
        [CK.Interop.DllImport( SetLastError = true )]
        IntPtr GetShellHookTarget();

        [CK.Interop.DllImport( SetLastError = true )]
        bool ActivateShellHook( IntPtr targetWnd );

        [CK.Interop.DllImport( SetLastError = true )]
        IntPtr GetMouseHookTarget();

        [CK.Interop.DllImport( SetLastError = true )]
        bool ActivateMouseHook( IntPtr targetWnd );

        [CK.Interop.DllImport( SetLastError = true )]
        IntPtr GetKeyboardHookTarget();

        [CK.Interop.DllImport( SetLastError = true )]
        bool ActivateKeyboardHook( IntPtr targetWnd );

    }
}
