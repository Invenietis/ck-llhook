#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.LLHook\ViewHookControls\MainWindow.cs) is part of CiviKey. 
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
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CK.Windows;
using CK.Core;
using System.IO;

namespace ViewHookControls
{
    public partial class MainWindow : Form
    {
        NativeHookManager _hookManager;
        DefaultActivityLogger _logger;

        class TextBoxWriter : TextWriter
        {
            TextBox _box;
            Encoding  _encoding;

            public TextBoxWriter( TextBox box )
            {
                _box = box;
            }

            public override Encoding Encoding
            {
                get { return _encoding ?? (_encoding = new UnicodeEncoding( false, false )); }
            }

            public override void Write( char value )
            {
                _box.AppendText( value.ToString() );
            }

            public override void WriteLine( string value )
            {
                _box.AppendText( value );
                _box.AppendText( Environment.NewLine );
            }

        }

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Text += IntPtr.Size == 4 ? "32 bits applications" : "64 bits applications";
            }
            catch( Exception ex )
            {
                Debugger.Break();
                Console.WriteLine( ex.Message );
            }
        }

        protected override void OnLoad( EventArgs e )
        {
            if( Site == null || !Site.DesignMode )
            {
                _logger = new DefaultActivityLogger();
                _logger.Tap.Register( new ActivityLoggerTextWriterSink( new TextBoxWriter( _logs ) ) );
                _hookManager = NativeHookManager.Create( _logger, withBridge: true );
            }
            base.OnLoad( e );
        }

        protected override void OnHandleDestroyed( EventArgs e )
        {
            if( _hookManager != null ) _hookManager.Shutdown( true );
            base.OnHandleDestroyed( e );
        }

        public NativeHookManager NativeHookManager 
        { 
            get { return _hookManager; } 
        } 

        public DefaultActivityLogger Logger 
        { 
            get { return _logger; } 
        }

        Timer _slowDownTimer;

        private void _slowDown_CheckedChanged( object sender, EventArgs e )
        {
            if( _slowDown.Checked )
            {
                _slowDownTimer = new Timer();
                _slowDownTimer.Interval = 5;
                _slowDownTimer.Tick += _slowDownTimer_Tick;
                _slowDownTimer.Start();
            }
            else
            {
                _slowDownTimer.Stop();
                _slowDownTimer.Dispose();
                _slowDownTimer = null;
            }
        }

        void _slowDownTimer_Tick( object sender, EventArgs e )
        {
            System.Threading.Thread.Sleep( Convert.ToInt32( _slowDownTime.Value ) );
        }


    }
}
