using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TEV_WaveformControl
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// Contains all the information to draw a header row
    /// on the Waveform Control.</summary>
    /// 
    /// <list type="table">
    ///     <listheader>
    ///         <term>Author</term>
    ///         <description>Version</description>
    ///     </listheader>
    ///     <item>
    ///         <term><a href="mailto:jeff.mccartney@aeroflex.com">
    ///               Jeff McCartney</a></term>
    ///         <description>0.1</description>
    ///     </item>
    /// </list>
    /////////////////////////////////////////////////////////
    public class Header
    {
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The name of the row that will also be used when
        /// displayed in the header.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected String m_Name;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The samples that will be displayed in the header for
        /// this row.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected OrderedDictionary m_Samples;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not this row will have corresponding
        /// grid lines displayed across all graphs.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected bool m_ShowGridLines;

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor: initializes the class.
        /// </summary>
        /////////////////////////////////////////////////////////
        public Header()
        {
            init();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor: initializes the class.  This also sets
        /// the row name based on the passed in value.
        /// </summary>
        /// 
        /// <param name="name">The name of the row.</param>
        /////////////////////////////////////////////////////////
        public Header(String name)
        {
            init();
            setName(name);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Creates a samples instance and sets the gride lines to
        /// false.
        /// </summary>
        /// 
        /// <returns>-1 but the return value has yet to be
        /// implemented.</returns>
        /////////////////////////////////////////////////////////
        public int init()
        {
            int status = -1;
            m_Samples = new OrderedDictionary();
            m_ShowGridLines = false;
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets this header row's samples to the provided 
        /// samples.
        /// </summary>
        /// 
        /// <param name="samples">A collection of data points, where
        /// TimeSample is the key and a float is the value.</param>
        /// 
        /// <returns>Returns 0 for success and -1 if an error
        /// occurred.</returns>
        /////////////////////////////////////////////////////////
        public int setSamples(OrderedDictionary samples)
        {
            int status = -1;
            // TODO : check and make sure that the samples contain the correct objects.
            if (samples != null &&
                samples.Count > 0)
            {
                m_Samples = samples;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Adds a single sample to the collection of samples for
        /// this header row.
        /// </summary>
        /// 
        /// <param name="time">A <see cref="TimeSample"/> representing
        /// The time stamp at which this sample occurs</param>
        /// <param name="value">An object of any type as long as the
        /// ToString function is implemented.  This represents the
        /// value that this sample contains.
        /// </param>
        /// 
        /// <returns>Returns 0 for success and -1 if an error
        /// occurred.</returns>
        /////////////////////////////////////////////////////////
        public int addSample(TimeSample time, object value)
        {
            int status = -1;
            if (!m_Samples.Contains(time) && value != null)
            {
                m_Samples.Add(time, value);
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not that the header row will display
        /// grid lines at the sample points.
        /// </summary>
        /// 
        /// <param name="show">A bool that represents whether or
        /// not to display grid lines.</param>
        /// 
        /// <returns>-1 but the return value has yet to be
        /// implemented.</returns>
        /////////////////////////////////////////////////////////
        public int showGridLines(bool show)
        {
            int status = -1;
            m_ShowGridLines = show;
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the name of this header row.
        /// </summary>
        /// 
        /// <returns>Returns a string representing the name of this
        /// header row.</returns>
        /////////////////////////////////////////////////////////
        public String getName()
        {
            return m_Name;
        }


        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the name for this header row.
        /// </summary>
        /// 
        /// <param name="name">A String representing the name of
        /// this header row.</param>
        /// 
        /// <returns>Returns 0 for success and a -1 if an error
        /// occurred.</returns>
        /////////////////////////////////////////////////////////
        public int setName(String name)
        {
            int status = -1;
            if (name != null && name.Length > 0)
            {
                m_Name = name;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the samples that this header row contains, where
        /// the key is a <see cref="TimeSample"/> and the value is
        /// an object.
        /// </summary>
        /// 
        /// <returns>Returns an OrderedDictionary where the key
        /// is the <see cref="TimeSample"/> and the value is
        /// an object.</returns>
        /////////////////////////////////////////////////////////
        public OrderedDictionary getSamples()
        {
            return m_Samples;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not this class will display grid
        /// lines.
        /// </summary>
        /// 
        /// <returns>Returns a bool where true indicates that this
        /// class will show grid lines and false indicating that
        /// it will not.</returns>
        /////////////////////////////////////////////////////////
        internal bool hasGridLines()
        {
            return m_ShowGridLines;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws a header row.
        /// </summary>
        /// 
        /// <param name="theme">The <see cref="Theme"/> to use when
        /// drawing the header information.</param>
        /// <param name="startTime">A <see cref="TimeSample"/> that
        /// represents the start of the draw window.</param>
        /// <param name="endTime">A <see cref="TimeSample"/> that
        /// represents the end of the draw window.</param>
        /// <param name="timeScale">A <see cref="TimeScale"/> that
        /// represents the unit of measure for them time.</param>
        /// <param name="mat">A matrix containing the current
        /// scaling.</param>
        /// 
        /// <returns>Returns an image with the rendered header
        /// data.</returns>
        /////////////////////////////////////////////////////////
        public Image draw(Theme theme, TimeSample startTime, TimeSample endTime, TimeScale timeScale, Matrix mat)
        {
            Bitmap img = null;
            if (startTime != null ||
                endTime != null)
            {
                try
                {
                    TimeSample ts = endTime - startTime;
                    float x = ts.getTime(timeScale) * 10;
                    PointF[] pts = { new PointF(x, 0) };
                    mat.TransformPoints(pts);
                    int y = 15;
                    if (hasGridLines())
                        y = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    if (pts[0].X < 1)
                        pts[0].X = 10;
                    img = new Bitmap((int)(pts[0].X), y);
                }
                catch (Exception e)
                {
                    // Do to garbage collection this catches an out of memory error.
                    System.Diagnostics.Debug.WriteLine("Header - Draw: " + e.Message);
                }
                Graphics gp = Graphics.FromImage(img);
                Matrix mm1 = mat.Clone();
                mm1.Translate((-1) * startTime.getTime(timeScale) * 10, 0);

                int height = img.Height;
                OrderedDictionary samples = getSamples();
                PointF lastPoint = new PointF(-100, -1);
                String lastString = "";
                foreach (TimeSample ts in samples.Keys)
                {
                    if (ts < startTime)
                        continue;
                    if (ts > endTime)
                        break;

                    PointF pt = new PointF(ts.getTime(timeScale) * 10, 0);

                    gp.Transform = new Matrix();
                    PointF[] pts = {pt};
                    mm1.TransformPoints(pts);
                    if (pts[0].X > lastPoint.X + gp.MeasureString(lastString, theme.getFont()).Width)
                    {
                        lastString = samples[ts] + "";
                        gp.DrawString(lastString, theme.getFont(), theme.getFontColor(), pts[0]);
                        lastPoint.X = pts[0].X;
                    }

                    //gp.Transform = mm1;
                    if (hasGridLines())
                    {
                        // TODO : Doesn't span the viewable area.
                        pts = new PointF[]{new PointF(pt.X, 0), new PointF(pt.X, height)};
                        mm1.TransformPoints(pts);
                        gp.DrawLine(theme.getDashedPen(), pts[0].X, pts[0].Y, pts[1].X, height);
                    }
                }

                gp.Flush();
            }
            else
                Console.Write("");
            return img;
        }
    }
}
