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
    public partial class MouseHook : UserControl
    {
        protected HookStatus HookStatus;
        
        public MouseHook()
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
                        HookStatus.SetHook( Parent.NativeHookManager.MouseHook );
                        Parent.NativeHookManager.MouseHook.ButtonAction += ( oE, eE ) => HookStatus.LogWriteLine( eE.ToString() );
                        Parent.NativeHookManager.MouseHook.WheelAction += ( oE, eE ) => HookStatus.LogWriteLine( eE.ToString() );
                    }
                    else Enabled = false;
                };
            }
            base.OnLoad( e );
        }
    }
}
