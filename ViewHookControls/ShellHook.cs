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
    public partial class ShellHook : UserControl
    {
        protected HookStatus HookStatus;
        
        public ShellHook()
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
                        HookStatus.SetHook( Parent.NativeHookManager.ShellHook );
                        Parent.NativeHookManager.ShellHook.TopLevelWindowActivated += ( oE, eE ) => HookStatus.LogWriteLine( "TopLevelWindowActivated: hWnd = 0x{0:X}", eE.WindowHandle );
                    }
                    else Enabled = false;
                };
            }
            base.OnLoad( e );
        }
    }
}
