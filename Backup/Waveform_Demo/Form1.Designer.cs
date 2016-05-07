namespace Waveform_Demo
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_ZoomIn = new System.Windows.Forms.Button();
            this.btn_ZoomOut = new System.Windows.Forms.Button();
            this.btn_xZoomIn = new System.Windows.Forms.Button();
            this.btn_xZoomOut = new System.Windows.Forms.Button();
            this.btn_EditTheme = new System.Windows.Forms.Button();
            this.btn_Pins = new System.Windows.Forms.Button();
            this.btn_ClearCursors = new System.Windows.Forms.Button();
            this.cmb_GraphMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ZoomToCursors = new System.Windows.Forms.Button();
            this.cmb_CursorMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chb_ShowStrobes = new System.Windows.Forms.CheckBox();
            this.chb_ShowStrobeFails = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.spnr_xOverlayOffset = new System.Windows.Forms.NumericUpDown();
            this.spnr_yOverlayOffset = new System.Windows.Forms.NumericUpDown();
            this.cms_RightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_ZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_ZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmb_ShowMeasurement = new System.Windows.Forms.CheckBox();
            this.btn_yZoomIn = new System.Windows.Forms.Button();
            this.btn_yZoomOut = new System.Windows.Forms.Button();
            this.lbl_CurrentSelection = new System.Windows.Forms.Label();
            this.twc_WaveFrom = new TEV_WaveformControl.TEV_WaveformControl();
            ((System.ComponentModel.ISupportInitialize)(this.spnr_xOverlayOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnr_yOverlayOffset)).BeginInit();
            this.cms_RightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ZoomIn
            // 
            this.btn_ZoomIn.Location = new System.Drawing.Point(12, 32);
            this.btn_ZoomIn.Name = "btn_ZoomIn";
            this.btn_ZoomIn.Size = new System.Drawing.Size(75, 23);
            this.btn_ZoomIn.TabIndex = 1;
            this.btn_ZoomIn.Text = "Zoom In";
            this.btn_ZoomIn.UseVisualStyleBackColor = true;
            this.btn_ZoomIn.Click += new System.EventHandler(this.btn_ZoomIn_Click);
            // 
            // btn_ZoomOut
            // 
            this.btn_ZoomOut.Location = new System.Drawing.Point(93, 32);
            this.btn_ZoomOut.Name = "btn_ZoomOut";
            this.btn_ZoomOut.Size = new System.Drawing.Size(75, 23);
            this.btn_ZoomOut.TabIndex = 2;
            this.btn_ZoomOut.Text = "Zoom Out";
            this.btn_ZoomOut.UseVisualStyleBackColor = true;
            this.btn_ZoomOut.Click += new System.EventHandler(this.btn_ZoomOut_Click);
            // 
            // btn_xZoomIn
            // 
            this.btn_xZoomIn.Location = new System.Drawing.Point(265, 32);
            this.btn_xZoomIn.Name = "btn_xZoomIn";
            this.btn_xZoomIn.Size = new System.Drawing.Size(75, 23);
            this.btn_xZoomIn.TabIndex = 3;
            this.btn_xZoomIn.Text = "x Zoom In";
            this.btn_xZoomIn.UseVisualStyleBackColor = true;
            this.btn_xZoomIn.Click += new System.EventHandler(this.btn_xZoomIn_Click);
            // 
            // btn_xZoomOut
            // 
            this.btn_xZoomOut.Location = new System.Drawing.Point(346, 32);
            this.btn_xZoomOut.Name = "btn_xZoomOut";
            this.btn_xZoomOut.Size = new System.Drawing.Size(75, 23);
            this.btn_xZoomOut.TabIndex = 4;
            this.btn_xZoomOut.Text = "x Zoom Out";
            this.btn_xZoomOut.UseVisualStyleBackColor = true;
            this.btn_xZoomOut.Click += new System.EventHandler(this.btn_xZoomOut_Click);
            // 
            // btn_EditTheme
            // 
            this.btn_EditTheme.Location = new System.Drawing.Point(440, 31);
            this.btn_EditTheme.Name = "btn_EditTheme";
            this.btn_EditTheme.Size = new System.Drawing.Size(75, 23);
            this.btn_EditTheme.TabIndex = 5;
            this.btn_EditTheme.Text = "Theme";
            this.btn_EditTheme.UseVisualStyleBackColor = true;
            this.btn_EditTheme.Click += new System.EventHandler(this.btn_EditTheme_Click);
            // 
            // btn_Pins
            // 
            this.btn_Pins.Location = new System.Drawing.Point(521, 31);
            this.btn_Pins.Name = "btn_Pins";
            this.btn_Pins.Size = new System.Drawing.Size(75, 23);
            this.btn_Pins.TabIndex = 6;
            this.btn_Pins.Text = "Pins";
            this.btn_Pins.UseVisualStyleBackColor = true;
            this.btn_Pins.Click += new System.EventHandler(this.btn_Pins_Click);
            // 
            // btn_ClearCursors
            // 
            this.btn_ClearCursors.Location = new System.Drawing.Point(178, 32);
            this.btn_ClearCursors.Name = "btn_ClearCursors";
            this.btn_ClearCursors.Size = new System.Drawing.Size(75, 23);
            this.btn_ClearCursors.TabIndex = 7;
            this.btn_ClearCursors.Text = "Clear Cursors";
            this.btn_ClearCursors.UseVisualStyleBackColor = true;
            this.btn_ClearCursors.Click += new System.EventHandler(this.btn_ClearCursors_Click);
            // 
            // cmb_GraphMode
            // 
            this.cmb_GraphMode.FormattingEnabled = true;
            this.cmb_GraphMode.Items.AddRange(new object[] {
            "Standard - Pattern",
            "Standard - Response",
            "Split",
            "Overlay"});
            this.cmb_GraphMode.Location = new System.Drawing.Point(93, 5);
            this.cmb_GraphMode.Name = "cmb_GraphMode";
            this.cmb_GraphMode.Size = new System.Drawing.Size(121, 21);
            this.cmb_GraphMode.TabIndex = 8;
            this.cmb_GraphMode.SelectedIndexChanged += new System.EventHandler(this.cmb_GraphMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Views:";
            // 
            // btn_ZoomToCursors
            // 
            this.btn_ZoomToCursors.Location = new System.Drawing.Point(265, 5);
            this.btn_ZoomToCursors.Name = "btn_ZoomToCursors";
            this.btn_ZoomToCursors.Size = new System.Drawing.Size(101, 23);
            this.btn_ZoomToCursors.TabIndex = 10;
            this.btn_ZoomToCursors.Text = "Zoom to Cursors";
            this.btn_ZoomToCursors.UseVisualStyleBackColor = true;
            this.btn_ZoomToCursors.Click += new System.EventHandler(this.btn_ZoomToCursors_Click);
            // 
            // cmb_CursorMode
            // 
            this.cmb_CursorMode.FormattingEnabled = true;
            this.cmb_CursorMode.Items.AddRange(new object[] {
            "Off",
            "Vertical",
            "Horizontal"});
            this.cmb_CursorMode.Location = new System.Drawing.Point(452, 5);
            this.cmb_CursorMode.Name = "cmb_CursorMode";
            this.cmb_CursorMode.Size = new System.Drawing.Size(121, 21);
            this.cmb_CursorMode.TabIndex = 11;
            this.cmb_CursorMode.SelectedIndexChanged += new System.EventHandler(this.cmb_CursorMode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Cursor Mode";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chb_ShowStrobes
            // 
            this.chb_ShowStrobes.AutoSize = true;
            this.chb_ShowStrobes.Checked = true;
            this.chb_ShowStrobes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_ShowStrobes.Location = new System.Drawing.Point(613, 8);
            this.chb_ShowStrobes.Name = "chb_ShowStrobes";
            this.chb_ShowStrobes.Size = new System.Drawing.Size(92, 17);
            this.chb_ShowStrobes.TabIndex = 13;
            this.chb_ShowStrobes.Text = "Show Strobes";
            this.chb_ShowStrobes.UseVisualStyleBackColor = true;
            this.chb_ShowStrobes.CheckedChanged += new System.EventHandler(this.chb_ShowStrobes_CheckedChanged);
            // 
            // chb_ShowStrobeFails
            // 
            this.chb_ShowStrobeFails.AutoSize = true;
            this.chb_ShowStrobeFails.Checked = true;
            this.chb_ShowStrobeFails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_ShowStrobeFails.Location = new System.Drawing.Point(613, 35);
            this.chb_ShowStrobeFails.Name = "chb_ShowStrobeFails";
            this.chb_ShowStrobeFails.Size = new System.Drawing.Size(111, 17);
            this.chb_ShowStrobeFails.TabIndex = 14;
            this.chb_ShowStrobeFails.Text = "Show Strobe Fails";
            this.chb_ShowStrobeFails.UseVisualStyleBackColor = true;
            this.chb_ShowStrobeFails.CheckedChanged += new System.EventHandler(this.chb_ShowStrobeFails_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Overlay Offset:";
            // 
            // spnr_xOverlayOffset
            // 
            this.spnr_xOverlayOffset.Location = new System.Drawing.Point(96, 58);
            this.spnr_xOverlayOffset.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spnr_xOverlayOffset.Name = "spnr_xOverlayOffset";
            this.spnr_xOverlayOffset.Size = new System.Drawing.Size(33, 20);
            this.spnr_xOverlayOffset.TabIndex = 16;
            this.spnr_xOverlayOffset.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spnr_xOverlayOffset.ValueChanged += new System.EventHandler(this.spnr_xOverlayOffset_ValueChanged);
            // 
            // spnr_yOverlayOffset
            // 
            this.spnr_yOverlayOffset.Location = new System.Drawing.Point(135, 58);
            this.spnr_yOverlayOffset.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spnr_yOverlayOffset.Name = "spnr_yOverlayOffset";
            this.spnr_yOverlayOffset.Size = new System.Drawing.Size(33, 20);
            this.spnr_yOverlayOffset.TabIndex = 17;
            this.spnr_yOverlayOffset.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spnr_yOverlayOffset.ValueChanged += new System.EventHandler(this.spnr_yOverlayOffset_ValueChanged);
            // 
            // cms_RightClick
            // 
            this.cms_RightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_ZoomIn,
            this.tsm_ZoomOut});
            this.cms_RightClick.Name = "cms_RightClick";
            this.cms_RightClick.Size = new System.Drawing.Size(130, 48);
            // 
            // tsm_ZoomIn
            // 
            this.tsm_ZoomIn.Name = "tsm_ZoomIn";
            this.tsm_ZoomIn.Size = new System.Drawing.Size(129, 22);
            this.tsm_ZoomIn.Text = "Zoom In";
            this.tsm_ZoomIn.Click += new System.EventHandler(this.tsm_ZoomIn_Click);
            // 
            // tsm_ZoomOut
            // 
            this.tsm_ZoomOut.Name = "tsm_ZoomOut";
            this.tsm_ZoomOut.Size = new System.Drawing.Size(129, 22);
            this.tsm_ZoomOut.Text = "Zoom Out";
            this.tsm_ZoomOut.Click += new System.EventHandler(this.tsm_ZoomOut_Click);
            // 
            // cmb_ShowMeasurement
            // 
            this.cmb_ShowMeasurement.AutoSize = true;
            this.cmb_ShowMeasurement.Checked = true;
            this.cmb_ShowMeasurement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmb_ShowMeasurement.Enabled = false;
            this.cmb_ShowMeasurement.Location = new System.Drawing.Point(613, 58);
            this.cmb_ShowMeasurement.Name = "cmb_ShowMeasurement";
            this.cmb_ShowMeasurement.Size = new System.Drawing.Size(120, 17);
            this.cmb_ShowMeasurement.TabIndex = 18;
            this.cmb_ShowMeasurement.Text = "Show Measurement";
            this.cmb_ShowMeasurement.UseVisualStyleBackColor = true;
            this.cmb_ShowMeasurement.Visible = false;
            this.cmb_ShowMeasurement.CheckedChanged += new System.EventHandler(this.cmb_ShowMeasurement_CheckedChanged);
            // 
            // btn_yZoomIn
            // 
            this.btn_yZoomIn.Location = new System.Drawing.Point(265, 58);
            this.btn_yZoomIn.Name = "btn_yZoomIn";
            this.btn_yZoomIn.Size = new System.Drawing.Size(75, 23);
            this.btn_yZoomIn.TabIndex = 20;
            this.btn_yZoomIn.Text = "y Zoom In";
            this.btn_yZoomIn.UseVisualStyleBackColor = true;
            this.btn_yZoomIn.Click += new System.EventHandler(this.btn_yZoomIn_Click);
            // 
            // btn_yZoomOut
            // 
            this.btn_yZoomOut.Location = new System.Drawing.Point(346, 58);
            this.btn_yZoomOut.Name = "btn_yZoomOut";
            this.btn_yZoomOut.Size = new System.Drawing.Size(75, 23);
            this.btn_yZoomOut.TabIndex = 21;
            this.btn_yZoomOut.Text = "y Zoom Out";
            this.btn_yZoomOut.UseVisualStyleBackColor = true;
            this.btn_yZoomOut.Click += new System.EventHandler(this.btn_yZoomOut_Click);
            // 
            // lbl_CurrentSelection
            // 
            this.lbl_CurrentSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_CurrentSelection.AutoSize = true;
            this.lbl_CurrentSelection.Location = new System.Drawing.Point(16, 410);
            this.lbl_CurrentSelection.Name = "lbl_CurrentSelection";
            this.lbl_CurrentSelection.Size = new System.Drawing.Size(54, 13);
            this.lbl_CurrentSelection.TabIndex = 22;
            this.lbl_CurrentSelection.Text = "Selection:";
            // 
            // twc_WaveFrom
            // 
            this.twc_WaveFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.twc_WaveFrom.BackColor = System.Drawing.SystemColors.WindowText;
            this.twc_WaveFrom.ContextMenuStrip = this.cms_RightClick;
            this.twc_WaveFrom.Location = new System.Drawing.Point(12, 84);
            this.twc_WaveFrom.Name = "twc_WaveFrom";
            this.twc_WaveFrom.Size = new System.Drawing.Size(712, 323);
            this.twc_WaveFrom.TabIndex = 19;
            this.twc_WaveFrom.MouseClick += new System.Windows.Forms.MouseEventHandler(this.wfc_Waveform_MouseClick);
            this.twc_WaveFrom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.wfc_Waveform_MouseDown);
            this.twc_WaveFrom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.wfc_Waveform_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 435);
            this.Controls.Add(this.lbl_CurrentSelection);
            this.Controls.Add(this.btn_yZoomOut);
            this.Controls.Add(this.btn_yZoomIn);
            this.Controls.Add(this.twc_WaveFrom);
            this.Controls.Add(this.spnr_yOverlayOffset);
            this.Controls.Add(this.cmb_ShowMeasurement);
            this.Controls.Add(this.spnr_xOverlayOffset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chb_ShowStrobeFails);
            this.Controls.Add(this.chb_ShowStrobes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_CursorMode);
            this.Controls.Add(this.btn_ZoomToCursors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_GraphMode);
            this.Controls.Add(this.btn_ClearCursors);
            this.Controls.Add(this.btn_Pins);
            this.Controls.Add(this.btn_EditTheme);
            this.Controls.Add(this.btn_xZoomOut);
            this.Controls.Add(this.btn_xZoomIn);
            this.Controls.Add(this.btn_ZoomOut);
            this.Controls.Add(this.btn_ZoomIn);
            this.Name = "Form1";
            this.Text = "Test Waveform Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.spnr_xOverlayOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnr_yOverlayOffset)).EndInit();
            this.cms_RightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ZoomIn;
        private System.Windows.Forms.Button btn_ZoomOut;
        private System.Windows.Forms.Button btn_xZoomIn;
        private System.Windows.Forms.Button btn_xZoomOut;
        private System.Windows.Forms.Button btn_EditTheme;
        private System.Windows.Forms.Button btn_Pins;
        private System.Windows.Forms.Button btn_ClearCursors;
        private System.Windows.Forms.ComboBox cmb_GraphMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ZoomToCursors;
        private System.Windows.Forms.ComboBox cmb_CursorMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_ShowStrobes;
        private System.Windows.Forms.CheckBox chb_ShowStrobeFails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown spnr_xOverlayOffset;
        private System.Windows.Forms.NumericUpDown spnr_yOverlayOffset;
        private System.Windows.Forms.ContextMenuStrip cms_RightClick;
        private System.Windows.Forms.ToolStripMenuItem tsm_ZoomIn;
        private System.Windows.Forms.ToolStripMenuItem tsm_ZoomOut;
        private System.Windows.Forms.CheckBox cmb_ShowMeasurement;
        private TEV_WaveformControl.TEV_WaveformControl twc_WaveFrom;
        private System.Windows.Forms.Button btn_yZoomIn;
        private System.Windows.Forms.Button btn_yZoomOut;
        private System.Windows.Forms.Label lbl_CurrentSelection;
    }
}

