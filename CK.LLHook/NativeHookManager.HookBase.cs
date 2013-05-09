#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.LLHook\NativeHookManager.HookBase.cs) is part of CiviKey. 
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
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using CK.Core;

namespace CK.Windows
{
    public sealed partial class NativeHookManager 
    {
        abstract class HookBase : NativeWindow, INativeGlobalHook
        {
            readonly Func<IntPtr,bool> _activateFunc;
            readonly Func<IntPtr> _getTargetFunc;
            protected readonly NativeHookManager Manager;
            StartState _state;
            bool _reuseWindow;
            bool _waitingForStart;

            internal HookBase( NativeHookManager m, Func<IntPtr, bool> activate, Func<IntPtr> getTarget )
            {
                Manager = m;
                _activateFunc = activate;
                _getTargetFunc = getTarget;
            }

            public event EventHandler StartStateChanged;

            public abstract string HookName { get; }

            protected abstract bool NeedsBridge { get; }

            public bool IsHookActivated
            {
                get
                {
                    try
                    {
                        Manager.CheckDisposed();
                        return _getTargetFunc() != IntPtr.Zero;
                    }
                    catch( Exception ex )
                    {
                        Manager._logger.Error( ex, "Error while getting WH_{0} hook status.", HookName );
                        return false;
                    }
                }
            }

            public StartState StartState
            {
                get { return _state; }
            }

            public bool ReuseMessageOnlyWindow
            {
                get { return _reuseWindow; }
                set
                {
                    if( _reuseWindow != value )
                    {
                        _reuseWindow = value;
                        if( !_reuseWindow && Handle != null && _state == NativeHookManager.StartState.Stopped && !_waitingForStart ) DestroyHandle();
                    }
                }
            }

            public bool Start()
            {
                try
                {
                    Manager.CheckDisposed();
                    if( Handle == IntPtr.Zero )
                    {
                        CreateParams p = new CreateParams();
                        // HWND_MESSAGE : Message only Window.
                        p.Parent = new IntPtr( -3 );
                        p.Caption = String.Empty;
                        CreateHandle( p );
                    }
                    _waitingForStart = true;
                    if( !_activateFunc( Handle ) )
                    {
                        Manager._logger.Error( "Error while activating WH_{0} hook (GetLastError=0x{1:X}).", HookName, Marshal.GetLastWin32Error() );
                        return false;
                    }
                    if( NeedsBridge ) Manager.SendBridgeCommand( String.Format( "START {0} {1}", HookName, (int)Handle ) );
                }
                catch( Exception ex )
                {
                    Manager._logger.Error( ex, "Error while activating WH_{0} hook.", HookName );
                    return false;
                }
                return true;
            }

            public bool Stop()
            {
                Manager.CheckDisposed();
                if( !_activateFunc( IntPtr.Zero ) )
                {
                    Manager._logger.Error( "Error while deactivating WH_{0} hook (GetLastError=0x{1:X}).", HookName, Marshal.GetLastWin32Error() );
                    return false;
                }
                if( NeedsBridge ) Manager.SendBridgeCommand( String.Format( "STOP {0} {1}", HookName, (int)Handle ) );
                return true;
            }

            protected override void WndProc( ref Message m )
            {
                if( Manager.IsDisposed ) base.WndProc( ref m );
                else if( m.Msg == WM_APP )
                {
                    if( m.WParam == IntPtr.Zero )
                    {
                        _state &= ~(m.LParam.ToInt32() == 4 ? StartState.IsStarted32 : StartState.IsStarted64);
                        if( _state == NativeHookManager.StartState.Stopped && !_reuseWindow ) DestroyHandle();
                    }
                    else
                    {
                        _waitingForStart = false;
                        _state |= (m.LParam.ToInt32() == 4 ? StartState.IsStarted32 : StartState.IsStarted64);
                    }
                    var e = StartStateChanged;
                    if( e != null ) e( this, EventArgs.Empty );
                }
                else base.WndProc( ref m );
            }
        }


    }
}
