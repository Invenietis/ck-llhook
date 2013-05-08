using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using CK.Core;

namespace CK.Windows
{
    public sealed partial class NativeHookManager : IDisposable
    {
        static INativeGlobalHookInterop _native;

        readonly IActivityLogger _logger;
        readonly ShellHookImpl _shell;
        readonly MouseHookImpl _mouse;
        readonly KeyboardHookImpl _keyboard;
        readonly Process _bridge;
        bool _disposed;

        /// <summary>
        /// First message handled for all hooks is managed by HookBase
        /// to handle activation/deactivation of its hook.
        /// </summary>
        public const int WM_APP = 0x8000;

        /// <summary>
        /// Defines the start state of a hook by exposing both
        /// status for 32 &amp; 64 bits.
        /// </summary>
        [Flags]
        public enum StartState
        {
            /// <summary>
            /// Both 32 &amp; 64 bits hooks are deactivated.
            /// </summary>
            Stopped = 0,
            /// <summary>
            /// The 32 bits hook is started.
            /// </summary>
            IsStarted32 = 1,
            /// <summary>
            /// The 64 bits hook is started.
            /// </summary>
            IsStarted64 = 2,
            /// <summary>
            /// Both hooks are started.
            /// </summary>
            IsStarted = IsStarted32 | IsStarted64
        }

        NativeHookManager( IActivityLogger logger, Process bridge )
        {
            _logger = logger;
            _bridge = bridge;
            _shell = new ShellHookImpl( this );
            _mouse = new MouseHookImpl( this );
            _keyboard = new KeyboardHookImpl( this );
        }

        public static NativeHookManager Create( IActivityLogger logger, bool withBridge = true )
        {
            if( logger == null ) throw new ArgumentNullException( "logger" );
            using( logger.OpenGroup( LogLevel.Info, "Initializing a new NativeHookManager." ) )
            {
                Process bridge = null;
                try
                {
                    // This method is thread-safe and the type is cached.
                    // It is useless to add yet another lock layer.
                    _native = CK.Interop.PInvoker.GetInvoker<INativeGlobalHookInterop>();
                    if( withBridge )
                    {
                        ProcessStartInfo s = new ProcessStartInfo();
                        s.FileName = "CK.LLHook.NativeBridge." + (OSVersionInfo.ProgramBits == OSVersionInfo.SoftwareArchitecture.Bit32 ? "64" : "32");
                        #if DEBUG
                        s.FileName += ".dbg.exe";
                        #else
                        s.FileName += ".exe";
                        #endif
                        s.UseShellExecute = false;
                        s.CreateNoWindow = true;
                        s.RedirectStandardInput = true;
                        bridge = Process.Start( s );
                    }
                }
                catch( Exception ex )
                {
                    logger.Error( ex );
                    return null;
                }
                return new NativeHookManager( logger, bridge );
            }
        }

        ~NativeHookManager()
        {
            DoShutdown( false, false );
        }

        /// <summary>
        /// Calls <see cref="Shutdown"/> without waiting for bridge termination.
        /// </summary>
        public void Dispose()
        {
            DoShutdown( true, false );
        }

        public void Shutdown( bool waitForTermination )
        {
            DoShutdown( true, waitForTermination );
        }

        void DoShutdown( bool disposing, bool waitForTermination )
        {
            if( !_disposed )
            {
                if( disposing ) GC.SuppressFinalize( this );
                _disposed = true;
                try
                {
                    _native.DeactivateAllHooks();
                    if( _bridge != null )
                    {
                        _bridge.StandardInput.WriteLine();
                        if( waitForTermination ) _bridge.WaitForExit();
                    }
                }
                catch( Exception ex )
                {
                    if( disposing ) _logger.Error( ex, "While shutting down NativeHookManager." );
                }
            }
        }

        public bool IsDisposed { get { return _disposed; } }

        void CheckDisposed()
        {
            if( _disposed ) throw new ObjectDisposedException( "NativeHookManager" );
        }


        public INativeShellHook ShellHook { get { return _shell; } }

        public INativeMouseHook MouseHook { get { return _mouse; } }

        public INativeKeyboardHook KeyboardHook { get { return _keyboard; } }

        class MouseHookImpl : HookBase, INativeMouseHook
        {
            public MouseHookImpl( NativeHookManager m )
                : base( m, _native.ActivateMouseHook, _native.GetMouseHookTarget )
            { 
            }

            public override string HookName { get { return "MOUSE"; } }

            protected override bool NeedsBridge { get { return false; } }

            public event EventHandler<NativeMouseButtonEventArgs> ButtonAction;
            public event EventHandler<NativeMouseWheelEventArgs> WheelAction;

            protected override void WndProc( ref Message m )
            {
                if( Manager.IsDisposed ) base.WndProc( ref m );
                else switch( m.Msg )
                    {
                        case WM_APP + 1:
                            {
                                int w = m.WParam.ToInt32();
                                var e = ButtonAction;
                                if( e != null )
                                {
                                    var ev = new NativeMouseButtonEventArgs( w );
                                    e( this, ev );
                                }
                                break;
                            }
                        case WM_APP + 2:
                            {
                                var eW = WheelAction;
                                if( eW != null )
                                {
                                    var ev = new NativeMouseWheelEventArgs( (Int16)(UInt16)m.WParam.ToInt32() );
                                    eW( this, ev );
                                }
                                break;
                            }
                        default: base.WndProc( ref m ); break;
                    }
            }
        }

        class KeyboardHookImpl : HookBase, INativeKeyboardHook
        {
            public KeyboardHookImpl( NativeHookManager m )
                : base( m, _native.ActivateKeyboardHook, _native.GetKeyboardHookTarget )
            { 
            }

            public override string HookName { get { return "KEYBOARD"; } }

            protected override bool NeedsBridge { get { return false; } }

            public event EventHandler<NativeKeyboardEventArgs> Event;

            protected override void WndProc( ref Message m )
            {
                if( Manager.IsDisposed ) base.WndProc( ref m );
                else switch( m.Msg )
                    {
                        case WM_APP + 1:
                            {
                                var e = Event;
                                if( e != null )
                                {
                                    var ev = new NativeKeyboardEventArgs( m.WParam.ToInt32(), m.LParam.ToInt32() );
                                    e( this, ev );
                                }
                                break;
                            }
                        default: base.WndProc( ref m ); break;
                    }
            }
        }

        class ShellHookImpl : HookBase, INativeShellHook
        {
            public ShellHookImpl( NativeHookManager m )
                : base( m, _native.ActivateShellHook, _native.GetShellHookTarget )
            { 
            }

            public event EventHandler<ShellHookWindowChanged> TopLevelWindowActivated;

            public override string HookName { get { return "SHELL"; } }

            protected override bool NeedsBridge { get { return true; } }

            protected override void WndProc( ref Message m )
            {
                if( Manager.IsDisposed ) base.WndProc( ref m );
                else switch( m.Msg )
                {
                    case WM_APP+1: 
                    {
                        var e = TopLevelWindowActivated;
                        if( e != null )
                        {
                            var ev = new ShellHookWindowChanged( m.WParam );
                            e( this, ev );
                        }
                        break;
                    }
                    default: base.WndProc( ref m ); break;
                }
            }
        }

        internal void SendBridgeCommand( string command )
        {
            Debug.Assert( !String.IsNullOrWhiteSpace( command ) );
            if( _bridge != null ) _bridge.StandardInput.WriteLine( command );
        }
    }
}
