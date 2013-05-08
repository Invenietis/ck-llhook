namespace ViewHookControls
{
    partial class MainWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._shellHook = new ViewHookControls.ShellHook();
            this._keyboardHook = new ViewHookControls.KeyboardHook();
            this._mouseHook = new ViewHookControls.MouseHook();
            this._globalBox = new System.Windows.Forms.GroupBox();
            this._logs = new System.Windows.Forms.TextBox();
            this._slowDownTime = new System.Windows.Forms.NumericUpDown();
            this._slowDown = new System.Windows.Forms.CheckBox();
            this._globalBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._slowDownTime)).BeginInit();
            this.SuspendLayout();
            // 
            // _shellHook
            // 
            this._shellHook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._shellHook.Location = new System.Drawing.Point(3, 12);
            this._shellHook.Name = "_shellHook";
            this._shellHook.Size = new System.Drawing.Size(598, 137);
            this._shellHook.TabIndex = 0;
            // 
            // _keyboardHook
            // 
            this._keyboardHook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._keyboardHook.Location = new System.Drawing.Point(4, 156);
            this._keyboardHook.Name = "_keyboardHook";
            this._keyboardHook.Size = new System.Drawing.Size(599, 137);
            this._keyboardHook.TabIndex = 1;
            // 
            // _mouseHook
            // 
            this._mouseHook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mouseHook.Location = new System.Drawing.Point(4, 300);
            this._mouseHook.Name = "_mouseHook";
            this._mouseHook.Size = new System.Drawing.Size(598, 137);
            this._mouseHook.TabIndex = 2;
            // 
            // _globalBox
            // 
            this._globalBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._globalBox.Controls.Add(this._logs);
            this._globalBox.Location = new System.Drawing.Point(4, 494);
            this._globalBox.Name = "_globalBox";
            this._globalBox.Size = new System.Drawing.Size(599, 184);
            this._globalBox.TabIndex = 3;
            this._globalBox.TabStop = false;
            this._globalBox.Text = "Logs";
            // 
            // _logs
            // 
            this._logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._logs.Location = new System.Drawing.Point(3, 16);
            this._logs.Multiline = true;
            this._logs.Name = "_logs";
            this._logs.Size = new System.Drawing.Size(593, 165);
            this._logs.TabIndex = 0;
            // 
            // _slowDownTime
            // 
            this._slowDownTime.Location = new System.Drawing.Point(147, 445);
            this._slowDownTime.Name = "_slowDownTime";
            this._slowDownTime.Size = new System.Drawing.Size(46, 20);
            this._slowDownTime.TabIndex = 4;
            this._slowDownTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._slowDownTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // _slowDown
            // 
            this._slowDown.AutoSize = true;
            this._slowDown.Location = new System.Drawing.Point(12, 447);
            this._slowDown.Name = "_slowDown";
            this._slowDown.Size = new System.Drawing.Size(133, 17);
            this._slowDown.TabIndex = 5;
            this._slowDown.Text = "Slow down GUI thread";
            this._slowDown.UseVisualStyleBackColor = true;
            this._slowDown.CheckedChanged += new System.EventHandler(this._slowDown_CheckedChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 690);
            this.Controls.Add(this._slowDown);
            this.Controls.Add(this._slowDownTime);
            this.Controls.Add(this._globalBox);
            this.Controls.Add(this._mouseHook);
            this.Controls.Add(this._keyboardHook);
            this.Controls.Add(this._shellHook);
            this.Name = "MainWindow";
            this.Text = "Test Native Hooks - ";
            this._globalBox.ResumeLayout(false);
            this._globalBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._slowDownTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ShellHook _shellHook;
        private KeyboardHook _keyboardHook;
        private MouseHook _mouseHook;
        private System.Windows.Forms.GroupBox _globalBox;
        private System.Windows.Forms.TextBox _logs;
        private System.Windows.Forms.NumericUpDown _slowDownTime;
        private System.Windows.Forms.CheckBox _slowDown;

    }
}

