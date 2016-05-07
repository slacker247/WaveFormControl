namespace Waveform_Demo
{
    partial class PinsDlg
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.dgv_Pins = new System.Windows.Forms.DataGridView();
            this.dlg_ColorPicker = new System.Windows.Forms.ColorDialog();
            this.col_Visible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_PinID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PinName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_PinColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_StrobeColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MaxVolt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NumSamples = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_NumStrobes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Pins)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(475, 276);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 0;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(394, 276);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 1;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // dgv_Pins
            // 
            this.dgv_Pins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Pins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Pins.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_Visible,
            this.col_PinID,
            this.col_PinName,
            this.col_PinColor,
            this.col_StrobeColor,
            this.col_MaxVolt,
            this.col_NumSamples,
            this.col_NumStrobes});
            this.dgv_Pins.Location = new System.Drawing.Point(12, 12);
            this.dgv_Pins.Name = "dgv_Pins";
            this.dgv_Pins.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_Pins.Size = new System.Drawing.Size(538, 258);
            this.dgv_Pins.TabIndex = 2;
            this.dgv_Pins.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_Pins_MouseDoubleClick);
            this.dgv_Pins.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Pins_CellEndEdit);
            this.dgv_Pins.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Pins_CellContentClick);
            // 
            // dlg_ColorPicker
            // 
            this.dlg_ColorPicker.AnyColor = true;
            // 
            // col_Visible
            // 
            this.col_Visible.HeaderText = "Visible";
            this.col_Visible.Name = "col_Visible";
            this.col_Visible.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_Visible.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col_Visible.Width = 50;
            // 
            // col_PinID
            // 
            this.col_PinID.HeaderText = "Pin ID";
            this.col_PinID.Name = "col_PinID";
            this.col_PinID.ReadOnly = true;
            // 
            // col_PinName
            // 
            this.col_PinName.HeaderText = "Pin Name";
            this.col_PinName.Name = "col_PinName";
            // 
            // col_PinColor
            // 
            this.col_PinColor.HeaderText = "Pin Color";
            this.col_PinColor.Name = "col_PinColor";
            this.col_PinColor.ReadOnly = true;
            // 
            // col_StrobeColor
            // 
            this.col_StrobeColor.HeaderText = "Strobe Color";
            this.col_StrobeColor.Name = "col_StrobeColor";
            this.col_StrobeColor.ReadOnly = true;
            // 
            // col_MaxVolt
            // 
            this.col_MaxVolt.HeaderText = "Max Voltage";
            this.col_MaxVolt.Name = "col_MaxVolt";
            this.col_MaxVolt.ReadOnly = true;
            // 
            // col_NumSamples
            // 
            this.col_NumSamples.HeaderText = "# Samples";
            this.col_NumSamples.Name = "col_NumSamples";
            this.col_NumSamples.ReadOnly = true;
            // 
            // col_NumStrobes
            // 
            this.col_NumStrobes.HeaderText = "# Strobes";
            this.col_NumStrobes.Name = "col_NumStrobes";
            this.col_NumStrobes.ReadOnly = true;
            // 
            // PinsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 311);
            this.Controls.Add(this.dgv_Pins);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_Cancel);
            this.Name = "PinsDlg";
            this.Text = "Pins Editor";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Pins)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.DataGridView dgv_Pins;
        private System.Windows.Forms.ColorDialog dlg_ColorPicker;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_Visible;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_PinID;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_PinName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_PinColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_StrobeColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaxVolt;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NumSamples;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NumStrobes;
    }
}