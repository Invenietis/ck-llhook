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
    public partial class KeyboardHook : UserControl
    {
        protected HookStatus HookStatus;
        
        public KeyboardHook()
        {
            InitializeComponent();
        }

        public new MainWindow Parent
        {
            get { return (MainWindow)base.Parent; }
        }

        protected override void OnLoad( EventArgs e )
        {
            if( Site == null || !Site.DesignMode )
            {
                Parent.Load += ( o, ev ) =>
                {
                    if( Parent.NativeHookManager != null )
                    {
                        HookStatus.SetHook( Parent.NativeHookManager.KeyboardHook );
                        Parent.NativeHookManager.KeyboardHook.Event += ( oE, eE ) => HookStatus.LogWriteLine( eE.ToString() );
                    }
                    else Enabled = false;
                };
            }
            base.OnLoad( e );
        }
    }
}
