using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TEV_WaveformControl;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace Waveform_Demo
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// This form is designed to demonstrate the
    /// capabilities of the TEV_WaveformCtrl.
    /// </summary>
    ///
    /// <list type="table">
    ///     <listheader>
    ///         <term>Author</term>
    ///         <description>Version</description>
    ///     </listheader>
    ///     <item>
    ///         <term><a href="mailto:jeff.mccartney@aeroflex.com">
    ///               Jeff McCartney</a></term>
    ///         <description>0.2</description>
    ///     </item>
    /// </list>
    /////////////////////////////////////////////////////////
    public partial class Form1 : Form
    {
        /////////////////////////////////////////////////////////
        /// <summary>Contains the theme to be used when displaying
        /// the graph.</summary>
        /////////////////////////////////////////////////////////
        protected Theme m_Theme;
        /////////////////////////////////////////////////////////
        /// <summary>All the pins that will be displayed on the
        /// TEV_WaveformControl.</summary>
        /////////////////////////////////////////////////////////
        protected ArrayList m_Pins;
        /////////////////////////////////////////////////////////
        /// <summary>The x/y zoom for the graph.</summary>
        /////////////////////////////////////////////////////////
        protected float m_Zoom = 20f;
        /////////////////////////////////////////////////////////
        /// <summary>The x only zoom for the graph.</summary>
        /////////////////////////////////////////////////////////
        protected float m_xZoom = 0;
        public float xZoom
        {
            get { return m_xZoom; }
            set
            {
                m_xZoom = value;
                txb_xZoom.Text = m_xZoom + "";
                NotifyPropertyChanged("xZoom");
            }
        }
        /////////////////////////////////////////////////////////
        /// <summary>The y only zoom for the graph.</summary>
        /////////////////////////////////////////////////////////
        protected float m_yZoom = 0;
        public float yZoom
        {
            get { return m_yZoom; }
            set
            {
                m_yZoom = value;
                txb_yZoom.Text = m_yZoom + "";
                NotifyPropertyChanged("yZoom");
            }
        }
        /////////////////////////////////////////////////////////
        /// <summary>The TimeScale that will be used and displayed.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected TimeScale m_TimeScale = TimeScale.NANOSECONDS;
        protected int m_GraphStepping = 10;
        protected float m_ZoomFactor = 1f;
        public float ZoomFactor
        {
            get { return m_ZoomFactor; }
            set
            {
                m_ZoomFactor = value;
                txb_ZoomFactor.Text = m_ZoomFactor + "";
                NotifyPropertyChanged("ZoomFactor");
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Cosntructor: Initializes the gui components and
        /// initializes the pins array.  Then it sets the WaveformCtrl
        /// to the standard graphing mode and sets the cmb_GraphMode
        /// to the standard graphing mdoe.
        /// </summary>
        /////////////////////////////////////////////////////////
        public Form1()
        {
            InitializeComponent();
            twc_WaveFrom.SelectedPinChanged += new SelectedPinChangedEventHandler(wfc_Waveform_SelectedPinChanged);
            m_Pins = new ArrayList();
            init();
            twc_WaveFrom.setGraphMode(TEV_WaveformControl.TEV_WaveformControl.GraphMode.MODE_STANDARD_PATTERN);
            cmb_GraphMode.SelectedIndex = 0;
            cmb_CursorMode.SelectedIndex = 0;

            xZoom = m_xZoom;
            yZoom = m_yZoom;
            ZoomFactor = m_ZoomFactor;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Loads a stored theme from a file.
        /// </summary>
        /////////////////////////////////////////////////////////
        public void loadTheme()
        {
            //Open the file written above and read values from it.
            String name = Assembly.GetEntryAssembly().GetName().Name;
            String path = Environment.GetEnvironmentVariable("APPDATA") +
                   "\\" + name + "\\Waveform.thm";
            if (File.Exists(path))
            {
                Stream stream = File.Open(path, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();

                try
                {
                    m_Theme = (Theme)bformatter.Deserialize(stream);

                }
                catch (Exception)
                {
                    stream.Close();
                    File.Delete(path);
                    m_Theme = new Theme();
                }
                if(stream.CanRead)
                    stream.Close();
            }
            else
            {
                // create a default theme
                m_Theme = new Theme();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Saves a theme to a file.
        /// </summary>
        /////////////////////////////////////////////////////////
        public void saveTheme()
        {
            String name = Assembly.GetEntryAssembly().GetName().Name;
            String path = Environment.GetEnvironmentVariable("APPDATA") +
                   "\\" + name + "\\Waveform.thm";
            FileInfo fi = new FileInfo(path);
            fi.Directory.Create();
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();

            bformatter.Serialize(stream, m_Theme);
            stream.Close();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Loads all the pins with their Response and Pattern
        /// sample data.  Then loads these pins into the WaveformCtrl.
        /// This also creates the header information that will be
        /// displayed.  This is all random data.
        /// </summary>
        /////////////////////////////////////////////////////////
        public void init()
        {
            loadTheme();
            // create the grid
            twc_WaveFrom.setGraph("Test Graph", m_Theme);
            twc_WaveFrom.setTimeScale(m_TimeScale);
            // create the pin data
            Pin p = new Pin("12345678901234567890", "ADB1952", Pens.Red);
            p.setDigital(false);
            double[] buffer = sinWave(100, 80);
            for (int i = 0; i < buffer.Length; i++)
            {
                p.addResponseSample(new TimeSample(i, m_TimeScale), (float)buffer[i]);
            }
            twc_WaveFrom.addPin(p);
            //m_Pins.Add(p);

            Header cycle = new Header("Cycle");
            Header vector = new Header("Vector");
            Header tset = new Header("Tset");
            tset.showGridLines(true);
            int cycleCount = 5495;

            Random rand = new Random();
            Pin p41 = new Pin("Strobes", "ADB" + rand.Next(1000, 9999), new Pen(Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))));

            for (int i = 0; i < 100; i++)
            {
                Pen cP;
                Pen cR;
                if( i < 3)
                {
                    cP = new Pen(Color.White);
                    cR = new Pen(Color.Blue);
                }
                else
                {
                    cR = new Pen(Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)));
                    cP = new Pen(Color.FromArgb(255 - cR.Color.R, 255 - cR.Color.G, 255 - cR.Color.B));
                }
                Pin p2 = new Pin("Pin " + i, "ADB" + rand.Next(1000, 9999), cR);
                p2.setPatternColor(cP);
                p2.setDigital(true);
                buffer = squareWave();
                for (int x = 0; x < buffer.Length; x++)
                {
                    p2.addResponseSample(new TimeSample(x, m_TimeScale), (float)buffer[x]);
                    if (i == 0 && x % 10 == 0)
                    {
                        cycle.addSample(new TimeSample(x, m_TimeScale), cycleCount);
                        vector.addSample(new TimeSample(x, m_TimeScale), cycleCount - 10);
                        cycleCount += 2;
                        tset.addSample(new TimeSample(x + rand.Next(0, 40), m_TimeScale), 1);
                    }
                }

                for (int b = 0; b < 4; b++)
                {
                    Strobe st = new Strobe();
                    TimeSample ts = new TimeSample(rand.Next(1, 999), m_TimeScale);
                    st.setStartTime(ts);
                    st.setEndTime(new TimeSample(ts.getTime(TimeScale.NANOSECONDS) + rand.Next(0, 10), TimeScale.NANOSECONDS));
                    int t = rand.Next(0, 2);
                    switch(t)
                    {
                        case 0:
                            st.setStrobeState(Strobe.StrobeState.RISING);
                            break;
                        case 1:
                            st.setStrobeState(Strobe.StrobeState.FALLING);
                            break;
                        case 2:
                            st.setStrobeState(Strobe.StrobeState.BOTH);
                            break;
                    }
                    st.setStrobePassColor(Pens.YellowGreen);
                    st.setStrobeFailColor(Pens.Red);
                    float tf = rand.Next(0, 100);
                    if(tf > 50)
                        st.setFailed(false);
                    else
                        st.setFailed(true);
                    p2.addStrobe(st);
                    p41.addStrobe(st);
                }
                m_Pins.Add(p2);
            }

            for (int i = 0; i < m_Pins.Count - 1; i++)
            {
                Pin p2 = (Pin)m_Pins[i];
                buffer = squareWave();
                for (int x = 0; x < buffer.Length; x++)
                {
                    p2.addPatternSample(new TimeSample(x, m_TimeScale), (float)buffer[x]);
                }
                twc_WaveFrom.addPin(p2);
            }

            for (int x = 0; x < buffer.Length; x++)
            {
                p41.addPatternSample(new TimeSample(x, m_TimeScale), 0);
            }

            twc_WaveFrom.addPin(p41);

            // add header rows
            twc_WaveFrom.addHeaderRow(cycle);
            twc_WaveFrom.addHeaderRow(vector);
            twc_WaveFrom.addHeaderRow(tset);
        }

        public void init2()
        {
            loadTheme();
            // create the grid
            twc_WaveFrom.setGraph("Test Graph", m_Theme);

            twc_WaveFrom.setTimeScale(TimeScale.MICROSECONDS);
            // MRL CODE

            Header cycle = new Header("Cycle");
            Header vector = new Header("Vector");
            Header tset = new Header("Tset");


            tset.showGridLines(true);
            Pin rz = new Pin("RZ", "", Pens.Red);
            Pin nrz = new Pin("NRZ", "", Pens.Red);
            Pin strobe = new Pin("STROBE", "", Pens.White);
            Pin window = new Pin("WINDOW", "", Pens.White);
            rz.setDigital(true);
            nrz.setDigital(true);
            strobe.setDigital(true);
            window.setDigital(true);
            float T0 = 100f;
            float D1 = 30f;
            float D2 = 70f;
            float C2 = 50f;
            float C3 = 75f;
            float HIGH = 5.0f;
            float LOW = -5.0f;
            m_TimeScale = TimeScale.NANOSECONDS;



            for (int v = 0; v < 30; v++)
            {
                Strobe st = new Strobe();
                vector.addSample(new TimeSample(v * T0, m_TimeScale), v);
                cycle.addSample(new TimeSample(v * T0, m_TimeScale), v);
                tset.addSample(new TimeSample(v * T0, m_TimeScale), "T1");

                rz.addPatternSample(new TimeSample(v * T0 + D1, m_TimeScale), HIGH);
                rz.addPatternSample(new TimeSample(v * T0 + D2, m_TimeScale), LOW);

                nrz.addPatternSample(new TimeSample(v * T0 + D1, m_TimeScale), HIGH);

                st.setStartTime(new TimeSample(v * T0 + C3, m_TimeScale));
                st.setStrobeState(Strobe.StrobeState.RISING);
                st.setStrobeColor(Pens.Black);
                strobe.addStrobe(st);

                st = new Strobe();
                st.setStartTime(new TimeSample(v * T0 + C2, m_TimeScale));
                st.setEndTime(new TimeSample(v * T0 + C3, m_TimeScale));
                st.setStrobeState(Strobe.StrobeState.RISING);
                st.setStrobeColor(Pens.Black);
                window.addStrobe(st);
                v++;

                vector.addSample(new TimeSample(v * T0, m_TimeScale), v);
                cycle.addSample(new TimeSample(v * T0, m_TimeScale), v);
                tset.addSample(new TimeSample(v * T0, m_TimeScale), "T1");

                rz.addPatternSample(new TimeSample(v * T0 + D1, m_TimeScale), LOW);
                rz.addPatternSample(new TimeSample(v * T0 + D2, m_TimeScale), LOW);

                nrz.addPatternSample(new TimeSample(v * T0 + D1, m_TimeScale), LOW);

                st = new Strobe();
                st.setStartTime(new TimeSample(v * T0 + C3, m_TimeScale));
                st.setStrobeState(Strobe.StrobeState.FALLING);
                st.setStrobeColor(Pens.White);
                strobe.addStrobe(st);

                st = new Strobe();
                st.setStartTime(new TimeSample(v * T0 + C2, m_TimeScale));
                st.setEndTime(new TimeSample(v * T0 + C3, m_TimeScale));
                st.setStrobeState(Strobe.StrobeState.FALLING);
                st.setStrobeColor(Pens.White);
                window.addStrobe(st);

            }
            twc_WaveFrom.addPin(rz);
            twc_WaveFrom.addPin(nrz);
            twc_WaveFrom.addPin(strobe);
            twc_WaveFrom.addPin(window);

            // add header rows
            twc_WaveFrom.addHeaderRow(cycle);
            twc_WaveFrom.addHeaderRow(vector);
            twc_WaveFrom.addHeaderRow(tset);

            xZoom = 2;
            twc_WaveFrom.setxScale(xZoom);

            return;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// A function to generate a square wave with random data.
        /// </summary>
        /// 
        /// <returns>Returns sample points that generate a square
        /// wave.</returns>
        /////////////////////////////////////////////////////////
        public double[] squareWave()
        {
            Thread.Sleep(15);
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numSamples = 5000;
            double[] buffer = new double[numSamples];
            for (int i = 0; i < numSamples; i++)
            {
                int t = rand.Next(0, 3);
                float value = rand.Next(-5, 5);
                switch (t)
                {
                    case 0:
                        value = -5;
                        break;
                    case 1:
                        value = 0;
                        break;
                    case 2:
                        value = 5;
                        break;
                }
                buffer[i] = (double)value;
            }
            return buffer;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Takes the provided amp and frequency and generates a
        /// sin wave.
        /// </summary>
        /// 
        /// <param name="amp">The amperage to generate the sin
        /// wave.</param>
        /// <param name="freq">The frequency to generate the sin
        /// wave.</param>
        /// 
        /// <returns>Returns sample points that represent the
        /// sin wave.</returns>
        /////////////////////////////////////////////////////////
        public double[] sinWave(int amp, float freq)
        {
            float sample_rate = 8000;
            float number_of_samples = 500;

            double[] buffer = new double[(int)number_of_samples];
            double amplitude = 0.25 * amp;
            for (int sample_number = 0; sample_number < number_of_samples; sample_number++)
            {
                float time_in_seconds = sample_number / sample_rate;
                buffer[sample_number] = amplitude * Math.Sin(2.0 * Math.PI * freq * time_in_seconds);
            }
            return buffer;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Increments the x only zoom in by 5 units.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_ZoomIn_Click(object sender, EventArgs e)
        {
            btn_xZoomIn_Click(sender, e);
            btn_yZoomIn_Click(sender, e);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Increments the x/y zoom out by 5 units.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_ZoomOut_Click(object sender, EventArgs e)
        {
            btn_xZoomOut_Click(sender, e);
            btn_yZoomOut_Click(sender, e);
        }

        protected void setStepping()
        {
            // it's close enough
            float a = 0 - 0.20149f;
            float b = 0 - 5.36269f;
            float c = 10.0f;
            float y = a * xZoom * xZoom + b * xZoom + c;
            twc_WaveFrom.setGraphStepping((int)y);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Increments the x only zoom in by 5 units.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_xZoomIn_Click(object sender, EventArgs e)
        {
            xZoom += ZoomFactor;
            if (twc_WaveFrom.setxScale(xZoom) == -1)
            {
                xZoom -= ZoomFactor;
                xZoom *= 10f;
                xZoom = ((int)xZoom) / 10f;
            }
            setStepping();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Increments the x only zoom out by 5 units.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_xZoomOut_Click(object sender, EventArgs e)
        {
            xZoom -= ZoomFactor;
            if (twc_WaveFrom.setxScale(xZoom) == -1)
            {
                xZoom += ZoomFactor;
                xZoom *= 10f;
                xZoom = ((int)xZoom) / 10f;
            }
            setStepping();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Increments the x only zoom in by 5 units.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void tsm_ZoomIn_Click(object sender, EventArgs e)
        {
            xZoom += 5;
            if (twc_WaveFrom.setxScale(xZoom) == -1)
            {
                xZoom -= 5;
            }
            cms_RightClick.Hide();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Increments the x/y zoom out by 5 units.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void tsm_ZoomOut_Click(object sender, EventArgs e)
        {
            xZoom -= 5;
            if (twc_WaveFrom.setxScale(xZoom) == -1)
            {
                xZoom += 5;
            }
            cms_RightClick.Hide();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Displays the Theme Editor dialog.  This allows the
        /// user to change the theme settings.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_EditTheme_Click(object sender, EventArgs e)
        {
            ThemeEditor dlg = new ThemeEditor(m_Theme);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_Theme = dlg.getTheme();
                twc_WaveFrom.setTheme(m_Theme);
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Shows a dialog with all the pins in it.  This dialog
        /// allows the user to change pin settings.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        ///////////////////////////////////////////////////////// 
        private void btn_Pins_Click(object sender, EventArgs e)
        {
            // Launch a dialog with the pins in it.
            Pin[] pins = new Pin[m_Pins.Count];
            m_Pins.CopyTo(pins);
            PinsDlg dlg = new PinsDlg(pins);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_Pins.Clear();
                m_Pins.AddRange(dlg.getPins());
                twc_WaveFrom.Invalidate();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Clears the cursors from the graph.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        ///////////////////////////////////////////////////////// 
        private void btn_ClearCursors_Click(object sender, EventArgs e)
        {
            twc_WaveFrom.clearCursors();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Changes the view of the graph to the selected mode
        /// in the cmb_GraphMode.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void cmb_GraphMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_GraphMode.SelectedItem.ToString())
            {
                case "Standard - Pattern":
                    twc_WaveFrom.setGraphMode(TEV_WaveformControl.TEV_WaveformControl.GraphMode.MODE_STANDARD_PATTERN);
                    break;
                case "Standard - Response":
                    twc_WaveFrom.setGraphMode(TEV_WaveformControl.TEV_WaveformControl.GraphMode.MODE_STANDARD_RESPONSE);
                    break;
                case "Split":
                    twc_WaveFrom.setGraphMode(TEV_WaveformControl.TEV_WaveformControl.GraphMode.MODE_SPLIT);
                    break;
                case "Overlay":
                    twc_WaveFrom.setGraphMode(TEV_WaveformControl.TEV_WaveformControl.GraphMode.MODE_OVERLAY);
                    break;
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Makes the TEV_WaveformCtrl zoom to the cursors.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_ZoomToCursors_Click(object sender, EventArgs e)
        {
            twc_WaveFrom.zoomToCursors();
            xZoom = twc_WaveFrom.getxScale();
            setStepping();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets which type of cursors can be used.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void cmb_CursorMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_CursorMode.SelectedItem.ToString())
            {
                case "Off":
                    twc_WaveFrom.setCursorMode(TEV_WaveformControl.TEV_WaveformControl.CursorMode.OFF);
                    break;
                case "Vertical":
                    twc_WaveFrom.setCursorMode(TEV_WaveformControl.TEV_WaveformControl.CursorMode.LEFT_RIGHT);
                    break;
                case "Horizontal":
                    twc_WaveFrom.setCursorMode(TEV_WaveformControl.TEV_WaveformControl.CursorMode.TOP_BOTTOM);
                    break;
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Changes whether or not the strobes will be shown.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void chb_ShowStrobes_CheckedChanged(object sender, EventArgs e)
        {
            twc_WaveFrom.showStrobes(chb_ShowStrobes.Checked);
            if (chb_ShowStrobes.Checked == false)
                chb_ShowStrobeFails.Checked = false;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Changes whether or not the strobe fail state will be
        /// shown.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void chb_ShowStrobeFails_CheckedChanged(object sender, EventArgs e)
        {
            twc_WaveFrom.showStrobeFailState(chb_ShowStrobeFails.Checked);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the x offset for the overlay view mode.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void spnr_xOverlayOffset_ValueChanged(object sender, EventArgs e)
        {
            twc_WaveFrom.setOverlayOffset(new Point((int)spnr_xOverlayOffset.Value, (int)spnr_yOverlayOffset.Value));
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Updates the y offset for the overlay view mode.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void spnr_yOverlayOffset_ValueChanged(object sender, EventArgs e)
        {
            twc_WaveFrom.setOverlayOffset(new Point((int)spnr_xOverlayOffset.Value, (int)spnr_yOverlayOffset.Value));
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// When the form closes, the theme will be saved.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.saveTheme();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Changes whether or not the mouse measurement will be
        /// shown.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void cmb_ShowMeasurement_CheckedChanged(object sender, EventArgs e)
        {
            twc_WaveFrom.setTransitionSpan(cmb_ShowMeasurement.Checked);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Increments the y only zoom in by 0.1 units.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_yZoomIn_Click(object sender, EventArgs e)
        {
            yZoom += ZoomFactor;
            if (twc_WaveFrom.setyScale(yZoom) == -1)
            {
                yZoom -= ZoomFactor;
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Decrements the y only zoom in by 0.1 units.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void btn_yZoomOut_Click(object sender, EventArgs e)
        {
            yZoom -= ZoomFactor;
            if (twc_WaveFrom.setyScale(yZoom) == -1)
            {
                yZoom += ZoomFactor;
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void wfc_Waveform_MouseUp(object sender, MouseEventArgs e)
        {
            lbl_CurrentSelection.Text = "Selection:";
            if(twc_WaveFrom.getSelectedPin() != null)
                lbl_CurrentSelection.Text = "Pin: " + twc_WaveFrom.getSelectedPin().getName();
            lbl_CurrentSelection.Text += "    Volts: " + twc_WaveFrom.getSelectedVolts();
            if(twc_WaveFrom.getSelectedTime() != null)
                lbl_CurrentSelection.Text += "    Time: " + twc_WaveFrom.getSelectedTime().getTime(m_TimeScale);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void wfc_Waveform_MouseDown(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine("form mouse down");
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Test function to show when the TEV_WaveFormControl is
        /// clicked on by the mouse.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void wfc_Waveform_MouseClick(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine("form mouse click");
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets called when a the selected pin changes.  When this
        /// occurres it updates the status label with the selected
        /// pin name.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void wfc_Waveform_SelectedPinChanged(object sender, EventArgs e)
        {
            lbl_CurrentSelection.Text = "Selection:";
            if (twc_WaveFrom.getSelectedPin() != null)
                lbl_CurrentSelection.Text = "Pin: " + twc_WaveFrom.getSelectedPin().getName();
        }


        private void mi_FileOpen_Click(object sender, EventArgs e)
        {
            FileOpenDlg dlg = new FileOpenDlg();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                twc_WaveFrom.addPin(dlg.m_Pin);
            }
        }

        private void mi_FileNew_Click(object sender, EventArgs e)
        {
            twc_WaveFrom.clearPins();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            //this.m_HasChanged = true;
        }

        private void txb_XZoom_Leave(object sender, EventArgs e)
        {

        }

        private void txb_XZoom_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txb_ZoomFactor_TextChanged(object sender, EventArgs e)
        {
            m_ZoomFactor = 0;
            float.TryParse(txb_ZoomFactor.Text, out m_ZoomFactor);
        }
    }
}