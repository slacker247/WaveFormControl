using System;
using System.Collections.Specialized;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace TEV_WaveformControl
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// Contains all the information to draw the pin
    /// data on the TEV_WaveformCtrl.
    /// 
    /// If the pin is set to digital, the high/low threshold
    /// values must be set.  Otherwise they will be defaulted
    /// to 2.7/2.3 respectively.</summary>
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
    public class Pin : INotifyPropertyChanged
    {

        /////////////////////////////////////////////////////////
        /// <summary>The reference name of the pin.  This will be
        /// displayed to the user.</summary>
        /////////////////////////////////////////////////////////
        protected String m_Name;
        /////////////////////////////////////////////////////////
        /// <summary>The id assigned to the pin.  This will be
        /// displayed to the user.</summary>
        /////////////////////////////////////////////////////////
        protected String m_ID;
        /////////////////////////////////////////////////////////
        /// <summary>The color of the response signal line that
        /// will be displayed to the user.  The response is the
        /// captured signal from the line</summary>
        /////////////////////////////////////////////////////////
        protected Pen m_ResponseColor; // Line color
        /////////////////////////////////////////////////////////
        /// <summary>The color of the pattern signal line that
        /// will be displayed to the user.  The pattern is
        /// the signal that is sent on the line.</summary>
        /////////////////////////////////////////////////////////
        protected Pen m_PatternColor; // Line color
        /////////////////////////////////////////////////////////
        /// <summary>The color of the strobe indicators that will
        /// be displayed to the user.</summary>
        /////////////////////////////////////////////////////////
        protected Pen m_StrobeColor; // Strobe color.
        /////////////////////////////////////////////////////////
        /// <summary>Indicates whether or not this pin is a digital
        /// signal (true) or an analog signal (false).</summary>
        /////////////////////////////////////////////////////////
        protected bool m_Digital;
        /////////////////////////////////////////////////////////
        /// <summary>This is a map of <see ref="TimeScale"/> objects
        /// and a float value that represent the response on the
        /// given pin. The TimeScale is when the measurement occurred
        /// and the float value is the value in volts/amps of the
        /// measurement.</summary>
        /////////////////////////////////////////////////////////
        protected OrderedDictionary m_ResponseSamples;
        /////////////////////////////////////////////////////////
        /// <summary>This is a map of <see ref="TimeScale"/> objects
        /// and a float value that represent the pattern on the
        /// given pin. The TimeScale is when the measurement occurred
        /// and the float value is the value in volts/amps of the
        /// measurement.</summary>
        /////////////////////////////////////////////////////////
        protected OrderedDictionary m_PatternSamples;
        /////////////////////////////////////////////////////////
        /// <summary>strobe a marker indicating what the state of
        /// the line should be, either rising, falling, or both.</summary>
        /////////////////////////////////////////////////////////
        protected OrderedDictionary m_Strobes;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Contains the low voltage value threshold to indicate
        /// a digital low.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected float m_Low = 0 - 3.25f;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Contains the high voltage value threshold to indicate
        /// a digital high.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected float m_High = 3.25f;
        protected TimeSample m_MaxTime = new TimeSample(0, TimeScale.MICROSECONDS);

        /////////////////////////////////////////////////////////
        /// <summary>Keeps track of the maxium voltaget reached in
        /// the sample values.</summary>
        /////////////////////////////////////////////////////////
        protected float m_MaxVolt;

        /////////////////////////////////////////////////////////
        /// <summary>Constructor: Creates a pin with the given
        /// values.</summary>
        ///
        /// <param name="name">A String representing the name of
        /// the pin.</param>
        /// <param name="id">A String representing the id of the
        /// pin.</param>
        /// <param name="c">A <see cref="Pen"/> representing the
        /// response color of the pin. By default this sets the
        /// pattern color as well.</param>
        ///
        /// <returns>Returns an instance of the class.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pin(String name, String id, Pen c)
        {
            m_StrobeColor = Pens.YellowGreen;
            m_MaxVolt = -100;
            init();
            setName(name);
            setID(id);
            setResponseColor(c);
            setPatternColor(c);
        }

        /////////////////////////////////////////////////////////
        /// <summary>Initializes the needed variables.</summary>
        /////////////////////////////////////////////////////////
        public void init()
        {
            m_ResponseSamples = new OrderedDictionary();
            m_PatternSamples = new OrderedDictionary();
            m_Strobes = new OrderedDictionary();
            m_ResponseColor = Pens.Red;
            m_PatternColor = Pens.Green;
        }

        #region Properties
        /////////////////////////////////////////////////////////
        /// <summary>The name of the pin.</summary>
        ///
        /// <returns>Returns a String representing the name of the
        /// pin.
        /// </returns>
        /////////////////////////////////////////////////////////
        public String Name
        {
            get
            {
                return getName();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>The ID of the pin</summary>
        ///
        /// <returns>Returns a String representing the ID of the
        /// pin.
        /// </returns>
        /////////////////////////////////////////////////////////
        public String ID
        {
            get
            {
                return getID();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>The color that will be used to display the
        /// pin's response signal.</summary>
        ///
        /// <returns>Returns a Pen instance containing the color
        /// to be used to draw the signal.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen ResponseColor // Line color
        {
            get
            {
                return getResponseColor();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>The color that will be used to display the
        /// pin's pattern signal.</summary>
        ///
        /// <returns>Returns a Pen instance containing the color
        /// to be used to draw the signal.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen PatternColor // Line color
        {
            get
            {
                return getPatternColor();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>The color used to display the strobes.</summary>
        ///
        /// <returns>Returns a Pen instance containing the color
        /// to be used to draw a strobe.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen StrobeColor // Strobe color.
        {
            get
            {
                return getStrobeColor();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>Indicates whether or not this pin is a digital
        /// signal</summary>
        ///
        /// <returns>Returns a bool where true indicates that this
        /// is a digital pin and false indicates that it is an
        /// analog pin.
        /// </returns>
        /////////////////////////////////////////////////////////
        public bool Digital
        {
            get
            {
                return isDigital();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>A map containing all of the samples for this
        /// pin.  Where the key is the TimeSample and the value is
        /// the measured value in Volts/Amps.</summary>
        ///
        /// <returns>Returns an OrderedDictionary consisting of
        /// the key being the TimeSample and the value being the
        /// measured value in Volts/Amps.  If the samples have not
        /// been set then this will return null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public OrderedDictionary PatternSamples
        {
            get
            {
                return getPatternSamples();
            }
        }
        /////////////////////////////////////////////////////////
        /// <summary>A map containing all of the samples for this
        /// pin.  Where the key is the TimeSample and the value is
        /// the measured value in Volts/Amps.</summary>
        ///
        /// <returns>Returns an OrderedDictionary consisting of
        /// the key being the TimeSample and the value being the
        /// measured value in Volts/Amps.  If the samples have not
        /// been set then this will return null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public OrderedDictionary ResponseSamples
        {
            get
            {
                return getResponseSamples();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>A map containing all the strobes for this pin.
        /// Where the key is the TimeSample and the value indicates
        /// that the signal should be Rising/Falling/Both.
        /// </summary>
        ///
        /// <returns>Returns an OrderedDictionary consisting of
        /// the key being the TimeSample and the value being the
        /// Rising/Falling/Both constants.  If the samples have not
        /// been set then this will return null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public OrderedDictionary Strobes
        {
            get
            {
                return getStrobes();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>This is the maximum voltage contained within
        /// the samples.</summary>
        ///
        /// <returns>Returns a float representing the maximum
        /// voltage contained within the samples.
        /// </returns>
        /////////////////////////////////////////////////////////
        public float MaxVolt
        {
            get
            {
                return getMaxVolt();
            }
        } 
        #endregion

        /////////////////////////////////////////////////////////
        /// <summary>This is the maximum voltage contained within
        /// the samples.</summary>
        ///
        /// <returns>Returns a float representing the maximum
        /// voltage contained within the samples.
        /// </returns>
        /////////////////////////////////////////////////////////
        public float getMaxVolt()
        {
            return m_MaxVolt;
        }

        public TimeSample getMaxTime()
        {
            return m_MaxTime;
        }

        /////////////////////////////////////////////////////////
        /// <summary>The name of the pin.</summary>
        ///
        /// <returns>Returns a String representing the name of the
        /// pin.
        /// </returns>
        /////////////////////////////////////////////////////////
        public String getName()
        {
            return m_Name;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Denotes whether this pin has digital data or
        /// analog data.</summary>
        ///
        /// <param name="digital">A bool where true indicates
        /// that this pin is digital and false indicates that it
        /// is analog.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.  Note: currently
        /// not implemented.  Always returns -1.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setDigital(bool digital)
        {
            int status = -1;
            m_Digital = digital;
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Denotes whether this pin has digital data or
        /// analog data.</summary>
        ///
        /// <returns>Returns True indicating that this pin is digital
        /// and false indicating that it is analog.
        /// </returns>
        /////////////////////////////////////////////////////////
        public bool isDigital()
        {
            return m_Digital;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the given values to the threshold values.  These
        /// threshold values are what determin the low/mid/high
        /// digital value.
        /// </summary>
        /// 
        /// <param name="high">A float to denote when the signal
        /// is considered to be a digital high.</param>
        /// <param name="low">A float to denote when the signal
        /// is considered to be a digital low.</param>
        /// 
        /// <returns>Returns 0 for success and a -1 if the high
        /// value is less than the low value.</returns>
        /////////////////////////////////////////////////////////
        public int setThresholds(float high, float low)
        {
            int status = -1;
            if (high > low)
            {
                m_High = high;
                m_Low = low;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Converts an analog voltage value into a digital value
        /// based on the thresholds set.
        /// </summary>
        /// 
        /// <param name="value">An analog voltage value.</param>
        /// 
        /// <returns>Returns a digital voltage value.</returns>
        /////////////////////////////////////////////////////////
        protected float getDigitalValue(float value)
        {
            float diff = m_High - m_Low;

            if (value <= m_Low)
                value = 0 - getMaxVolt();
            else if (value < m_High &&
                     value > m_Low)
                value = 0;
            else if (value >= m_High)
                value = getMaxVolt();
            return value;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the label for the pin.</summary>
        ///
        /// <param name="name">A String representing a label for the
        /// pin.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setName(String name)
        {
            int status = -1;
            if (name.Length > 0)
            {
                m_Name = name;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>The ID of the pin</summary>
        ///
        /// <returns>Returns a String representing the ID of the
        /// pin.
        /// </returns>
        /////////////////////////////////////////////////////////
        public String getID()
        {
            return m_ID;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the id to the given value.</summary>
        ///
        /// <param name="id">A String representing the pin's id.  This
        /// is less than or equal to 8 characters.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setID(String id)
        {
            int status = -1;
            if (id.Length <= 7)
            {
                m_ID = id;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Gets the color that will be used to display
        /// the response signal line.</summary>
        ///
        /// <returns>Returns an instance of Pen that contains the
        /// color.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen getResponseColor()
        {
            return m_ResponseColor;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the color that will be used to display
        /// the response signal line.</summary>
        ///
        /// <param name="color">A <see cref="Pen"/> instance that
        /// contains the color.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurres.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setResponseColor(Pen color)
        {
            int status = -1;
            if (color != null)
            {
                m_ResponseColor = color;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Gets the color that will be used to display
        /// the pattern signal line.</summary>
        ///
        /// <returns>Returns an instance of Pen that contains the
        /// color.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen getPatternColor()
        {
            return m_PatternColor;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the color that will be used to display
        /// the pattern signal line.</summary>
        ///
        /// <param name="color">A <see cref="Pen"/> instance that
        /// contains the color.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurres.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setPatternColor(Pen color)
        {
            int status = -1;
            if (color != null)
            {
                m_PatternColor = color;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Adds a sample to the pin.  A sampe consists
        /// of a time in which it occurred, usually a measurement
        /// in microseconds.  Then the value, which is usually
        /// a plus or minus value representing the volts/amps.</summary>
        ///
        /// <param name="time">A <see cref="TimeScale"/> representing
        /// time in which the measurement occurred, usually in
        /// microseconds.</param>
        /// <param name="value">a plus or minus float value
        /// representing the volts/amps.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int addResponseSample(TimeSample time, float value)
        {
            int status = -1;
            if (!m_ResponseSamples.Contains(time))
            {
                if (Math.Abs(value) > m_MaxVolt)
                    m_MaxVolt = Math.Abs(value);
                if (time > m_MaxTime)
                    m_MaxTime = time;
                m_ResponseSamples.Add(time, value);
                status = 0;
            }
            return status;
            // TODO : Change this to an ordered linked list with TimeSample requesting for the value.
        }

        /////////////////////////////////////////////////////////
        /// <summary>Adds a sample to the pin.  A sampe consists
        /// of a time in which it occurred, usually a measurement
        /// in microseconds.  Then the value, which is usually
        /// a plus or minus value representing the volts/amps.</summary>
        ///
        /// <param name="time">A <see cref="TimeSample"/> representing
        /// time in which the measurement occurred, usually in
        /// microseconds.</param>
        /// <param name="value">A plus or minus float value
        /// representing the volts/amps.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int addPatternSample(TimeSample time, float value)
        {
            int status = -1;
            if (!m_PatternSamples.Contains(time))
            {
                if (Math.Abs(value) > m_MaxVolt)
                    m_MaxVolt = Math.Abs(value);
                if (time > m_MaxTime)
                    m_MaxTime = time;
                m_PatternSamples.Add(time, value);
                status = 0;
            }
            return status;
            // TODO : Change this to an ordered linked list with TimeSample requesting for the value.
        }

        /////////////////////////////////////////////////////////
        /// <summary>A map containing all of the samples for this
        /// pin.  Where the key is the TimeSample and the value is
        /// the measured value in Volts/Amps.</summary>
        ///
        /// <returns>Returns an OrderedDictionary consisting of
        /// the key being the TimeSample and the value being the
        /// measured value in Volts/Amps.  If the samples have not
        /// been set then this will return null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public OrderedDictionary getResponseSamples()
        {
            return m_ResponseSamples;
        }

        /////////////////////////////////////////////////////////
        /// <summary>A map containing all of the samples for this
        /// pin.  Where the key is the TimeSample and the value is
        /// the measured value in Volts/Amps.</summary>
        ///
        /// <returns>Returns an OrderedDictionary consisting of
        /// the key being the TimeSample and the value being the
        /// measured value in Volts/Amps.  If the samples have not
        /// been set then this will return null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public OrderedDictionary getPatternSamples()
        {
            return m_PatternSamples;
        }

        // TODO : implement a get Samples based on a passed in value.
        private void getSamples()
        {
            /// ???
        }

        /////////////////////////////////////////////////////////
        /// <summary>Adds a strobe to the pin at the given time
        /// span and with one of the following constants:
        /// RISING, FALLING, BOTH.</summary>
        ///
        /// <param name="st">A <see cref="Strobe"/> representing
        /// a strobe marker.
        /// </param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if the strobe already exists.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int addStrobe(Strobe st)
        {
            int status = -1;
            if (!m_Strobes.Contains(st.getStartTime()))
            {
                m_Strobes.Add(st.getStartTime(), st);
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>A map containing all the strobes for this pin.
        /// Where the key is the <see cref="TimeSample"/> and the
        /// value indicates that the signal should be
        /// Rising/Falling/Both.</summary>
        ///
        /// <returns>Returns an OrderedDictionary consisting of
        /// the key being the <see cref="TimeSample"/> and the value
        /// being the Rising/Falling/Both constants.  If the samples
        /// have not been set then this will return null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public OrderedDictionary getStrobes()
        {
            return m_Strobes;
        }

        /////////////////////////////////////////////////////////
        /// <summary>The color used to display the strobes.</summary>
        ///
        /// <returns>Returns a Pen instance containing the color
        /// to be used to draw a strobe.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen getStrobeColor()
        {
            return m_StrobeColor;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the color that will be used to display
        /// the strobes.</summary>
        ///
        /// <param name="pen">A <see cref="Pen"/> instance
        /// containing the color.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurres.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setStrobeColor(Pen pen)
        {
            int status = -1;
            if (pen != null)
            {
                m_StrobeColor = pen;
                status = 0;
            }
            return status;
        }

        #region INotifyPropertyChanged Members

        /////////////////////////////////////////////////////////
        /// Variable: PropertyChanged
        ///
        /// <summary>Represents the method that will handle the
        /// PropertyChanged event raised when a property is changed
        /// on a component.</summary>
        /////////////////////////////////////////////////////////
        public event PropertyChangedEventHandler PropertyChanged;

        /////////////////////////////////////////////////////////
        /// Function: name
        ///
        /// <summary>The function is used to notify clients,
        /// typically binding clients, that a property value has
        /// changed.</summary>
        ///
        /// <param>name: The reference name associated with the
        /// member variable.</param>
        /////////////////////////////////////////////////////////
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the pins name and id to an image.
        /// </summary>
        /// 
        /// <param name="t">The <see cref="Theme"/> to use to
        /// draw the label information.</param>
        /// <param name="selected">Denotes whether or not this
        /// pin is selected.  If it is selected the background
        /// color will be set to the themes selected color value.
        /// </param>
        /// <param name="mat">A matrix containing the current
        /// scaling.</param>
        /// 
        /// <returns>Returns an image with the rendered label
        /// information on it.</returns>
        /////////////////////////////////////////////////////////
        public Image drawLabel(Theme t, bool selected, Matrix mat)
        {
            Bitmap img = new Bitmap(170, (int)(t.getFont().Size * 2));
            Graphics gp = Graphics.FromImage(img);
            if (selected)
                gp.FillRectangle(t.getPinSelectionColor(), new Rectangle(0, 0, img.Width, img.Height));
            gp.Transform = mat;

            //gp.MeasureString("Length of 30 chars", t.getFont());

            String pinName = getName();
            if (pinName.Length > 17)
            {
                pinName = pinName.Substring(0, 17);
                pinName += "...";
            }
            int x = 1;
            int y = 1;
            gp.DrawString(pinName, t.getFont(), t.getFontColor(), x, y);
            if (getID().Length > 0)
            {
                String id = "[" + getID() + "]";
                gp.DrawString(id,
                              t.getFont(),
                              t.getFontColor(),
                              img.Width - gp.MeasureString(id, t.getFont()).Width,
                              y);
            }
            gp.Flush();
            return img;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the pattern data for the pin.
        /// </summary>
        /// 
        /// <param name="startTime">A <see cref="TimeSample"/> that
        /// represents the start of the draw window.</param>
        /// <param name="endTime">A <see cref="TimeSample"/> that
        /// represents the end of the draw window.</param>
        /// <param name="timeScale">A <see cref="TimeScale"/> that
        /// represents the unit of measure for them time.</param>
        /// <param name="mat">A matrix containing the current
        /// scaling.</param>
        /// 
        /// <returns>Returns an image with the rendered signal
        /// data.</returns>
        /////////////////////////////////////////////////////////
        public Image drawPattern(TimeSample startTime, TimeSample endTime, TimeScale timeScale, Matrix mat)
        {
            return draw(startTime, endTime, timeScale, 1, mat);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the response data for the pin.
        /// </summary>
        /// 
        /// <param name="startTime">A <see cref="TimeSample"/> that
        /// represents the start of the draw window.</param>
        /// <param name="endTime">A <see cref="TimeSample"/> that
        /// represents the end of the draw window.</param>
        /// <param name="timeScale">A <see cref="TimeScale"/> that
        /// represents the unit of measure for them time.</param>
        /// <param name="mat">A matrix containing the current
        /// scaling.</param>
        /// 
        /// <returns>Returns an image with the rendered signal
        /// data.</returns>
        /////////////////////////////////////////////////////////
        public Image drawResponse(TimeSample startTime, TimeSample endTime, TimeScale timeScale, Matrix mat)
        {
            return draw(startTime, endTime, timeScale, 2, mat);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the strobes for this pin to the image.
        /// </summary>
        /// 
        /// <param name="theme">The <see cref="Theme"/> to use when
        /// drawing the strobe information.</param>
        /// <param name="startTime">A <see cref="TimeSample"/> that
        /// represents the start of the draw window.</param>
        /// <param name="endTime">A <see cref="TimeSample"/> that
        /// represents the end of the draw window.</param>
        /// <param name="timeScale">A <see cref="TimeScale"/> that
        /// represents the unit of measure for them time.</param>
        /// <param name="drawFailFlag">Denotes whether or not the
        /// fail flag should be drawn.</param>
        /// <param name="mat">A matrix containing the current
        /// scaling.</param>
        /// 
        /// <returns>Returns an image with the rendered strobe
        /// information.</returns>
        /////////////////////////////////////////////////////////
        public Image drawStrobes(Theme theme, TimeSample startTime, TimeSample endTime, TimeScale timeScale, bool drawFailFlag, Matrix mat)
        {
            Bitmap img = null;
            if (startTime != null ||
                endTime != null)
            {
                Pen m_DashedPen = null;
                float[] dashValues = { 3f, 3f };
                m_DashedPen = new Pen(Color.FromArgb(255, 255, 255));
                m_DashedPen.DashPattern = dashValues;
                try
                {
                    TimeSample ts = endTime - startTime;
                    PointF[] pts = { new PointF(ts.getTime(timeScale) * 10, 120) };
                    if (pts[0].X < 1)
                        pts[0].X = 10;
                    if (pts[0].Y < 1)
                        pts[0].Y = 10; 
                    mat.TransformPoints(pts);
                    img = new Bitmap((int)(pts[0].X), (int)((pts[0].Y + 10)));
                }
                catch (Exception e)
                {
                    // Do to garbage collection this catches an out of memory error.
                    System.Diagnostics.Debug.WriteLine("Pin - DrawStrobes: " + e.Message);
                }
                Graphics gp = Graphics.FromImage(img);
                Matrix mm1 = mat.Clone();
                mm1.Translate((-1) * startTime.getTime(timeScale) * 10, 0);
                //gp.Transform = mm1;

                foreach (TimeSample ts in m_Strobes.Keys)
                {
                    Strobe st = (Strobe)(m_Strobes[ts]);
                    if (st.getEndTime() < startTime)
                        continue;
                    if (st.getEndTime() > endTime)
                        continue;
                    // FIXED : Arrow heads not quite right.
                    Pen strobeColor = st.getStrobePassColor();
                    if (st.isFailed())
                        strobeColor = st.getStrobeFailColor();
                    float firstX = (ts.getTime(timeScale) * 10);
                    float secondX = -1;
                    int arrowHead = 3;
                    int y = 70;
                    secondX = (st.getEndTime().getTime(timeScale) * 10);
                    PointF[] lines = {new PointF(firstX, 10), // lineTop
                                     new PointF(firstX, y), // lineBottom
                                     new PointF(secondX, 10), // lineTop2
                                     new PointF(secondX, y)}; // lineBottom2
                    mm1.TransformPoints(lines);
                    gp.DrawLine(strobeColor,
                            lines[0],
                            lines[1]);
                    if (st.getEndTime() != ts)
                    {
                        gp.DrawLine(strobeColor,
                                lines[2],
                                lines[3]);

                        Color temp34 = m_DashedPen.Color;
                        m_DashedPen.Color = strobeColor.Color;
                        gp.DrawLine(m_DashedPen, lines[0], lines[2]);
                        gp.DrawLine(m_DashedPen, lines[1], lines[3]);
                        m_DashedPen.Color = temp34;
                    }

                    PointF[] arrows = {new PointF(firstX, 10), // up first
                                      new PointF(firstX, y), // down first
                                      new PointF(secondX, 10), // up second
                                      new PointF(secondX, y)}; // down second
                    mm1.TransformPoints(arrows);
                    switch (st.getStrobeState())
                    {
                            // TODO : add a case for the MIDBAND
                        case Strobe.StrobeState.BOTH:
                            PointF[] arrowsF = {new PointF((firstX), y - 4),
                                                 new PointF(firstX + arrowHead, 10 + 4)};
                            mm1.TransformPoints(arrowsF);
                            gp.DrawLine(strobeColor,
                                    new PointF(arrowsF[0].X + arrowHead, arrowsF[0].Y),
                                    arrows[1]);
                            gp.DrawLine(strobeColor,
                                    new PointF(arrowsF[0].X - arrowHead, arrowsF[0].Y),
                                    arrows[1]);
                            gp.DrawLine(strobeColor,
                                    new PointF(arrowsF[1].X + arrowHead, arrowsF[1].Y),
                                    arrows[0]);
                            gp.DrawLine(strobeColor,
                                    new PointF(arrowsF[1].X - arrowHead, arrowsF[1].Y),
                                    arrows[0]);
                            if (st.getEndTime() != ts)
                            {
                                PointF[] arrowsS = {new PointF(secondX, y - 4),
                                                   new PointF(secondX, 10 + 4)};
                                mm1.TransformPoints(arrowsS);
                                gp.DrawLine(strobeColor,
                                        new PointF(arrowsS[0].X + arrowHead, arrowsS[0].Y),
                                        arrows[3]);
                                gp.DrawLine(strobeColor,
                                        new PointF(arrowsS[0].X - arrowHead, arrowsS[0].Y),
                                        arrows[3]);
                                gp.DrawLine(strobeColor,
                                        new PointF(arrowsS[1].X + arrowHead, arrowsS[1].Y),
                                        arrows[2]);
                                gp.DrawLine(strobeColor,
                                        new PointF(arrowsS[1].X - arrowHead, arrowsS[1].Y),
                                        arrows[2]);
                            }
                            break;
                        case Strobe.StrobeState.RISING:
                            PointF[] arrowsF2 = { new PointF(firstX, 10 + 4)};
                            mm1.TransformPoints(arrowsF2);
                            gp.DrawLine(strobeColor,
                                    new PointF(arrowsF2[0].X + arrowHead, arrowsF2[0].Y),
                                    arrows[0]);
                            gp.DrawLine(strobeColor,
                                    new PointF(arrowsF2[0].X - arrowHead, arrowsF2[0].Y),
                                    arrows[0]);
                            if (st.getEndTime() != ts)
                            {
                                PointF[] arrowsS2 = { new PointF(secondX, 10 + 4)};
                                mm1.TransformPoints(arrowsS2);
                                gp.DrawLine(strobeColor,
                                        new PointF(arrowsS2[0].X + arrowHead, arrowsS2[0].Y),
                                        arrows[2]);
                                gp.DrawLine(strobeColor,
                                        new PointF(arrowsS2[0].X - arrowHead, arrowsS2[0].Y),
                                        arrows[2]);
                            }
                            break;
                        case Strobe.StrobeState.FALLING:
                            PointF[] arrowsF3 = { new PointF(firstX, y - 4)};
                            mm1.TransformPoints(arrowsF3);
                            gp.DrawLine(strobeColor,
                                    new PointF(arrowsF3[0].X + arrowHead, arrowsF3[0].Y),
                                    arrows[1]);
                            gp.DrawLine(strobeColor,
                                    new PointF(arrowsF3[0].X - arrowHead, arrowsF3[0].Y),
                                    arrows[1]);
                            if (st.getEndTime() != ts)
                            {
                                PointF[] arrowsS3 = {new PointF(secondX, y - 4)};
                                mm1.TransformPoints(arrowsS3);
                                gp.DrawLine(strobeColor,
                                        new PointF(arrowsS3[0].X + arrowHead, arrowsS3[0].Y),
                                        arrows[3]);
                                gp.DrawLine(strobeColor,
                                        new PointF(arrowsS3[0].X - arrowHead, arrowsS3[0].Y),
                                        arrows[3]);
                            }
                            break;
                    }
                    if (st.isFailed() && drawFailFlag)
                    {
                        //gp.Transform = new Matrix();
                        if (st.getEndTime() != ts)
                        {
                            SizeF sf = gp.MeasureString("F", theme.getFont());
                            PointF[] pts = {new PointF(secondX - (sf.Width / 2f) - ((secondX - firstX) / 2f),
                                                   y + 15)};
                            mm1.TransformPoints(pts);
                            gp.FillRectangle(st.getStrobeFailColor().Brush, pts[0].X, pts[0].Y, sf.Width, sf.Height);
                            gp.DrawString("F",
                                          theme.getFont(),
                                          theme.getBackground(),
                                          pts[0]);
                        }
                        else
                        {
                            SizeF sf = gp.MeasureString("F", theme.getFont());
                            PointF[] pts = {new PointF(secondX - (sf.Width / 2f),
                                                   y + 15)};
                            mm1.TransformPoints(pts);
                            gp.FillRectangle(st.getStrobeFailColor().Brush, pts[0].X, pts[0].Y, sf.Width, sf.Height);
                            gp.DrawString("F",
                                          theme.getFont(),
                                          theme.getBackground(),
                                          pts[0]);
                        }
                        //gp.Transform = mm1;
                    }
                }
                gp.Flush();
            }
            return img;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the signal data for the pin.
        /// </summary>
        /// 
        /// <param name="startTime">A <see cref="TimeSample"/> that
        /// represents the start of the draw window.</param>
        /// <param name="endTime">A <see cref="TimeSample"/> that
        /// represents the end of the draw window.</param>
        /// <param name="timeScale">A <see cref="TimeScale"/> that
        /// represents the unit of measure for them time.</param>
        /// <param name="type">1 denotes pattern data and 2
        /// denotes response data.</param>
        /// <param name="mat">A matrix containing the current
        /// scaling.</param>
        /// 
        /// <returns>Returns an image with the rendered signal
        /// data.</returns>
        /////////////////////////////////////////////////////////
        protected Image draw(TimeSample startTime, TimeSample endTime, TimeScale timeScale, int type, Matrix mat)
        {
            Bitmap img = null;
            if ((startTime != null ||
                endTime != null) &&
                getMaxVolt() != 0)
            {
                OrderedDictionary samples = null;
                Pen ct = null;
                switch (type)
                {
                    case 1:
                        samples = getPatternSamples();
                        ct = getPatternColor();
                        break;
                    case 2:
                        samples = getResponseSamples();
                        ct = getResponseColor();
                        break;
                }

                try
                {
                    TimeSample ts = endTime - startTime;
                    PointF[] pts = { new PointF(ts.getTime(timeScale) * 10, 90) };
                    if (pts[0].X < 1)
                        pts[0].X = 10;
                    if (pts[0].Y < 1)
                        pts[0].Y = 10;
                    mat.TransformPoints(pts);
                    img = new Bitmap((int)(pts[0].X), (int)(pts[0].Y));
                }
                catch (Exception e)
                {
                    // Do to garbage collection this catches an out of memory error.
                    System.Diagnostics.Debug.WriteLine("Pin - Draw: " + e.Message);
                }
                Graphics gp = Graphics.FromImage(img);
                Matrix mm1 = mat.Clone();
                mm1.Translate((-1) * startTime.getTime(timeScale) * 10, 0);
                //gp.Transform = mm1;

                int inverse = -1;
                int height = 40;
                PointF[] lastPoint = { new PointF(0, height) };
                mm1.TransformPoints(lastPoint);
                foreach (TimeSample ts in samples.Keys)
                {
                    //System.Diagnostics.Debug.WriteLine("Time: " + ts.ToString(timeScale));

                    // TODO : figure out the first last one.
                    if (ts < startTime)
                    {
                        float value1 = (float)(samples[ts]);
                        if (isDigital())
                            value1 = getDigitalValue(value1);
                        float y1 = inverse * ((value1 / getMaxVolt() * height) - height);
                        PointF[] p1t = { new PointF(ts.getTime(timeScale) * 10, y1) };
                        mm1.TransformPoints(p1t);
                        lastPoint[0] = new PointF(p1t[0].X, y1);
                        continue;
                    }

                    float value = (float)(samples[ts]);
                    if (isDigital())
                        value = getDigitalValue(value);
                    float y = inverse * ((value / getMaxVolt() * height) - height);
                    PointF[] pt = { new PointF(ts.getTime(timeScale) * 10, y) };

                    mm1.TransformPoints(pt);
                    if (isDigital())
                    {
                        PointF tempPoint = new PointF(pt[0].X, lastPoint[0].Y);
                        gp.DrawLine(ct, lastPoint[0], tempPoint);
                        lastPoint[0] = new PointF(pt[0].X, lastPoint[0].Y);
                    }
                    gp.DrawLine(ct, lastPoint[0], pt[0]);
                    lastPoint[0] = pt[0];
                    if (ts > endTime)
                        break;
                }
                gp.Flush();
            }
            else
                Console.Write("");
            return img;
        }
    }
}
