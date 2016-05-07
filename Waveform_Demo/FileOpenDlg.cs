using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TEV_WaveformControl;

namespace Waveform_Demo
{
    public partial class FileOpenDlg : Form
    {
        public Pin m_Pin = null;

        public FileOpenDlg()
        {
            InitializeComponent();
            txb_HighVolt.Text = "0";
            txb_LowVolt.Text = "0";
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if(cmb_Pins.SelectedIndex > -1)
            {
            }
            else
            {
                Pen p = new Pen(colorDialog1.Color);
                m_Pin = new Pin(cmb_Pins.Text, cmb_Pins.Text, p);
                m_Pin.setDigital(chb_Digital.Checked);
                float l = 0;
                float.TryParse(txb_LowVolt.Text, out l);
                float h = 0;
                float.TryParse(txb_HighVolt.Text, out h);
                m_Pin.setThresholds(h, l);

                float timeSlice = 0;
                if (File.Exists(txb_Pattern.Text))
                {
                    StreamReader f = new StreamReader(txb_Pattern.Text);
                    List<String> lines = new List<String>();
                    lines.AddRange(f.ReadToEnd().Split('\n'));
                    float ts = 0;
                    float.TryParse(txb_TimeSpan.Text, out ts);
                    timeSlice = ts / (float)(lines.Count);
                    if (timeSlice > 0)
                    {
                        for (int i = 0; i < lines.Count; i++)
                        {
                            float v = 0;
                            float.TryParse(lines[i], out v);
                            m_Pin.addPatternSample(new TimeSample(i * timeSlice, TimeScale.NANOSECONDS), v);
                        }
                    }
                }
                if (File.Exists(txb_Response.Text))
                {
                    StreamReader f = new StreamReader(txb_Response.Text);
                    List<String> lines = new List<String>();
                    lines.AddRange(f.ReadToEnd().Split('\n'));
                    if (timeSlice == 0)
                    {
                        float ts = 0;
                        float.TryParse(txb_TimeSpan.Text, out ts);
                        timeSlice = ts / (float)(lines.Count);
                    }
                    if (timeSlice > 0)
                    {
                        for (int i = 0; i < lines.Count; i++)
                        {
                            float v = 0;
                            float.TryParse(lines[i], out v);
                            m_Pin.addResponseSample(new TimeSample(i * timeSlice, TimeScale.NANOSECONDS), v);
                        }
                    }
                    
                }
            }
        }

        private void btn_PickColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
            }
        }

        protected void setHighLow(List<String> lines)
        {
            float h = 0;
            float.TryParse(txb_HighVolt.Text, out h);
            float l = 0;
            float.TryParse(txb_LowVolt.Text, out l);
            for (int i = 0; i < lines.Count; i++)
            {
                float v = 0;
                if (float.TryParse(lines[i], out v))
                {
                    if (v > h)
                        h = v;
                    if (v < l)
                        l = v;
                }
            }
            txb_HighVolt.Text = h + "";
            txb_LowVolt.Text = l + "";
        }

        private void btn_BrowseP_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    txb_Pattern.Text = openFileDialog1.FileName;
                    StreamReader f = new StreamReader(openFileDialog1.FileName);
                    List<String> txt = new List<String>();
                    txt.AddRange(f.ReadToEnd().Split('\n'));
                    setHighLow(txt);
                    f.Close();
                }
            }
        }

        private void btn_BrowseR_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    txb_Response.Text = openFileDialog1.FileName;
                    StreamReader f = new StreamReader(openFileDialog1.FileName);
                    List<String> txt = new List<String>();
                    txt.AddRange(f.ReadToEnd().Split('\n'));
                    setHighLow(txt);
                    f.Close();
                }
            }
        }
    }
}
