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
