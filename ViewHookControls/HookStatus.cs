#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.LLHook\ViewHookControls\HookStatus.cs) is part of CiviKey. 
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
*     Invenietis <http://www.invenietis.com>
* All rights reserved. 
*-----------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CK.Windows;

namespace ViewHookControls
{
    public partial class HookStatus : UserControl
    {
        INativeGlobalHook _hook;

        public Func<IntPtr> GetActivateTargetFunc;

        public HookStatus()
        {
            InitializeComponent();
        }

        internal void SetHook( INativeGlobalHook hook )
        {
            _hook = hook;
            _hook.StartStateChanged += IsStartedChanged;
            RefreshStatus();
        }

        protected override void DestroyHandle()
        {
            _hook.StartStateChanged -= IsStartedChanged;
            base.DestroyHandle();
        }

        private void IsStartedChanged( object sender, EventArgs e )
        {
            RefreshStatus();
            switch( _hook.StartState )
            {
                case NativeHookManager.StartState.IsStarted32: _textLog.AppendText( "-- Started = 32 bits only --" ); break;
                case NativeHookManager.StartState.IsStarted64: _textLog.AppendText( "-- Started = 64 bits only --" ); break;
                case NativeHookManager.StartState.IsStarted: _textLog.AppendText( "-- Started = 32 & 64 bits --" ); break;
                default: _textLog.AppendText( "-- Stopped --" ); break;
            }
            _textLog.AppendText( Environment.NewLine );
        }

        private void RefreshStatus_Clicked( object sender, EventArgs e )
        {
            RefreshStatus();
        }

        void RefreshStatus()
        {
            _startStatus.Text = _hook.StartState.ToString();
            _activeStatus.Text = _hook.IsHookActivated ? "Activated" : "Deactivated";
        }

        public void LogWrite( string line )
        {
            _textLog.AppendText( line );
        }

        public void LogWriteLine( string line )
        {
            _textLog.AppendText( line );
            _textLog.AppendText( Environment.NewLine );
        }

        public void LogWriteLine( string line, params object [] args )
        {
            _textLog.AppendText( String.Format( line, args) );
            _textLog.AppendText( Environment.NewLine );
        }

        private void _start_Click( object sender, EventArgs e )
        {
            if( sender == _start )
            {
                _hook.Start();
            }
            else
            {
                _hook.Stop();
            }
        }

    }
}
