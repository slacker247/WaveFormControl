namespace TEV_WaveformControl
{
    partial class TEV_WaveformControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbx_Waveform = new System.Windows.Forms.PictureBox();
            this.vs_ScrollBar = new System.Windows.Forms.VScrollBar();
            this.hs_ScrollBar = new System.Windows.Forms.HScrollBar();
            this.tmr_MouseDrag = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Waveform)).BeginInit();
            this.SuspendLayout();
            // 
            // pbx_Waveform
            // 
            this.pbx_Waveform.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbx_Waveform.BackColor = System.Drawing.SystemColors.WindowText;
            this.pbx_Waveform.Location = new System.Drawing.Point(0, 0);
            this.pbx_Waveform.Name = "pbx_Waveform";
            this.pbx_Waveform.Size = new System.Drawing.Size(525, 300);
            this.pbx_Waveform.TabIndex = 0;
            this.pbx_Waveform.TabStop = false;
            this.pbx_Waveform.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbx_Waveform_MouseMove);
            this.pbx_Waveform.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbx_Waveform_MouseDown);
            this.pbx_Waveform.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbx_Waveform_MouseUp);
            // 
            // vs_ScrollBar
            // 
            this.vs_ScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vs_ScrollBar.Location = new System.Drawing.Point(525, 0);
            this.vs_ScrollBar.Name = "vs_ScrollBar";
            this.vs_ScrollBar.Size = new System.Drawing.Size(17, 300);
            this.vs_ScrollBar.TabIndex = 1;
            this.vs_ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vs_ScrollBar_Scroll);
            // 
            // hs_ScrollBar
            // 
            this.hs_ScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hs_ScrollBar.Location = new System.Drawing.Point(0, 300);
            this.hs_ScrollBar.Name = "hs_ScrollBar";
            this.hs_ScrollBar.Size = new System.Drawing.Size(525, 17);
            this.hs_ScrollBar.TabIndex = 2;
            this.hs_ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hs_ScrollBar_Scroll);
            // 
            // tmr_MouseDrag
            // 
            this.tmr_MouseDrag.Interval = 10;
            this.tmr_MouseDrag.Tick += new System.EventHandler(this.tmr_MouseDrag_Tick);
            // 
            // TEV_WaveformControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.hs_ScrollBar);
            this.Controls.Add(this.vs_ScrollBar);
            this.Controls.Add(this.pbx_Waveform);
            this.DoubleBuffered = true;
            this.Name = "TEV_WaveformControl";
            this.Size = new System.Drawing.Size(542, 317);
            this.Resize += new System.EventHandler(this.WaveformCtrl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Waveform)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbx_Waveform;
        private System.Windows.Forms.VScrollBar vs_ScrollBar;
        private System.Windows.Forms.HScrollBar hs_ScrollBar;
        private System.Windows.Forms.Timer tmr_MouseDrag;
    }
}
