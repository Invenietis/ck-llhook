using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CK.Windows
{
    /// <summary>
    /// Common interface extended by all hooks.
    /// See <see cref="NativeHookManager"/>.
    /// </summary>
    public interface INativeGlobalHook
    {
        /// <summary>
        /// Fires whenever <see cref="StartState"/> changed.
        /// </summary>
        event EventHandler StartStateChanged;

        /// <summary>
        /// Get the  name of hook in uppercase without prefix nor suffix (SHELL, KEYBOARD, etc.).
        /// </summary>
        string HookName { get; }

        /// <summary>
        /// Gets or sets whether the internal message only window should be destroyed when <see cref="Stop"/> is called.
        /// False by default. Sets it to true if this hook is often <see cref="Start"/>/<see cref="Stop"/> to avoid recreating
        /// a brand new window each time.
        /// </summary>
        bool ReuseMessageOnlyWindow { get; set; }

        /// <summary>
        /// Starts the hook.
        /// </summary>
        /// <returns>False if an error occured. The error is logged into the logger associated to the <see cref="NativeHookManager"/>.</returns>
        bool Start();

        /// <summary>
        /// Stops the hook.
        /// </summary>
        /// <returns>False if an error occured. The error is logged into the logger associated to the <see cref="NativeHookManager"/>.</returns>
        bool Stop();

        /// <summary>
        /// Gets the hook activation state.
        /// </summary>
        NativeHookManager.StartState StartState { get; }

        /// <summary>
        /// Gets whether the hook is active. If true and <see cref="StartState"/> is <see cref="NativeHookManager.StartState.Stopped"/>, it means that 
        /// the native hook is bound to another application that uses the same CK.LLHook hook mechanism.
        /// </summary>
        bool IsHookActivated { get; }
    }
}
