namespace Waveform_Demo
{
    partial class FileOpenDlg
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
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Pins = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_Pattern = new System.Windows.Forms.TextBox();
            this.btn_BrowseP = new System.Windows.Forms.Button();
            this.chb_Digital = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txb_LowVolt = new System.Windows.Forms.TextBox();
            this.txb_HighVolt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txb_TimeSpan = new System.Windows.Forms.TextBox();
            this.btn_BrowseR = new System.Windows.Forms.Button();
            this.txb_Response = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_PickColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Location = new System.Drawing.Point(305, 183);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(224, 183);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pin:";
            // 
            // cmb_Pins
            // 
            this.cmb_Pins.FormattingEnabled = true;
            this.cmb_Pins.Location = new System.Drawing.Point(73, 42);
            this.cmb_Pins.Name = "cmb_Pins";
            this.cmb_Pins.Size = new System.Drawing.Size(121, 21);
            this.cmb_Pins.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pattern:";
            // 
            // txb_Pattern
            // 
            this.txb_Pattern.Location = new System.Drawing.Point(73, 122);
            this.txb_Pattern.Name = "txb_Pattern";
            this.txb_Pattern.Size = new System.Drawing.Size(211, 20);
            this.txb_Pattern.TabIndex = 5;
            // 
            // btn_BrowseP
            // 
            this.btn_BrowseP.Location = new System.Drawing.Point(290, 120);
            this.btn_BrowseP.Name = "btn_BrowseP";
            this.btn_BrowseP.Size = new System.Drawing.Size(75, 23);
            this.btn_BrowseP.TabIndex = 6;
            this.btn_BrowseP.Text = "Browse...";
            this.btn_BrowseP.UseVisualStyleBackColor = true;
            this.btn_BrowseP.Click += new System.EventHandler(this.btn_BrowseP_Click);
            // 
            // chb_Digital
            // 
            this.chb_Digital.AutoSize = true;
            this.chb_Digital.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_Digital.Location = new System.Drawing.Point(22, 19);
            this.chb_Digital.Name = "chb_Digital";
            this.chb_Digital.Size = new System.Drawing.Size(66, 17);
            this.chb_Digital.TabIndex = 7;
            this.chb_Digital.Text = "Is Digital";
            this.chb_Digital.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Low:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "High:";
            // 
            // txb_LowVolt
            // 
            this.txb_LowVolt.Location = new System.Drawing.Point(73, 70);
            this.txb_LowVolt.Name = "txb_LowVolt";
            this.txb_LowVolt.Size = new System.Drawing.Size(121, 20);
            this.txb_LowVolt.TabIndex = 10;
            // 
            // txb_HighVolt
            // 
            this.txb_HighVolt.Location = new System.Drawing.Point(73, 96);
            this.txb_HighVolt.Name = "txb_HighVolt";
            this.txb_HighVolt.Size = new System.Drawing.Size(121, 20);
            this.txb_HighVolt.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(212, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Time Span:";
            // 
            // txb_TimeSpan
            // 
            this.txb_TimeSpan.Location = new System.Drawing.Point(279, 42);
            this.txb_TimeSpan.Name = "txb_TimeSpan";
            this.txb_TimeSpan.Size = new System.Drawing.Size(100, 20);
            this.txb_TimeSpan.TabIndex = 13;
            // 
            // btn_BrowseR
            // 
            this.btn_BrowseR.Location = new System.Drawing.Point(290, 146);
            this.btn_BrowseR.Name = "btn_BrowseR";
            this.btn_BrowseR.Size = new System.Drawing.Size(75, 23);
            this.btn_BrowseR.TabIndex = 16;
            this.btn_BrowseR.Text = "Browse...";
            this.btn_BrowseR.UseVisualStyleBackColor = true;
            this.btn_BrowseR.Click += new System.EventHandler(this.btn_BrowseR_Click);
            // 
            // txb_Response
            // 
            this.txb_Response.Location = new System.Drawing.Point(73, 148);
            this.txb_Response.Name = "txb_Response";
            this.txb_Response.Size = new System.Drawing.Size(211, 20);
            this.txb_Response.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Response:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(221, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Pin Color:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_PickColor
            // 
            this.btn_PickColor.Location = new System.Drawing.Point(279, 68);
            this.btn_PickColor.Name = "btn_PickColor";
            this.btn_PickColor.Size = new System.Drawing.Size(75, 23);
            this.btn_PickColor.TabIndex = 18;
            this.btn_PickColor.Text = "Pick...";
            this.btn_PickColor.UseVisualStyleBackColor = true;
            this.btn_PickColor.Click += new System.EventHandler(this.btn_PickColor_Click);
            // 
            // FileOpenDlg
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(392, 218);
            this.Controls.Add(this.btn_PickColor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_BrowseR);
            this.Controls.Add(this.txb_Response);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txb_TimeSpan);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txb_HighVolt);
            this.Controls.Add(this.txb_LowVolt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chb_Digital);
            this.Controls.Add(this.btn_BrowseP);
            this.Controls.Add(this.txb_Pattern);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_Pins);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FileOpenDlg";
            this.Text = "FileOpenDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Pins;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_Pattern;
        private System.Windows.Forms.Button btn_BrowseP;
        private System.Windows.Forms.CheckBox chb_Digital;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_LowVolt;
        private System.Windows.Forms.TextBox txb_HighVolt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txb_TimeSpan;
        private System.Windows.Forms.Button btn_BrowseR;
        private System.Windows.Forms.TextBox txb_Response;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_PickColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}