﻿namespace ViewHookControls
{
    partial class ShellHook
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._box = new System.Windows.Forms.GroupBox();
            this.HookStatus = new ViewHookControls.HookStatus();
            this._box.SuspendLayout();
            this.SuspendLayout();
            // 
            // _box
            // 
            this._box.Controls.Add(this.HookStatus);
            this._box.Dock = System.Windows.Forms.DockStyle.Fill;
            this._box.Location = new System.Drawing.Point(0, 0);
            this._box.Name = "_box";
            this._box.Size = new System.Drawing.Size(599, 137);
            this._box.TabIndex = 0;
            this._box.TabStop = false;
            this._box.Text = "WH_SHELL";
            // 
            // HookStatus
            // 
            this.HookStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HookStatus.Location = new System.Drawing.Point(3, 16);
            this.HookStatus.Name = "HookStatus";
            this.HookStatus.Size = new System.Drawing.Size(593, 118);
            this.HookStatus.TabIndex = 0;
            // 
            // ShellHook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._box);
            this.Name = "ShellHook";
            this.Size = new System.Drawing.Size(599, 137);
            this._box.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _box;

    }
}
