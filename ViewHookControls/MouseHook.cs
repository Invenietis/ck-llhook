#region LGPL License
/*----------------------------------------------------------------------------
* This file (CK.LLHook\ViewHookControls\MouseHook.cs) is part of CiviKey. 
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
