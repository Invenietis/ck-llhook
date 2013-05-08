namespace ViewHookControls
{
    partial class HookStatus
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
            this._stop = new System.Windows.Forms.Button();
            this._start = new System.Windows.Forms.Button();
            this._startStatus = new System.Windows.Forms.Label();
            this._statusRefresh = new System.Windows.Forms.Button();
            this._activeStatus = new System.Windows.Forms.Label();
            this._textLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _stop
            // 
            this._stop.Location = new System.Drawing.Point(6, 73);
            this._stop.Name = "_stop";
            this._stop.Size = new System.Drawing.Size(106, 23);
            this._stop.TabIndex = 5;
            this._stop.Text = "Stop";
            this._stop.UseVisualStyleBackColor = true;
            this._stop.Click += new System.EventHandler(this._start_Click);
            // 
            // _start
            // 
            this._start.Location = new System.Drawing.Point(6, 44);
            this._start.Name = "_start";
            this._start.Size = new System.Drawing.Size(106, 23);
            this._start.TabIndex = 6;
            this._start.Text = "Start";
            this._start.UseVisualStyleBackColor = true;
            this._start.Click += new System.EventHandler(this._start_Click);
            // 
            // _startStatus
            // 
            this._startStatus.AutoSize = true;
            this._startStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._startStatus.Location = new System.Drawing.Point(3, 5);
            this._startStatus.Name = "_startStatus";
            this._startStatus.Size = new System.Drawing.Size(74, 13);
            this._startStatus.TabIndex = 4;
            this._startStatus.Text = "Start Status";
            // 
            // _statusRefresh
            // 
            this._statusRefresh.Location = new System.Drawing.Point(88, 5);
            this._statusRefresh.Name = "_statusRefresh";
            this._statusRefresh.Size = new System.Drawing.Size(24, 33);
            this._statusRefresh.TabIndex = 3;
            this._statusRefresh.Text = "R";
            this._statusRefresh.UseVisualStyleBackColor = true;
            this._statusRefresh.Click += new System.EventHandler(this.RefreshStatus_Clicked);
            // 
            // _activeStatus
            // 
            this._activeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._activeStatus.Location = new System.Drawing.Point(3, 21);
            this._activeStatus.Name = "_activeStatus";
            this._activeStatus.Size = new System.Drawing.Size(78, 13);
            this._activeStatus.TabIndex = 4;
            this._activeStatus.Text = "Active Status";
            this._activeStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _textLog
            // 
            this._textLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textLog.Location = new System.Drawing.Point(119, 3);
            this._textLog.Multiline = true;
            this._textLog.Name = "_textLog";
            this._textLog.Size = new System.Drawing.Size(685, 93);
            this._textLog.TabIndex = 7;
            // 
            // HookStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._textLog);
            this.Controls.Add(this._stop);
            this.Controls.Add(this._start);
            this.Controls.Add(this._activeStatus);
            this.Controls.Add(this._startStatus);
            this.Controls.Add(this._statusRefresh);
            this.Name = "HookStatus";
            this.Size = new System.Drawing.Size(807, 99);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _stop;
        private System.Windows.Forms.Button _start;
        private System.Windows.Forms.Label _startStatus;
        private System.Windows.Forms.Button _statusRefresh;
        private System.Windows.Forms.Label _activeStatus;
        private System.Windows.Forms.TextBox _textLog;

    }
}
