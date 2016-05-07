namespace Waveform_Demo
{
    partial class ThemeEditor
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
            this.dlg_Color = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_Font = new System.Windows.Forms.ComboBox();
            this.spnr_FontSize = new System.Windows.Forms.NumericUpDown();
            this.btn_FontColor = new System.Windows.Forms.Button();
            this.btn_GraphColor = new System.Windows.Forms.Button();
            this.btn_CursorColor = new System.Windows.Forms.Button();
            this.btn_TimeSpanColor = new System.Windows.Forms.Button();
            this.btn_BackgroundColor = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spnr_FontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // dlg_Color
            // 
            this.dlg_Color.AnyColor = true;
            this.dlg_Color.Color = System.Drawing.Color.Yellow;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Font:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Font Color:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Font Size:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Graph Color:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Measure Transition Color:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(89, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Cursor Color:";
            // 
            // cmb_Font
            // 
            this.cmb_Font.FormattingEnabled = true;
            this.cmb_Font.Location = new System.Drawing.Point(49, 34);
            this.cmb_Font.Name = "cmb_Font";
            this.cmb_Font.Size = new System.Drawing.Size(242, 21);
            this.cmb_Font.TabIndex = 6;
            // 
            // spnr_FontSize
            // 
            this.spnr_FontSize.Location = new System.Drawing.Point(358, 34);
            this.spnr_FontSize.Name = "spnr_FontSize";
            this.spnr_FontSize.Size = new System.Drawing.Size(40, 20);
            this.spnr_FontSize.TabIndex = 7;
            // 
            // btn_FontColor
            // 
            this.btn_FontColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_FontColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FontColor.Location = new System.Drawing.Point(162, 79);
            this.btn_FontColor.Name = "btn_FontColor";
            this.btn_FontColor.Size = new System.Drawing.Size(30, 19);
            this.btn_FontColor.TabIndex = 8;
            this.btn_FontColor.UseVisualStyleBackColor = false;
            this.btn_FontColor.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // btn_GraphColor
            // 
            this.btn_GraphColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_GraphColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_GraphColor.Location = new System.Drawing.Point(162, 104);
            this.btn_GraphColor.Name = "btn_GraphColor";
            this.btn_GraphColor.Size = new System.Drawing.Size(30, 19);
            this.btn_GraphColor.TabIndex = 9;
            this.btn_GraphColor.UseVisualStyleBackColor = false;
            this.btn_GraphColor.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // btn_CursorColor
            // 
            this.btn_CursorColor.BackColor = System.Drawing.Color.Yellow;
            this.btn_CursorColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CursorColor.Location = new System.Drawing.Point(162, 129);
            this.btn_CursorColor.Name = "btn_CursorColor";
            this.btn_CursorColor.Size = new System.Drawing.Size(30, 19);
            this.btn_CursorColor.TabIndex = 10;
            this.btn_CursorColor.UseVisualStyleBackColor = false;
            this.btn_CursorColor.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // btn_TimeSpanColor
            // 
            this.btn_TimeSpanColor.BackColor = System.Drawing.Color.White;
            this.btn_TimeSpanColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TimeSpanColor.Location = new System.Drawing.Point(162, 179);
            this.btn_TimeSpanColor.Name = "btn_TimeSpanColor";
            this.btn_TimeSpanColor.Size = new System.Drawing.Size(30, 19);
            this.btn_TimeSpanColor.TabIndex = 11;
            this.btn_TimeSpanColor.UseVisualStyleBackColor = false;
            this.btn_TimeSpanColor.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // btn_BackgroundColor
            // 
            this.btn_BackgroundColor.BackColor = System.Drawing.Color.Black;
            this.btn_BackgroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BackgroundColor.Location = new System.Drawing.Point(162, 154);
            this.btn_BackgroundColor.Name = "btn_BackgroundColor";
            this.btn_BackgroundColor.Size = new System.Drawing.Size(30, 19);
            this.btn_BackgroundColor.TabIndex = 12;
            this.btn_BackgroundColor.UseVisualStyleBackColor = false;
            this.btn_BackgroundColor.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Background Color:";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(321, 221);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 14;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(240, 221);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 15;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            // 
            // ThemeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 256);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_BackgroundColor);
            this.Controls.Add(this.btn_TimeSpanColor);
            this.Controls.Add(this.btn_CursorColor);
            this.Controls.Add(this.btn_GraphColor);
            this.Controls.Add(this.btn_FontColor);
            this.Controls.Add(this.spnr_FontSize);
            this.Controls.Add(this.cmb_Font);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ThemeEditor";
            this.Text = "Waveform Theme Editor";
            ((System.ComponentModel.ISupportInitialize)(this.spnr_FontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog dlg_Color;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_Font;
        private System.Windows.Forms.NumericUpDown spnr_FontSize;
        private System.Windows.Forms.Button btn_FontColor;
        private System.Windows.Forms.Button btn_GraphColor;
        private System.Windows.Forms.Button btn_CursorColor;
        private System.Windows.Forms.Button btn_TimeSpanColor;
        private System.Windows.Forms.Button btn_BackgroundColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Ok;
    }
}