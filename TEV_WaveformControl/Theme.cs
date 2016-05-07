using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace TEV_WaveformControl
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// This class stores the various colors for the display
    /// of the Waveform Control.
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
    [Serializable()]
    public class Theme : ISerializable
    {
        /////////////////////////////////////////////////////////
        /// <summary>The font that will be used for the display
        /// of the text in the Waveform Control</summary>
        /////////////////////////////////////////////////////////
        protected Font m_Font;
        /////////////////////////////////////////////////////////
        /// <summary>The color of the font</summary>
        /////////////////////////////////////////////////////////
        protected Color m_FontBrush;
        /////////////////////////////////////////////////////////
        /// <summary>This is the background color.</summary>
        /////////////////////////////////////////////////////////
        protected Color m_BackgroundBrush;
        /////////////////////////////////////////////////////////
        /// <summary>This is the graph lines color. The graph
        /// includes x/y axis, hash marks, and border lines.</summary>
        /////////////////////////////////////////////////////////
        protected Color m_GraphPen;
        /////////////////////////////////////////////////////////
        /// <summary>The timeSpan Measures the distance between
        /// two transitions. This variable stores the color of
        /// both the text and line that represents this info.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Color m_TimeSpanPen; 
        /////////////////////////////////////////////////////////
        /// <summary>The vertical/horizontal cursors color.</summary>
        /////////////////////////////////////////////////////////
        protected Color m_CursorPen;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The color to use when displaying a selected pin.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Color m_PinSelection;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Contains the data to create a dashed line.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Pen m_DashedPen;

        /////////////////////////////////////////////////////////
        /// <summary>Constructor: Initializes the class to a
        /// default theme.</summary>
        ///
        /// <returns>Returns an instance of the theme class.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Theme()
        {
            m_Font = new Font(FontFamily.GenericMonospace, 8);
            m_FontBrush = Color.Green;
            m_BackgroundBrush = Color.Black;
            m_GraphPen = Color.Green;
            m_TimeSpanPen = Color.White;
            m_CursorPen = Color.LightGreen;
            m_PinSelection = Color.LightBlue;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Constructor: Deserialization constructor.</summary>
        ///
        /// <param name="info">Is a <see cref="SerializationInfo"/>
        /// that stores all the data needed to serialize or deserialize
        /// this object.</param>
        /// <param name="ctxt">Is a <see cref="StreamingContext"/>
        /// that describes the source and destination of a given
        /// serialized stream, as well as a means for serialization
        /// to retain that context and an additional caller-defined
        /// context.</param>
        ///
        /// <returns>Returns an instance of the class.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Theme(SerializationInfo info, StreamingContext ctxt)
        {
            // TODO : System.Xml.Serialization.XmlSerializer
            //Get the values from info and assign them to the appropriate properties
            m_Font = new Font((String)info.GetValue("FontFamily", typeof(String)),
                (float)info.GetValue("FontSize", typeof(float)));
            m_FontBrush = (Color)info.GetValue("FontBrush", typeof(Color));
            m_BackgroundBrush = (Color)info.GetValue("BackgroundBrush", typeof(Color));
            m_GraphPen = (Color)info.GetValue("GraphPen", typeof(Color));
            m_TimeSpanPen = (Color)info.GetValue("TimeSpanPen", typeof(Color));
            m_CursorPen = (Color)info.GetValue("CursorPen", typeof(Color));
            m_PinSelection = (Color)info.GetValue("PinSelection", typeof(Color));
        }
                
        /////////////////////////////////////////////////////////
        /// <summary>Serialization function.</summary>
        ///
        /// <param name="info">Is a <see cref="SerializationInfo"/>
        /// that stores all the data needed to serialize or deserialize
        /// this object.</param>
        /// <param name="ctxt">Is a <see cref="StreamingContext"/>
        /// that describes the source and destination of a given
        /// serialized stream, as well as a means for serialization
        /// to retain that context and an additional caller-defined
        /// context.</param>
        /////////////////////////////////////////////////////////
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            // You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("FontFamily", m_Font.FontFamily.ToString());
            info.AddValue("FontSize", m_Font.Size);
            info.AddValue("FontBrush", m_FontBrush);
            info.AddValue("BackgroundBrush", m_BackgroundBrush);
            info.AddValue("GraphPen", m_GraphPen);
            info.AddValue("TimeSpanPen", m_TimeSpanPen);
            info.AddValue("CursorPen", m_CursorPen);
            info.AddValue("PinSelection", m_PinSelection);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the Pen that represents a dashed line with a
        /// color of the graph.
        /// </summary>
        /// 
        /// <returns>Returns a Pen representing a dashed line and
        /// in the color of the graph.</returns>
        /////////////////////////////////////////////////////////
        public Pen getDashedPen()
        {
            float[] dashValues = { 3f, 3f };
            m_DashedPen = new Pen(getGraphColor().Color);
            m_DashedPen.DashPattern = dashValues;
            return m_DashedPen;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Returns the font that will be used for displaying
        /// the text in the Waveform control.</summary>
        ///
        /// <returns>Returns an instance of the font to be used or
        /// null if it has yet to be defined.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Font getFont()
        {
            return m_Font;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the given font to the theme font.</summary>
        ///
        /// <param name="f">The <see cref="Font"/> to be used by
        /// the theme.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 indicating an error has occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setFont(Font f)
        {
            int status = -1;
            if (f != null)
            {
                m_Font = f;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Returns the color that will be used for the
        /// text.</summary>
        ///
        /// <returns>Returns a Brush instance containing the
        /// color to be used for drawing text.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Brush getFontColor()
        {
            return new Pen(m_FontBrush).Brush;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the color to be used for text.</summary>
        ///
        /// <param name="b">A <see cref="Brush"/> that has the new
        /// color to use for text.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setFontBrush(Brush b)
        {
            int status = -1;
            if (b != null)
            {
                m_FontBrush = new Pen(b).Color;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the brush that contains the color that will be
        /// used to show a selected pin.
        /// </summary>
        /// 
        /// <returns>Returns a Brush instance representing the
        /// color that will be used to show a selected pin.</returns>
        /////////////////////////////////////////////////////////
        public Brush getPinSelectionColor()
        {
            return new Pen(m_PinSelection).Brush;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the pin selection color.
        /// </summary>
        /// 
        /// <param name="b">A Brush instance contain the color to
        /// use to show a selected pin.</param>
        /// 
        /// <returns>Returns a 0 for success and a -1 when an
        /// error occurred.</returns>
        /////////////////////////////////////////////////////////
        public int setPinSelectionColor(Brush b)
        {
            int status = -1;
            if (b != null)
            {
                m_PinSelection = new Pen(b).Color;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Returns a <see cref="Brush"/> that has the
        /// color of the background for the Waveform.</summary>
        ///
        /// <returns>Returns a <see cref="Brush"/> that has the
        /// color of the background or null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Brush getBackground()
        {
            return new Pen(m_BackgroundBrush).Brush;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the color to be used for the background.
        /// </summary>
        ///
        /// <param name="b">A <see cref="Brush"/> representing the
        /// color to use for the background.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setBackgroundBrush(Brush b)
        {
            int status = -1;
            if (b != null)
            {
                m_BackgroundBrush = new Pen(b).Color;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Returns an instance of Pen that stores the 
        /// color used to draw the graph lines.</summary>
        ///
        /// <returns>Returns an instance of Pen that stores the 
        /// color used to draw the graph lines or null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen getGraphColor()
        {
            return new Pen(m_GraphPen);
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the graph color.</summary>
        ///
        /// <param name="p">An instance of <see cref="Pen"/>
        /// containing the color.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setGraphPen(Pen p)
        {
            int status = -1;
            if (p != null)
            {
                m_GraphPen = p.Color;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>The timeSpan Measures the distance between
        /// two transitions. This returns the color of both the
        /// text and line that represents this info.</summary>
        ///
        /// <returns>Returns an instance of Pen that represents
        /// the color or null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen getTimeSpanColor()
        {
            return new Pen(m_TimeSpanPen);
        }

        /////////////////////////////////////////////////////////
        /// <summary>The timeSpan Measures the distance between
        /// two transitions. This sets the color of both the
        /// text and line that represents this info.</summary>
        ///
        /// <param name="p">An instance of <see cref="Pen"/> that
        /// has the desired color.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setTimeSpanPen(Pen p)
        {
            int status = -1;
            if (p != null)
            {
                m_TimeSpanPen = p.Color;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Returns a Pen instance containing the color
        /// that is currently being used to display cursors.</summary>
        ///
        /// <returns>Returns an instance of Pen containing the
        /// color or null.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen getCursorColor()
        {
            return new Pen(m_CursorPen);
        }

        /////////////////////////////////////////////////////////
        /// <summary>This sets the cursor color in the Waveform
        /// control.</summary>
        ///
        /// <param name="p">An Instance of <see cref="Pen"/> that
        /// contains the color.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurred.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setCursorPen(Pen p)
        {
            int status = -1;
            if (p != null)
            {
                m_CursorPen = p.Color;
                status = 0;
            }
            return status;
        }
    }
}
