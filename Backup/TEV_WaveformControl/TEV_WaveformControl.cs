using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Collections;

namespace TEV_WaveformControl
{
    // A delegate type for hooking up change notifications.
    public delegate void SelectedPinChangedEventHandler(object sender, EventArgs e);

    /////////////////////////////////////////////////////////
    /// <summary>
    /// This control is designed to recreate an
    /// oscilloscope/waveform.  It has the following functions:
    /// 
    /// Displays the elapsed signal data for a Pin over a given
    /// time period. 
    /// 
    /// This control can zoom the entire view or it can zoom
    /// just along the time axis.
    /// 
    /// This control supports the use of cursors to measure 
    /// signals.
    /// 
    /// This control supports the realtime measurement of signal
    /// transitions.
    /// 
    /// The footer time step is adjustable.
    /// 
    /// Has a customizable header.
    /// 
    /// Has a customizable theme.
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
    ///         <description>1.0</description>
    ///     </item>
    /// </list>
    /////////////////////////////////////////////////////////
    public partial class TEV_WaveformControl : UserControl
    {
        #region Constants
        /////////////////////////////////////////////////////////
        /// <summary>This is an index into the cursor array for
        /// the left cursor.</summary>
        /////////////////////////////////////////////////////////
        public const int CURSOR_LEFT = 0;
        /////////////////////////////////////////////////////////
        /// <summary>This is an index into the cursor array for
        /// the right cursor.</summary>
        /////////////////////////////////////////////////////////
        public const int CURSOR_RIGHT = 1;
        /////////////////////////////////////////////////////////
        /// <summary>This is an index into the cursor array for
        /// the top cursor.</summary>
        /////////////////////////////////////////////////////////
        public const int CURSOR_TOP = 2;
        /////////////////////////////////////////////////////////
        /// <summary>This is an index into the cursor array for
        /// the bottom cursor.</summary>
        /////////////////////////////////////////////////////////
        public const int CURSOR_BOTTOM = 3;
        #endregion

        /////////////////////////////////////////////////////////
        /// <summary>Defines the viewable types that the graph
        /// can display.
        /// </summary>
        /////////////////////////////////////////////////////////
        public enum GraphMode
        {
            /////////////////////////////////////////////////////////
            /// <summary>This displays the pattern on the pin</summary>
            /////////////////////////////////////////////////////////
            MODE_STANDARD_PATTERN = -1,
            /////////////////////////////////////////////////////////
            /// <summary>This displays the response on the pin</summary>
            /////////////////////////////////////////////////////////
            MODE_STANDARD_RESPONSE = 1,
            /////////////////////////////////////////////////////////
            /// <summary>This displays the pattern sent on the pin
            /// on top and the response, on the pin, on the bottom.
            /// </summary>
            /////////////////////////////////////////////////////////
            MODE_SPLIT,
            /////////////////////////////////////////////////////////
            /// <summary>this displays the pattern sent on the pin
            /// overlayed with the response on the pin.  The response is
            /// offset from the pattern.</summary>
            /////////////////////////////////////////////////////////
            MODE_OVERLAY
        };

        /////////////////////////////////////////////////////////
        /// <summary>Defines the types of cursors that can be
        /// displayed.
        /// </summary>
        /////////////////////////////////////////////////////////
        public enum CursorMode
        {
            /////////////////////////////////////////////////////////
            /// <summary>Switches the cursor mode off.</summary>
            /////////////////////////////////////////////////////////
            OFF,
            /////////////////////////////////////////////////////////
            /// <summary>Switches the cursor mode to left and right
            /// cursors.</summary>
            /////////////////////////////////////////////////////////
            LEFT_RIGHT,
            /////////////////////////////////////////////////////////
            /// <summary>Switches the cursor mode to top and bottom
            /// cursors.</summary>
            /////////////////////////////////////////////////////////
            TOP_BOTTOM
        };

        /////////////////////////////////////////////////////////
        /// <summary>The name of the graph. Do we still need this?
        /// Make it part of the header?</summary>
        /////////////////////////////////////////////////////////
        protected String m_Name;
        /////////////////////////////////////////////////////////
        /// <summary>Denotes which graph mode will be used when
        /// displaying the graph.</summary>
        /////////////////////////////////////////////////////////
        protected GraphMode m_GraphMode = GraphMode.MODE_STANDARD_PATTERN;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The x/y offset of the overlay signal in pixels.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Point m_OverlayOffset;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// This denotes where the y axis is drawn for the graphs.
        /// This is more of a cheater value since the width of the
        /// labels are what's used for the actual graph.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected int m_GraphOffset = 170;  // hard coded offset for the start of the graphs
        /////////////////////////////////////////////////////////
        /// <summary>The theme that will be used to display the
        /// graph.</summary>
        /////////////////////////////////////////////////////////
        protected Theme m_Theme;
        /////////////////////////////////////////////////////////
        /// <summary>This stores all the pins.</summary>
        /////////////////////////////////////////////////////////
        protected List<Pin> m_Pins;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not to show the strobes.  True means
        /// that they will be shown and false means that they will
        /// not be shown.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected bool m_ShowStrobes;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not to show the fail state of all
        /// the strobes.  True means that they will be shown and
        /// false means that they will not be shown.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected bool m_ShowStrobeFailState;
        /////////////////////////////////////////////////////////
        /// <summary>This controls the x/y zooming of the graph.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected float m_Scale;
        /////////////////////////////////////////////////////////
        /// <summary>This controls the x scaling of the graph
        /// data only.</summary>
        /////////////////////////////////////////////////////////
        protected float m_xScale;
        /////////////////////////////////////////////////////////
        /// <summary>This controls the y scaling of the graph
        /// data only.</summary>
        /////////////////////////////////////////////////////////
        protected float m_yScale;
        /////////////////////////////////////////////////////////
        /// <summary>The viewable area that can be drawn in.</summary>
        /////////////////////////////////////////////////////////
        protected Rectangle m_DrawWindow;
        /////////////////////////////////////////////////////////
        /// <summary>Denotes whether or not their will be grid
        /// lines every so often on the graph.</summary>
        /////////////////////////////////////////////////////////
        protected bool m_ShowGridLines;
        /////////////////////////////////////////////////////////
        /// <summary>Contains the location data for each of the 
        /// measurement cursors that can be displayed on the screen.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected PointF[] m_Cursors = new PointF[4];
        /////////////////////////////////////////////////////////
        /// <summary>Denotes the types of cursors that will be
        /// displayed.</summary>
        /////////////////////////////////////////////////////////
        protected CursorMode m_CursorMode = CursorMode.OFF;
        /////////////////////////////////////////////////////////
        /// <summary>The number of pixels the header takes up.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected int m_HeaderSpace = 55;
        /////////////////////////////////////////////////////////
        /// <summary>The location in which the footer starts.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected int m_FooterSpace = 0;
        /////////////////////////////////////////////////////////
        /// <summary>The rows that will be displayed at the top
        /// of the graph in the header region.  The first dictionary
        /// contains the label for the row and the other dictionary
        /// is the value that will show up at the given
        /// <see ref="TimeSample"/>.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected ArrayList m_HeaderRows;
        /////////////////////////////////////////////////////////
        /// <summary>This is a general use pen to allow the drawing
        /// of dashed lines.</summary>
        /////////////////////////////////////////////////////////
        protected Pen m_DashedPen;
        /////////////////////////////////////////////////////////
        /// <summary>Used to track/calculate the horizontal scroll
        /// maximum size.</summary>
        /////////////////////////////////////////////////////////
        protected int m_hScrollMax = 0;
        /////////////////////////////////////////////////////////
        /// <summary>Used to track/calculate the horizontal scroll
        /// step size.</summary>
        /////////////////////////////////////////////////////////
        protected int m_hScrollStep = 0;
        /////////////////////////////////////////////////////////
        /// <summary>Denotes how often the grid lines will appear.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected int m_GridStepping = 15;
        /////////////////////////////////////////////////////////
        /// <summary>Denotes how often the graph time labels
        /// will appear.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected int m_GraphStepping = 10;
        /////////////////////////////////////////////////////////
        /// <summary>Denotes the TimeScale that the graph will
        /// used to display the time measurements in.</summary>
        /////////////////////////////////////////////////////////
        protected TimeScale m_TimeScale = TimeScale.MICROSECONDS;
        /////////////////////////////////////////////////////////
        /// <summary>True will show the measurement between to
        /// transitions of the signal. False will not.</summary>
        /////////////////////////////////////////////////////////
        protected bool m_TransitionSpan = true;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The currently selected pin.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Pin m_SelectedPin = null;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The y location of the currently selected pin.  Where
        /// x is the low y value and y is the high y value.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Point m_SelectedPinPos = new Point(-1, -1);
        /////////////////////////////////////////////////////////
        /// <summary>Keeps track of which button was pushed on the
        /// mouse.</summary>
        /////////////////////////////////////////////////////////
        protected MouseState m_MouseState = new MouseState();
        /////////////////////////////////////////////////////////
        /// <summary>Tacks whether or not the display needs
        /// redrawn.  May not be needed since the onPaint will
        /// only get called when it needs redrawn.</summary>
        /////////////////////////////////////////////////////////
        protected bool m_ViewChanged = true;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// This is used to cache the full render of the waveforms.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Bitmap m_CacheRender;
        /////////////////////////////////////////////////////////
        /// <summary>This controls how the lines are drawn of the 
        /// signal.  True indicates that when going from one time
        /// measurement to the next, it will draw a step between
        /// the two and false means that it will draw a strait
        /// line between the two.</summary>
        /////////////////////////////////////////////////////////
        protected bool m_Digital = true;

        /////////////////////////////////////////////////////////
        /// <summary>Constructor: Initializes the gui components
        /// and sets member variables to known states.  Then sets
        /// up the viewable area.</summary>
        ///
        /// <returns>Returns an instance of the class.
        /// </returns>
        /////////////////////////////////////////////////////////
        public TEV_WaveformControl()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(pbx_Waveform_MouseWheel);
            init();
            calcViewable();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// An event that clients can use to be notified whenever the
        /// pin changes.
        /// </summary>
        /////////////////////////////////////////////////////////
        public event SelectedPinChangedEventHandler SelectedPinChanged;

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Invoke the Changed event; called whenever the pin changes.
        /// </summary>
        /// 
        /// <param name="e">Not used by this event.</param>
        /////////////////////////////////////////////////////////
        protected virtual void OnSelectedPinChanged(EventArgs e)
        {
            if (SelectedPinChanged != null)
                SelectedPinChanged(this, e);
        }

        /////////////////////////////////////////////////////////
        /// <summary>Initializes the graph with the given name and
        /// the given theme.  Then sets the scale to 20 units.
        /// </summary>
        ///
        /// <param name="name">A <see cref="String"/> representing
        /// the name of the graph.</param>
        /// <param name="theme">The <see cref="Theme"/> contains the
        /// colors to be used to draw the graph.
        /// </param>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setGraph(String name, Theme theme)
        {
            int status = -1;
            setName(name);
            setTheme(theme);
            calcViewable();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets member variables to default values.</summary>
        /////////////////////////////////////////////////////////
        public void init()
        {
            m_OverlayOffset = new Point(5, 5);
            m_ShowStrobes = true;
            m_ShowStrobeFailState = true;
            m_Pins = new List<Pin>();
            m_HeaderRows = new ArrayList();
            for (int i = 0; i < m_Cursors.Length; i++)
                m_Cursors[i] = new Point(-1, -1);
        }

        /////////////////////////////////////////////////////////
        /// <summary>Adds a label and data to the header area.
        /// </summary>
        ///
        /// <param name="row">A <see cref="Header"/> object with
        /// the row information to be displayed.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be:<br/>
        /// -1 for a general error.<br/>
        /// </returns>
        /////////////////////////////////////////////////////////
        public int addHeaderRow(Header row)
        {
            int status = -1;
            if (row != null)
            {
                m_HeaderRows.Add(row);
                //m_HeaderSpace = (int)(m_HeaderRows.Count * 40);
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the given <see cref="Header"/> from the list
        /// of header rows.
        /// </summary>
        /// 
        /// <param name="row">A <see cref="Header"/> instance that
        /// will be removed.</param>
        /// 
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if it was unable to remove the
        /// <see cref="Header"/> row.</returns>
        /////////////////////////////////////////////////////////
        public int removeHeaderRow(Header row)
        {
            int status = -1;
            if (m_HeaderRows.Contains(row))
            {
                m_HeaderRows.Remove(row);
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Removes all of the <see cref="Header"/> rows from the
        /// collection.
        /// </summary>
        /// 
        /// <returns>Returns a -1, a proper status has yet to be
        /// implemented.</returns>
        /////////////////////////////////////////////////////////
        public int clearHeaders()
        {
            int status = -1;
            m_HeaderRows.Clear();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the current graph mode being used.
        /// </summary>
        /// 
        /// <returns>Returns the current graph mode being used.
        /// </returns>
        /////////////////////////////////////////////////////////
        public GraphMode getGraphMode()
        {
            return m_GraphMode;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the graph mode to use when displaying the graph.
        /// </summary>
        /// 
        /// <param name="mode">Denotes the graph mode to use.</param>
        /// 
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setGraphMode(GraphMode mode)
        {
            int status = -1;
            m_GraphMode = mode;
            if (m_GraphMode == GraphMode.MODE_SPLIT)
                clearCursors();
            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// This sets the offset in pixels for the overlay mode.
        /// The offset moves the response signal based on the
        /// Point that is provide.
        /// </summary>
        /// 
        /// <param name="offset">A Point representing the offset
        /// to use for the overlay mode.</param>
        /// 
        /// <returns>Returns 0 for success and a -1 if an error
        /// occurred.</returns>
        /////////////////////////////////////////////////////////
        public int setOverlayOffset(Point offset)
        {
            int status = -1;
            if (offset.X >= 0 && offset.Y >= 0)
            {
                m_OverlayOffset = offset;
                m_ViewChanged = true;
                Invalidate();
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the offset, as a Point, to be used when displaying
        /// the overlay mode.
        /// </summary>
        /// 
        /// <returns>Returns the offset, as a Point, to be used
        /// when displaying the overlay mode.</returns>
        /////////////////////////////////////////////////////////
        public Point getOverlayOffset()
        {
            return m_OverlayOffset;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not the strobes will be displayed.
        /// </summary>
        /// 
        /// <param name="show">A bool where true indicates that
        /// the strobes will be displayed and false indicates
        /// that they will not be displayed.</param>
        /// 
        /// <returns>-1 but the return value has yet to be
        /// implemented.</returns>
        /////////////////////////////////////////////////////////
        public int showStrobes(bool show)
        {
            int status = -1;
            m_ShowStrobes = show;
            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not a strobe's fail state should be
        /// shown.
        /// </summary>
        /// 
        /// <param name="show">A bool where true indicates that
        /// the strobe's fail states will be displayed and false
        /// indicates that they will not be displayed.</param>
        /// 
        /// <returns>-1 but the return value has yet to be
        /// implemented.</returns>
        /////////////////////////////////////////////////////////
        public int showStrobeFailState(bool show)
        {
            int status = -1;
            m_ShowStrobeFailState = show;
            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Not implemented yet.
        /// </summary>
        /// 
        /// <returns></returns>
        /////////////////////////////////////////////////////////
        public int setSelectedPin(Pin p)
        {
            int status = -1;
            if (m_Pins.Contains(p))
            {
                m_SelectedPin = p;
                OnSelectedPinChanged(EventArgs.Empty);
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the currently selected pin.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="Pin"/> representing the
        /// currently selected pin.</returns>
        /////////////////////////////////////////////////////////
        public Pin getSelectedPin()
        {
            return m_SelectedPin;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the time at the last location the left mouse
        /// button was clicked.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="TimeSample"/> at the mouse
        /// location or null.</returns>
        /////////////////////////////////////////////////////////
        public TimeSample getSelectedTime()
        {
            TimeSample ts = convertXtoTime(m_MouseState.m_Location.X);
            //if (m_Cursors[CURSOR_LEFT] != new Point(-1, -1))
            //{
            //    if (m_Cursors[CURSOR_RIGHT] != new Point(-1, -1))
            //    {
            //        ts = new TimeSample(m_Cursors[CURSOR_RIGHT].X,
            //                                           TimeScale.PICOSECONDS);
            //    }
            //    else
            //    {
            //        ts = new TimeSample(m_Cursors[CURSOR_LEFT].X,
            //                                           TimeScale.PICOSECONDS);
            //    }
            //}
            return ts;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the voltage value from where the left mouse button
        /// was last clicked.
        /// </summary>
        /// 
        /// <returns>Returns a float representing the volts at the
        /// mouse location.</returns>
        /////////////////////////////////////////////////////////
        public float getSelectedVolts()
        {
            float v = 0f;
            if (m_Cursors[CURSOR_BOTTOM] != new Point(-1, -1))
            {
                v = m_Cursors[CURSOR_BOTTOM].Y;
            }
            else
            {
                v = m_Cursors[CURSOR_TOP].Y;
            }
            return v;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the interval to display time labels on
        /// the graph in the footer region.
        /// </summary>
        /// 
        /// <param name="step">An int representing how often these
        /// labels will be displayed.</param>
        /// 
        /// <returns>Returns 0 for success and a -1 if an error
        /// occurs.</returns>
        /////////////////////////////////////////////////////////
        public int setGraphStepping(int step)
        {
            int status = -1;
            if (step > 0)
            {
                m_GraphStepping = step;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the current stepping that is being used on this
        /// graph.
        /// </summary>
        /// 
        /// <returns>Returns the current stepping that is being
        /// used on this graph.</returns>
        /////////////////////////////////////////////////////////
        public int getGraphStepping()
        {
            return m_GraphStepping;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Gets the current stepping value for the
        /// grid lines.</summary>
        ///
        /// <returns>Returns an int for the current stepping value
        /// at which to show the grid lines.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int getGridStepping()
        {
            return m_GridStepping;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the stepping value for which to show
        /// the grid lines.</summary>
        ///
        /// <param name="step">An int representing the step value
        /// in the current TimeScale.</param>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setGridStepping(int step)
        {
            int status = -1;
            m_GridStepping = step;
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the current mode for displaying cursors.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="CursorMode"/> representing
        /// the current mode for displaying the cursors.</returns>
        /////////////////////////////////////////////////////////
        public CursorMode getCursorMode()
        {
            return m_CursorMode;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the mode for how the cursors will be displayed.
        /// </summary>
        /// 
        /// <param name="mode">A <see cref="CursorMode"/> object
        /// that represents the way in which to display the
        /// cursors.</param>
        /// 
        /// <returns>-1 but the return value has yet to be
        /// implemented.</returns>
        /////////////////////////////////////////////////////////
        public int setCursorMode(CursorMode mode)
        {
            int status = -1;
            m_CursorMode = mode;
            clearCursors();
            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Gets the current TimeScale for the graph.
        /// </summary>
        ///
        /// <returns>Returns the current TimeScale for the graph.
        /// </returns>
        /////////////////////////////////////////////////////////
        public TimeScale getTimeScale()
        {
            return m_TimeScale;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the TimeScale to be used in the display
        /// of the graph.</summary>
        ///
        /// <param name="scale">A <see cref="TimeScale"/> instance.
        /// </param>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setTimeScale(TimeScale scale)
        {
            int status = -1;
            m_TimeScale = scale;
            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Denotes whether or not the transition span
        /// will be shown.  </summary>
        ///
        /// <returns>Returns True means that it will and false
        /// means that it won't.
        /// </returns>
        /////////////////////////////////////////////////////////
        public bool getTransitionSpan()
        {
            return m_TransitionSpan;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Set whether or not to display the transition 
        /// measurement.</summary>
        ///
        /// <param name="span">A bool where true means that the
        /// transition measurement will be displayed and false
        /// means that it won't.</param>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setTransitionSpan(bool span)
        {
            int status = -1;
            m_TransitionSpan = span;
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Calculates the drawable window size and both
        /// scroll bar sizes.  Then causes a redraw of the control.
        /// </summary>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int calcViewable()
        {
            int status = -1;
            m_DrawWindow.X = 0;
            m_DrawWindow.Y = 0;
            m_DrawWindow.Width = this.Width;
            m_DrawWindow.Height = this.Height;
            m_FooterSpace = m_DrawWindow.Height - (m_HeaderSpace + 15);

            updateScrollBars();

            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Recalculates the scroll bars.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected void updateScrollBars()
        {
            if (m_Pins != null && m_DrawWindow.Height > 0 && m_DrawWindow.Width > 0)
            {
                vs_ScrollBar.SmallChange = 1;
                hs_ScrollBar.SmallChange = 1; 
                int graphSize = getGraphHeight();
                int height = 1;
                if (m_GraphMode == GraphMode.MODE_SPLIT)
                {
                    height = (m_Pins.Count - 1);
                    graphSize *= 2;
                }
                else
                    height = m_Pins.Count - 1;
                if (height >= 0 && graphSize > 0)
                {
                    vs_ScrollBar.Maximum = height;
                    // calc LargeChange properly
                    vs_ScrollBar.LargeChange = (m_DrawWindow.Height - (m_HeaderSpace * 2)) / graphSize;
                }
                else
                {
                    vs_ScrollBar.Maximum = 0;
                    vs_ScrollBar.LargeChange = 1;
                }
                if (m_Pins.Count > 0)
                {
                    TimeSample startTime = getViewableStartTime();
                    TimeSample endTime = getViewableEndTime();
                    TimeSample timeDiff = (endTime - startTime);
                    TimeSample maxTime = m_Pins[0].getMaxTime();
                    // Calc the hs_max value properly.
                    if (maxTime >= timeDiff - (timeDiff * 0.1))
                    {
                        hs_ScrollBar.Maximum = (int)Math.Ceiling(
                            (
                                timeDiff / 2 +
                                (
                                    timeDiff
                                ) *
                                (maxTime / timeDiff)
                            ).getTime());
                        // TODO : Calc.  How many time slices take up a pixel.
                        hs_ScrollBar.SmallChange = (int)Math.Ceiling(timeDiff.getTime() / m_DrawWindow.Width);
                    }
                    else
                        hs_ScrollBar.Maximum = 0;
                    hs_ScrollBar.LargeChange = (int)Math.Ceiling(timeDiff.getTime());
                }
                else
                    hs_ScrollBar.Maximum = 0;
            }
            else
            {
                vs_ScrollBar.Maximum = 0;
                hs_ScrollBar.Maximum = 0;
            }
            vs_ScrollBar.Invalidate();
            hs_ScrollBar.Invalidate();
        }

        /////////////////////////////////////////////////////////
        /// <summary>Deprecated. Sets the xy scale or zoom of the
        /// graph.</summary>
        ///
        /// <param name="scale">A float for the new xy scale or
        /// zoom value.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 indicating that the scale is less
        /// than 0.
        /// </returns>
        /////////////////////////////////////////////////////////
        [Obsolete("Use x and y scaling functions.")]
        public int setScale(float scale)
        {
            int status = -1;
            if(scale > 0)
            {
                m_Scale = scale;
                status = 0;
            }
            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Deprecated. Gets the current xy scale/zoom of the
        /// graph.</summary>
        ///
        /// <returns>Returns a float the current xy scale/zoom of
        /// the graph.
        /// </returns>
        /////////////////////////////////////////////////////////
        [Obsolete("Use x and y scaling functions.")]
        public float getScale()
        {
            return m_Scale;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the x scale value for the graph.</summary>
        ///
        /// <param name="scale">A float for the new scale value in
        /// the x direction.
        /// </param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if scale is less than -1.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setxScale(float scale)
        {
            int status = -1;
            if (scale > -1)
            {
                m_xScale = scale;
                status = 0;
            }
            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Gets the value for the current x scale.
        /// </summary>
        ///
        /// <returns>Returns the value for the current x scale.
        /// </returns>
        /////////////////////////////////////////////////////////
        public float getxScale()
        {
            return m_xScale;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Zooms in the x direction to expand the cursors
        /// to the edge of the current viewable area. Currently
        /// does not support horizontal cursors.</summary>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int zoomToCursors()
        {
            int status = -1;
            if (m_CursorMode == CursorMode.LEFT_RIGHT &&
                m_Cursors[CURSOR_LEFT] != new Point(-1, -1) &&
                m_Cursors[CURSOR_RIGHT] != new Point(-1, -1))
            {
                //calcViewable();
                // Cursor.x is the time value.
                TimeSample ts = new TimeSample(m_Cursors[CURSOR_LEFT].X,
                                               TimeScale.PICOSECONDS);
                int value = (int)Math.Ceiling(ts.getTime());
                hs_ScrollBar.Maximum = value + 10;
                hs_ScrollBar.Value = value;

                int x = 0;
                m_xScale = 0;
                ts = new TimeSample(m_Cursors[CURSOR_RIGHT].X,
                                               TimeScale.PICOSECONDS);
                while (x < m_DrawWindow.Width && x >= 0)
                {
                    m_xScale += 0.1f;
                    x = (int)(m_GraphOffset + 10 + convertTimeToX(ts));
                }
                m_xScale -= 0.2f;

                m_ViewChanged = true;
                Invalidate();
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the y scale value for the graph.</summary>
        ///
        /// <param name="scale">A float for the new scale value in
        /// the y direction.
        /// </param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if scale is less than -1.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setyScale(float scale)
        {
            int status = -1;
            if (scale > -1)
            {
                m_yScale = scale;
                status = 0;
            }
            calcViewable();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Gets the value for the current y scale.
        /// </summary>
        ///
        /// <returns>Returns the value for the current y scale.
        /// </returns>
        /////////////////////////////////////////////////////////
        public float getyScale()
        {
            return m_yScale;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the name of the graph.</summary>
        ///
        /// <param name="name">A <see cref="String"/> representing
        /// the name of the graph.</param>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setName(String name)
        {
            int status = -1;
            m_Name = name;
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the theme to be used when displaying the
        /// graph.  Once set this function redraws the control.
        /// </summary>
        ///
        /// <param name="theme">The <see cref="Theme"/> contains the
        /// colors to be used to draw the graph.</param>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setTheme(Theme theme)
        {
            int status = -1;

            if (theme != null)
                m_Theme = theme;
            else
                m_Theme = new Theme();

            float[] dashValues = { 3f, 3f };
            m_DashedPen = new Pen(m_Theme.getGraphColor().Color);
            m_DashedPen.DashPattern = dashValues;

            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Adds a pin to be displayed on the graph.</summary>
        ///
        /// <param name="pin"/>A <see cref="Pin"/> to be added.</param>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int addPin(Pin pin)
        {
            int status = -1;
            m_Pins.Add(pin);
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Removes all of the pins from the graph.</summary>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int clearPins()
        {
            int status = -1;
            m_Pins.Clear();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Removes a pin based on the given name.</summary>
        ///
        /// <param name="name">A <see cref="String"/> representing
        /// the label name of the pin.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if it fails to remove the pin.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int removePin(String name)
        {
            int status = -1;
            for (int i = 0; i < m_Pins.Count; i++)
            {
                if (m_Pins[i].getName().Equals(name))
                {
                    m_Pins.RemoveAt(i);
                    status = 0;
                }
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Removes the given pin from the collection.</summary>
        ///
        /// <param name="pin">A <see cref="Pin"/> representing
        /// the one to be removed.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if it fails to remove the pin.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int removePin(Pin pin)
        {
            return removePin(pin.getName());
        }

        /////////////////////////////////////////////////////////
        /// <summary>Turns off the display of the cursors.</summary>
        ///
        /// <returns>-1 but the return value has yet to be
        /// implemented.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int clearCursors()
        {
            int status = -1;
            for (int i = 0; i < m_Cursors.Length; i++)
            {
                m_Cursors[i].X = -1;
                m_Cursors[i].Y = -1;
            }
            m_MouseState.m_Button = MouseButtons.None;
            m_MouseState.m_Location.X = -1;
            m_MouseState.m_Location.Y = -1;
            //System.Diagnostics.Debug.WriteLine("clearCursors");
            m_ViewChanged = true;
            Invalidate();
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Takes the given <see ref="TimeSample"/> and converts
        /// it to the appropriate x value on the screen.
        /// </summary>
        /// 
        /// <param name="ts">The <see ref="TimeSample"/> to convert
        /// to an x position.</param>
        /// 
        /// <returns>The x position that the <see ref="TimeSample"/>
        /// represents.</returns>
        /////////////////////////////////////////////////////////
        protected float convertTimeToX(TimeSample ts)
        {
            Matrix mat = new Matrix();
            mat.Scale(m_xScale + 1, 1f);
            Point[] pts = {new Point(10, 10)};
            mat.TransformPoints(pts);
            TimeSample tss = new TimeSample(hs_ScrollBar.Value, TimeScale.PICOSECONDS);
            return (ts - tss).getTime(m_TimeScale) * pts[0].X;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Takes the x position and converts it to a
        /// <see cref="TimeSample"/> value.
        /// </summary>
        /// 
        /// <param name="x">The x location on the screen.</param>
        /// 
        /// <returns>Returns a <see cref="TimeSample"/> value 
        /// representing the time at the given x.</returns>
        /////////////////////////////////////////////////////////
        protected TimeSample convertXtoTime(int x)
        {
            Point[] pts = { new Point(10, 10) };
            Matrix mat = new Matrix();
            mat.Scale(m_xScale + 1f, m_yScale + 1f);
            mat.TransformPoints(pts);
            float sl = x - m_GraphOffset;
            float ls = sl / pts[0].X;
            // this value is the time sample translated from the mouse position.
            float value = ls;
            TimeSample ts = new TimeSample(value, m_TimeScale);
            TimeSample tss = new TimeSample(hs_ScrollBar.Value, TimeScale.PICOSECONDS);
            return ts + tss;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Converts the voltage value to a y value based on the
        /// currently selected pin.  Not implemented.
        /// </summary>
        /// 
        /// <param name="volts">A voltage value.</param>
        /// 
        /// <returns>The y location related to the voltage value.
        /// </returns>
        /////////////////////////////////////////////////////////
        protected int convertVoltsToY(float volts)
        {
            return -1;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Converts the y value to a voltage value based on the
        /// currently selected pin.
        /// </summary>
        /// 
        /// <param name="y">A y position.</param>
        /// 
        /// <returns>The voltage value related to the y location.
        /// </returns>
        /////////////////////////////////////////////////////////
        protected float convertYToVolts(int y)
        {
            float v = 0f;

            int index = m_Pins.IndexOf(m_SelectedPin);
            if (index > -1)
            {
                int graphHeight = getGraphHeight();
                Point[] pts = { new Point(0, 40) };
                Matrix mat = new Matrix();
                mat.Scale(m_xScale + 1f, m_yScale + 1f);
                mat.TransformPoints(pts);
                Point labelStart = new Point(graphHeight * ((index - vs_ScrollBar.Value) + 1) - pts[0].Y,
                                             graphHeight * ((index - vs_ScrollBar.Value) + 1) + pts[0].Y);

                int b = labelStart.Y - labelStart.X;
                float n = b / 2f;

                float c1 = y - labelStart.X;

                v = n - c1;
                v = v / n * m_SelectedPin.getMaxVolt();
            }
            
            return v;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Draws the header, footer, graph, all of the pins,
        /// the cursors and the TimeSpan measurement.  Also, updates
        /// the scroll bar values.</summary>
        ///
        /// <param name="e"><see cref="PaintEventArgs"/>.</param>
        /////////////////////////////////////////////////////////
        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO : this is being called 2 times when scrolling.
            base.OnPaint(e);
            DateTime startTime = DateTime.Now;
            if (m_ViewChanged)
            {
                newRenderer();
                m_ViewChanged = false;
            }
            DateTime endTime = DateTime.Now;
            TimeSpan ts2 = endTime - startTime;
            TimeSample duration = new TimeSample((float)(ts2.TotalMilliseconds), TimeScale.MILLISECONDS);
            //System.Diagnostics.Debug.WriteLine("Paint: " + getTimeAsString(duration, TimeScale.MILLISECONDS));
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Computes the height of the area that will consist of
        /// the graphed pin data.
        /// </summary>
        /// 
        /// <returns>The height of the graphed pin data.</returns>
        /////////////////////////////////////////////////////////
        protected int getGraphHeight()
        {
            int graphHeight = 80;

            Point[] tps = { new Point(0, graphHeight) };
            Matrix mat = new Matrix();
            mat.Scale(m_xScale + 1, m_yScale + 1f);
            mat.TransformPoints(tps);
            return tps[0].Y + 30;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Calculates the time at the left hand side of the viewable
        /// graph.
        /// </summary>
        /// 
        /// <returns></returns>
        /////////////////////////////////////////////////////////
        protected TimeSample getViewableStartTime()
        {
            return new TimeSample(hs_ScrollBar.Value, TimeScale.PICOSECONDS);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Calculates the time at the right hand side of the viewable
        /// graph.
        /// </summary>
        /// 
        /// <returns></returns>
        /////////////////////////////////////////////////////////
        protected TimeSample getViewableEndTime()
        {
            TimeSample ts = convertXtoTime(m_DrawWindow.Width);
            return ts;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// This is the new render engine.  It pushes the rendering
        /// to each of the objects that need to be rendered.  Each
        /// object is responsible for passing back an image to be
        /// placed on the screen.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected void newRenderer()
        {
            if (m_Theme == null || m_Pins == null)
                return;

            if (m_CacheRender == null || m_ViewChanged)
            {
                m_GraphCacheVolt = -99;
                Bitmap bmp = new Bitmap(m_DrawWindow.Width, m_DrawWindow.Height);
                Graphics gp = Graphics.FromImage(bmp);
                Matrix mat = new Matrix();
                mat.Scale(m_xScale + 1, m_yScale + 1f);
                gp.FillRectangle(m_Theme.getBackground(), 0, 0, m_DrawWindow.Width, m_DrawWindow.Height);

                int graphHeight = getGraphHeight();
                Point[] yHeight = { new Point(0, 40) };
                mat.TransformPoints(yHeight);

                TimeSample endTime = getViewableEndTime();

                Point[] pts = { new Point(10, 10) };
                mat.TransformPoints(pts);
                setMouseCursor(m_GraphOffset + 10, pts[0].X);

                // Draw Pins
                // Translate in the y the (graphHeight/2) + m_HeaderSpace
                // but should only apply to Draw Pins
                Matrix matTemp = new Matrix();
                matTemp.Translate(0, (graphHeight/2) + m_HeaderSpace);
                gp.Transform = matTemp;
                int pinIndex = vs_ScrollBar.Value;
                Image imgLabel = null;
                TimeSample startTime = getViewableStartTime();
                for (int index = vs_ScrollBar.Value; index < m_Pins.Count; index++)
                {
                    Pin p = m_Pins[index];

                    Point labelStart = new Point(0, graphHeight * ((pinIndex - vs_ScrollBar.Value)));

                    if (labelStart.Y - 17 > m_DrawWindow.Height)
                        break;

                    //try
                    {
                        // Draw Pin Selection
                        bool selected = false;
                        if (m_SelectedPin != null &&
                            m_SelectedPin.Equals(p))
                        {
                            selected = true;
                        }
                        // Draw Pin label
                        //PerfMon.start();

                        imgLabel = p.drawLabel(m_Theme, selected, new Matrix());
                        if (imgLabel != null)
                        {
                            if (m_GraphMode == GraphMode.MODE_SPLIT)
                            {
                                String temp = "Pattern";
                                int pStart = labelStart.Y - 8;
                                int rStart = graphHeight * ((pinIndex + 1 - vs_ScrollBar.Value)) - 8;

                                gp.DrawString(temp, m_Theme.getFont(), m_Theme.getFontColor(),
                                              new Point((int)(imgLabel.Width - gp.MeasureString(temp, m_Theme.getFont()).Width),
                                                        pStart));
                                int lStart = (int)(pStart + ((rStart - pStart) / 2f));
                                gp.DrawImage(imgLabel, new Point(0, lStart));
                                temp = "Response";
                                gp.DrawString(temp, m_Theme.getFont(), m_Theme.getFontColor(),
                                              new Point((int)(imgLabel.Width - gp.MeasureString(temp, m_Theme.getFont()).Width),
                                                        rStart));
                            }
                            else
                                gp.DrawImage(imgLabel, new Point(0, labelStart.Y - (int)(imgLabel.Height / 2f)));
                        }

                        Point start = new Point(imgLabel.Width + 10, labelStart.Y - yHeight[0].Y);
                        //PerfMon.stop("Pin label");

                        // Draw Graph
                        //PerfMon.start();

                        Image imgGraph = drawGraph(p.getMaxVolt(), mat, p.isDigital());
                        if (imgGraph != null)
                            gp.DrawImage(imgGraph, new Point(start.X - 60, start.Y - yHeight[0].Y));

                        //PerfMon.stop("Graph");
                        // Draw Pin Signal
                        if (m_GraphMode != GraphMode.MODE_STANDARD_RESPONSE)
                        {
                            //PerfMon.start();
                            Image imgPattern = p.drawPattern(startTime,
                                                             endTime,
                                                             m_TimeScale,
                                                             mat);
                            if (imgPattern != null)
                                gp.DrawImage(imgPattern, start);
                            //PerfMon.stop("Pin Pattern");
                        }
                        if (m_GraphMode != GraphMode.MODE_STANDARD_PATTERN)
                        {
                            //PerfMon.start();
                            Image imgResponse = p.drawResponse(startTime,
                                                              endTime,
                                                              m_TimeScale,
                                                              mat);
                            if (imgResponse != null)
                            {
                                if (m_GraphMode == GraphMode.MODE_SPLIT)
                                {
                                    start = new Point(start.X, graphHeight * ((++pinIndex - vs_ScrollBar.Value)) - yHeight[0].Y);
                                    imgGraph = drawGraph(p.getMaxVolt(), mat, p.isDigital());
                                    if (imgGraph != null)
                                        gp.DrawImage(imgGraph, new Point(start.X - 60,
                                                                         start.Y - yHeight[0].Y));
                                }
                                else if (m_GraphMode == GraphMode.MODE_OVERLAY)
                                {
                                    start = new Point(start.X + m_OverlayOffset.X, start.Y - m_OverlayOffset.Y);
                                }
                                gp.DrawImage(imgResponse, start);
                            }
                            //PerfMon.stop("Pin Response");
                        }
                        // Draw Mouse Measurement
                        // Draw Strobes
                        //PerfMon.start();
                        if (m_ShowStrobes)
                        {
                            Image imgStrobes = p.drawStrobes(m_Theme,
                                                             startTime,
                                                             endTime,
                                                             m_TimeScale, m_ShowStrobeFailState,
                                                             mat);
                            if (imgStrobes != null)
                                gp.DrawImage(imgStrobes, start);
                        }
                        //PerfMon.stop("Strobes");
                    }
                    pinIndex++;
                }
                gp.Transform = new Matrix();
                updateScrollBars();
                // Draw Header
                //PerfMon.start();
                int headerIndex = 0;
                foreach (Header h in m_HeaderRows)
                {
                    if (headerIndex > 2)
                        break;
                    Image img = h.draw(m_Theme,
                                       startTime,
                                       endTime,
                                       m_TimeScale,
                                       mat);
                    Point pt = new Point(imgLabel.Width + 10, (2 + (15 * headerIndex)));
                    gp.DrawString(h.getName(), m_Theme.getFont(), m_Theme.getFontColor(),
                                  new Point((int)(pt.X - 5 - gp.MeasureString(h.getName(), m_Theme.getFont()).Width), pt.Y));
                    if (img != null && imgLabel != null)
                        gp.DrawImage(img, pt);
                    headerIndex++;
                }
                //PerfMon.stop("Header");

                // Draw Footer
                //PerfMon.start();
                if (imgLabel != null)
                {
                    Image img = drawFooter(imgLabel.Width + 10);
                    if (img != null)
                        gp.DrawImage(img, new Point(0,
                                                m_FooterSpace));
                }
                //PerfMon.stop("Footer");
                gp.Flush();
                m_CacheRender = (Bitmap)bmp.Clone();
            }

            pbx_Waveform.Image = m_CacheRender;
            drawLiveCursors();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the footer information to an image object.
        /// </summary>
        /// 
        /// <param name="x_offset">The offset denoting where the
        /// start of the graphs are.</param>
        /// 
        /// <returns>Returns an image with the rendered footer
        /// information on it.</returns>
        /////////////////////////////////////////////////////////
        protected Image drawFooter(int x_offset)
        {
            Image img = new Bitmap(m_DrawWindow.Width, m_DrawWindow.Height - m_FooterSpace);
            Graphics gp = Graphics.FromImage(img);

            gp.FillRectangle(m_Theme.getBackground(), 0, 0, m_DrawWindow.Width, m_DrawWindow.Height - m_FooterSpace);
            String temp = "Time -";
            gp.DrawString(temp, m_Theme.getFont(), m_Theme.getFontColor(),
                          x_offset - (temp.Length * m_Theme.getFont().Size), 3);
            gp.DrawLine(m_Theme.getGraphColor(), new Point(x_offset, 0), new Point(m_DrawWindow.Width, 0));
            Point lastPoint = new Point(-100, -1);
            String lastString = "";
            int xValue = 0;
            for (int n = (int)getViewableStartTime().getTime(m_TimeScale); xValue < m_DrawWindow.Width; n++)
            {
                TimeSample ts = new TimeSample(n, m_TimeScale);
                xValue = (int)(x_offset + convertTimeToX(ts));
                if (xValue < x_offset)
                    continue;
                // hash marks
                gp.DrawLine(m_Theme.getGraphColor(),
                        new Point(xValue, 0),
                        new Point(xValue, 5));
                if (n % m_GraphStepping == 0 &&
                    xValue > lastPoint.X + gp.MeasureString(lastString, m_Theme.getFont()).Width)
                {
                    lastString = getTimeAsString(ts, m_TimeScale);

                    // Unit measure
                    gp.DrawString(lastString,
                                  m_Theme.getFont(),
                                  m_Theme.getFontColor(),
                                  xValue - (lastString.Length / 2) * m_Theme.getFont().Size,
                                  10);
                    lastPoint.X = xValue;
                }
            }
            return img;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws a cursor line along with the current volt/time
        /// value following the mouse movement.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected void drawLiveCursors()
        {
            if (m_GraphMode == GraphMode.MODE_SPLIT &&
                m_CursorMode == CursorMode.TOP_BOTTOM)
                return;
            Bitmap img = (Bitmap)m_CacheRender.Clone();
            Graphics g = Graphics.FromImage(img);
            String temp = "";

            Matrix mat = new Matrix();
            mat.Scale(m_xScale + 1, m_yScale + 1f);

            if (true) //m_MouseState.m_Button == MouseButtons.None)
            {
                Point[] pts = { new Point(10, 10) };
                mat.TransformPoints(pts);
                setMouseCursor(m_GraphOffset + 10, pts[0].X);

                if (m_CursorMode == CursorMode.LEFT_RIGHT)
                {
                    TimeSample startTime = getViewableStartTime();
                    for (int h = 0; h < 2; h++)
                    {
                        if (m_Cursors[h] != new Point(-1, -1))
                        {
                            TimeSample ts = new TimeSample(m_Cursors[h].X, TimeScale.PICOSECONDS);
                            // x isn't the actual x pos.  it is the time value.
                            int tx = (int)(m_GraphOffset + ((ts - startTime).getTime(m_TimeScale) * pts[0].X));
                            if (tx < m_GraphOffset)
                                continue;
                            g.DrawLine(m_Theme.getCursorColor(),
                                        new Point(tx, m_HeaderSpace),
                                        new Point(tx, m_FooterSpace));
                        }
                    }
                }
                else if (m_CursorMode == CursorMode.TOP_BOTTOM)
                {
                    for (int h = 2; h < 4; h++)
                        if (m_Cursors[h] != new Point(-1, -1))
                        {
                            // Draw Horizontal Cursors
                            // check for within pin range
                            int index = m_Pins.IndexOf(m_SelectedPin);
                            if (index > -1)
                            {
                                int height = 40;
                                float value = m_Cursors[h].Y;
                                int y = (-1) * (int)(value / m_SelectedPin.getMaxVolt() * height);
                                Point[] pt = { new Point(0, y) };
                                mat.TransformPoints(pt);

                                Point labelStart = new Point(0, getGraphHeight() * ((index - vs_ScrollBar.Value) + 1));
                                //if (labelStart.Y - yHeight[0].Y <= pt[0].Y &&
                                //    labelStart.Y + yHeight[0].Y >= pt[0].Y)
                                {
                                    // TODO : support y zoom
                                    g.DrawLine(m_Theme.getCursorColor(),
                                                new Point(m_GraphOffset + 10, labelStart.Y + pt[0].Y),
                                                new Point(m_DrawWindow.Width, labelStart.Y + pt[0].Y));
                                }
                            }
                        }
                } 
            }
            else
            {
                if (m_CursorMode == CursorMode.LEFT_RIGHT)
                {
                    temp = getTimeAsString(convertXtoTime(m_MouseState.m_Location.X), m_TimeScale);
                    //g.DrawLine(m_Theme.getCursorColor(),
                    //            new Point(m_MouseState.m_Location.X, m_HeaderSpace),
                    //            new Point(m_MouseState.m_Location.X, m_FooterSpace));
                }
                else if (m_CursorMode == CursorMode.TOP_BOTTOM && m_SelectedPin != null)
                {
                    temp = convertYToVolts(m_MouseState.m_Location.Y) + "v";
                    //g.DrawLine(m_Theme.getCursorColor(),
                    //            new Point(m_GraphOffset + 10, m_MouseState.m_Location.Y),
                    //            new Point(m_DrawWindow.Width, m_MouseState.m_Location.Y));
                }
                Point pt = new Point(m_MouseState.m_Location.X, m_MouseState.m_Location.Y);
                pt.Y = (int)(m_MouseState.m_Location.Y - (m_Theme.getFont().Size * 2));
                g.FillRectangle(m_Theme.getCursorColor().Brush,
                    new Rectangle(pt, new Size((int)(g.MeasureString(temp, m_Theme.getFont()).Width),
                                               (int)(m_Theme.getFont().Size * 1.5))));
                g.DrawString(temp, m_Theme.getFont(), m_Theme.getFontColor(), pt);
            }

            Image img1 = drawCursorsStatus(mat);
            if (img1 != null)
                g.DrawImage(img1, new Point(m_GraphOffset + 10,
                                        m_FooterSpace + 28));

            g.Flush();

            pbx_Waveform.Image = img;

        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the status message when two cursors are displayed.
        /// This status message consists of the first cursor's
        /// postion, the second cursor's position, and the delta
        /// between the two cursors.
        /// </summary>
        /// 
        /// <param name="mat">A matrix containing the current
        /// scaling.</param>
        /// 
        /// <returns>Returns an image with the cursor status
        /// rendered.</returns>
        /////////////////////////////////////////////////////////
        protected Image drawCursorsStatus(Matrix mat)
        {
            Image statusBmp = null;
            // The following is related to cursors.
            // Number format String.Format("{0:0.00}", num)
            // Resolution - space between Ticks
            // Left/Right(Top/Bottom) - the viewable time span
            // delta - right/bottom minus left/top
            if (m_CursorMode == CursorMode.LEFT_RIGHT &&
                m_Cursors[CURSOR_RIGHT] != new Point(-1, -1) &&
                m_Cursors[CURSOR_LEFT] != new Point(-1, -1))
            {
                // Resolution: #.##ns    Left: #.##ns   Right: #.##ns   Delta:  #.##ns
                // Status: [                     status text here                                 ]
                // x isn't the actual x pos.  it is the time value.
                float leftValue = m_Cursors[CURSOR_LEFT].X;
                float rightValue = m_Cursors[CURSOR_RIGHT].X;
                TimeSample leftSample = new TimeSample(leftValue, TimeScale.PICOSECONDS);
                TimeSample rightSample = new TimeSample(rightValue, TimeScale.PICOSECONDS);

                if (leftSample > rightSample)
                {
                    TimeSample temp = leftSample;
                    leftSample = rightSample;
                    rightSample = temp;
                }

                TimeSample tsTemp = rightSample - leftSample;
                if (tsTemp.getTime(TimeScale.SECONDS) < 0)
                    tsTemp *= new TimeSample(-1, TimeScale.PICOSECONDS);
                // Set the time scale properly.
                String status = String.Format("Resolution: {0}    Left: {1}   Right: {2}   Delta:  {3}",
                    getTimeAsString(new TimeSample(1, m_TimeScale), m_TimeScale, "{0:0.00}{1}"),
                    getTimeAsString(leftSample, m_TimeScale, "{0:0.00}{1}"),
                    getTimeAsString(rightSample, m_TimeScale, "{0:0.00}{1}"),
                    getTimeAsString(tsTemp, m_TimeScale, "{0:0.00}{1}"));
                statusBmp = new Bitmap((int)(m_Theme.getFont().Size * status.Length + 2), (int)(m_Theme.getFont().Size + 5));
                Graphics g = Graphics.FromImage(statusBmp);
                g.DrawString(status, m_Theme.getFont(), m_Theme.getFontColor(), 0, 0);
                g.Flush();
            }
            else if (m_CursorMode == CursorMode.TOP_BOTTOM &&
                     m_Cursors[CURSOR_TOP] != new Point(-1, -1) &&
                     m_Cursors[CURSOR_BOTTOM] != new Point(-1, -1) &&
                     m_SelectedPin != null)
            {
                // TODO : Find the offset for the V/A values.
                float topValue = 0f;
                float bottomValue = 0f;

                if (m_Cursors[CURSOR_BOTTOM].Y > m_Cursors[CURSOR_TOP].Y)
                {
                    topValue = m_Cursors[CURSOR_BOTTOM].Y;
                    bottomValue = m_Cursors[CURSOR_TOP].Y;
                }
                else
                {
                    bottomValue = m_Cursors[CURSOR_BOTTOM].Y;
                    topValue = m_Cursors[CURSOR_TOP].Y;
                }

                String status = String.Format("Resolution: {0}    Top: {1}   Bottom: {2}   Delta:  {3}",
                    m_SelectedPin.getMaxVolt() + "v",
                    String.Format("{0:0.00}", topValue) + "v",
                    String.Format("{0:0.00}", bottomValue) + "v",
                    String.Format("{0:0.00}", Math.Abs(bottomValue - topValue)) + "v");
                statusBmp = new Bitmap((int)(m_Theme.getFont().Size * status.Length + 2), (int)(m_Theme.getFont().Size + 5));
                Graphics g = Graphics.FromImage(statusBmp);
                g.DrawString(status, m_Theme.getFont(), m_Theme.getFontColor(), 0, 0);
                g.Flush();
            }
            return statusBmp;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Overloaded function for setMouseCursor(int x_offset, float set)
        /// This function calculates the matrix with scaling.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected void setMouseCursor()
        {
            Point[] pts = { new Point(10, 10) };
            Matrix mat = new Matrix();
            mat.Scale(m_xScale + 1f, m_yScale + 1f);
            mat.TransformPoints(pts);
            setMouseCursor(m_GraphOffset, pts[0].X);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the cursor values based on the mouse location.
        /// </summary>
        /// 
        /// <param name="x_offset">The start position of the
        /// graphs.</param>
        /// 
        /// <param name="step">The size in pixels of the step
        /// between each of the tick marks.</param>
        /////////////////////////////////////////////////////////
        protected void setMouseCursor(int x_offset, float step)
        {
            m_SelectedPinPos = new Point(-1, -1);
            if (m_MouseState.m_Button == MouseButtons.Left)
            {
                setSelectedPin();
            } 
            
            Point scrn = this.PointToScreen(pbx_Waveform.Location);
            scrn.Y += m_HeaderSpace;
            scrn.X += m_GraphOffset;
            Rectangle pbx = new Rectangle(scrn, pbx_Waveform.Size);
            pbx.Height -= (m_HeaderSpace + (m_DrawWindow.Height - m_FooterSpace) - 15);
            pbx.Width -= m_GraphOffset;
            Point mouse = this.PointToScreen(m_MouseState.m_Location);

            if (m_MouseState.m_Button == MouseButtons.Left &&
                m_MouseState.m_Location.X > x_offset &&
                pbx.Contains(mouse))
            {
                //if (m_CursorMode == CursorMode.LEFT_RIGHT)
                {
                    // TODO : Fix the time calculation
                    float sl = m_MouseState.m_Location.X - x_offset;
                    float ls = sl / step;
                    TimeSample value = convertXtoTime(m_MouseState.m_Location.X); // this value is the time sample translated from the mouse position.

                    bool first = false;
                    if (m_Cursors[CURSOR_LEFT] == new Point(-1, -1) ||
                        m_CursorMode == CursorMode.OFF)
                    {
                        first = true;
                    }
                    else if (m_Cursors[CURSOR_RIGHT] == new Point(-1, -1))
                    {
                    }
                    else // calc the distance from the mouse
                    {
                        int xLeftDist = (int)Math.Abs(m_Cursors[CURSOR_LEFT].X - value.getTime());
                        int xRightDist = (int)Math.Abs(m_Cursors[CURSOR_RIGHT].X - value.getTime());
                        if (xLeftDist < xRightDist)
                            first = true;
                    }
                    if (first)
                    {
                        m_Cursors[CURSOR_LEFT].X = (int)value.getTime();
                        m_Cursors[CURSOR_LEFT].Y = m_MouseState.m_Location.Y;
                    }
                    else if (!first)
                    {
                        m_Cursors[CURSOR_RIGHT].X = (int)value.getTime();
                        m_Cursors[CURSOR_RIGHT].Y = m_MouseState.m_Location.Y;
                    }
                }
                if (m_SelectedPin != null &&
                    m_GraphMode != GraphMode.MODE_SPLIT)
                {
                    float value = convertYToVolts(m_MouseState.m_Location.Y);
                    //System.Diagnostics.Debug.WriteLine("Cursor value: " + value);
                    bool first = false;
                    if (m_Cursors[CURSOR_TOP] == new Point(-1, -1) ||
                        m_CursorMode == CursorMode.OFF)
                    {
                        first = true;
                    }
                    else if (m_Cursors[CURSOR_BOTTOM] == new Point(-1, -1))
                    {
                    }
                    else // calc the distance from the mouse
                    {
                        int yTopDist = (int)Math.Abs(m_Cursors[CURSOR_TOP].Y - value);
                        int yBottomDist = (int)Math.Abs(m_Cursors[CURSOR_BOTTOM].Y - value);
                        if (yTopDist < yBottomDist)
                            first = true;
                    }
                    if (m_MouseState.m_Location.Y <= m_SelectedPinPos.Y &&
                        m_MouseState.m_Location.Y >= m_SelectedPinPos.X)
                    {
                        if (first)
                        {
                            m_Cursors[CURSOR_TOP].X = m_MouseState.m_Location.X;
                            m_Cursors[CURSOR_TOP].Y = value;
                        }
                        else if (!first)
                        {
                            m_Cursors[CURSOR_BOTTOM].X = m_MouseState.m_Location.X;
                            m_Cursors[CURSOR_BOTTOM].Y = value;
                        }
                    }
                }
            }
            // If this is commented out, any redraws will draw the cursor where the mouse is.
            if(m_MouseState.m_State == MouseButtonState.UP)
                m_MouseState.m_Button = MouseButtons.None;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Based on the mouse click location, this function
        /// determines which pin was selected.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected void setSelectedPin()
        {
            m_SelectedPinPos = m_MouseState.m_Location;
            int graphHeight = getGraphHeight();
            Point[] yHeight = { new Point(0, 40) };
            Matrix mat = new Matrix();
            mat.Scale(m_xScale + 1, m_yScale + 1f);
            mat.TransformPoints(yHeight);

            {
                int pinIndex = m_Pins.IndexOf(m_SelectedPin);
                Point labelStart = new Point(0, graphHeight * ((pinIndex - vs_ScrollBar.Value) + 1));
                m_SelectedPinPos.X = labelStart.Y - yHeight[0].Y;
                m_SelectedPinPos.Y = labelStart.Y + yHeight[0].Y;
            }
            if (m_MouseState.m_State != MouseButtonState.DOWN)
            {
                bool test = true;
                int pinIndex = vs_ScrollBar.Value;
                for (int index = vs_ScrollBar.Value; index < m_Pins.Count; index++)
                {
                    Pin p = m_Pins[index];

                    Point labelStart = new Point(0, graphHeight * ((pinIndex - vs_ScrollBar.Value) + 1));
                    if (m_SelectedPinPos != new Point(-1, -1))
                    {
                        int split = 0;
                        if (m_GraphMode == GraphMode.MODE_SPLIT)
                        {
                            split = graphHeight;
                            pinIndex++;
                        }
                        if (labelStart.Y - yHeight[0].Y <= m_MouseState.m_Location.Y &&
                           labelStart.Y + yHeight[0].Y + split >= m_MouseState.m_Location.Y)
                        {
                            test = false;
                            if (m_SelectedPin != p)
                            {
                                if (m_CursorMode == CursorMode.TOP_BOTTOM)
                                    clearCursors();
                                setSelectedPin(p);
                                m_SelectedPinPos.X = labelStart.Y - yHeight[0].Y;
                                m_SelectedPinPos.Y = labelStart.Y + yHeight[0].Y;
                            }
                            break;
                        }
                        pinIndex++;
                    }
                }
                if (test)
                    m_SelectedPin = null;
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// This will allow for the caching of the graph.  Since
        /// the graph will be the same for every pin, unless the
        /// maxVolt is different, then why draw it for each pin.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected Bitmap m_GraphCache;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Controls whether or not the graph cache will be used.
        /// The maxVolt is the only thing that changes from pin
        /// to pin.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected float m_GraphCacheVolt;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Draws the graph for which the pin data will be displayed
        /// apon.  
        /// </summary>
        /// 
        /// <param name="maxVolt">The max volts for the pin that will
        /// be displayed.</param>
        /// <param name="mat">A matrix containing the current
        /// scaling.</param>
        /// <param name="isDigital">Denotes where a H/L for digital or
        /// the min/max volts will be displayed for analog.</param>
        /// 
        /// <returns>Returns an image with the graph rendered.</returns>
        /////////////////////////////////////////////////////////
        protected Image drawGraph(float maxVolt, Matrix mat, bool isDigital)
        {
            Bitmap img = m_GraphCache;
            if (maxVolt != m_GraphCacheVolt)
            {
                int x = 60;
                int scale = 40;

                //Point[] scaledY = { new Point(y - scale, y + scale) };
                //mat.TransformPoints(scaledY);

                try
                {
                    Point[] pts = { new Point(0, 140) };
                    mat.TransformPoints(pts);
                    img = new Bitmap(m_DrawWindow.Width, pts[0].Y);
                }
                catch (Exception e)
                {
                    // Do to garbage collection this catches an out of memory error.
                    System.Diagnostics.Debug.WriteLine("TEV_Waveform - Draw Graph: " + e.Message);
                }
                Graphics gp = Graphics.FromImage(img);

                int y = 80; // (int)(graphHeight / 2f);
                Point[] scaledPoints = {new Point(0, y - scale),  // Top of graph
                                        new Point(0, y),  // Center of graph
                                        new Point(0, y + scale)};  // Bottom of graph
                mat.TransformPoints(scaledPoints);

                gp.DrawLine(m_Theme.getGraphColor(), x, scaledPoints[0].Y, x, scaledPoints[2].Y);
                gp.DrawLine(m_Theme.getGraphColor(), x, scaledPoints[1].Y, m_DrawWindow.Width, scaledPoints[1].Y);

                // Y graph values
                for (float q = -1; q < 1.5; q = q + 0.5f)
                {
                    Point[] pts = { new Point(0, (y - (int)(scale * q))) };
                    mat.TransformPoints(pts);
                    gp.DrawLine(m_Theme.getGraphColor(),
                                x,
                                pts[0].Y,
                                x - 5,
                                pts[0].Y);
                }

                int inverse = -1;
                String temp = "";
                if(isDigital)
                    temp = "H";
                else
                    temp = maxVolt + "v";
                gp.DrawString(temp,
                              m_Theme.getFont(),
                              m_Theme.getFontColor(),
                              x - gp.MeasureString(temp, m_Theme.getFont()).Width - 5,
                              scaledPoints[0].Y - m_Theme.getFont().Size);

                if(isDigital)
                    temp = "L";
                else
                    temp = "-" + maxVolt + "v";
                gp.DrawString(temp,
                              m_Theme.getFont(),
                              m_Theme.getFontColor(),
                              x - gp.MeasureString(temp, m_Theme.getFont()).Width - 5,
                              scaledPoints[2].Y - 7);

                // X graph ticks
                Point[] tps = { new Point(0, 150) };
                mat.TransformPoints(tps);
                Bitmap img2 = new Bitmap(m_DrawWindow.Width - x, tps[0].Y);
                Graphics gp2 = Graphics.FromImage(img2);
                //gp2.Transform = mat;
                for (int i = 0; i < m_DrawWindow.Width; i++)
                {
                    Point[] pts = {new Point(i * 10, y)};  // Scale tick mark?
                    mat.TransformPoints(pts);
                    gp2.DrawLine(m_Theme.getGraphColor(),
                                pts[0].X, pts[0].Y,
                                pts[0].X, pts[0].Y + 5);
                    if (pts[0].X > m_DrawWindow.Width)
                        break;
                }
                gp.DrawImage(img2, new Point(x, 0));
                m_GraphCache = img;
                m_GraphCacheVolt = maxVolt;
            }
            return img;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Used to generate a string representation of
        /// the given time sample.  The string will be appended
        /// with the given time scale unit.</summary>
        ///
        /// <param name="ticks"/>The time measurement represented
        /// as a <see cref="TimeSample"/> object.</param>
        /// <param name="timeScale">The desired <see cref="TimeScale"/>.
        /// </param>
        ///
        /// <returns>Returns a string with the time measurement
        /// appended with the desired unit.
        /// </returns>
        /////////////////////////////////////////////////////////
        protected String getTimeAsString(TimeSample ticks, TimeScale timeScale)
        {
            return getTimeAsString(ticks, timeScale, "{0}{1}");
        }

        /////////////////////////////////////////////////////////
        /// <summary>Used to generate a string representation of
        /// the given time sample.  The string will be appended
        /// with the given time scale unit.</summary>
        ///
        /// <param name="ticks"/>The time measurement represented
        /// as a <see cref="TimeSample"/> object.</param>
        /// <param name="timeScale">The desired <see cref="TimeScale"/>.
        /// </param>
        /// <param name="format">A <see cref="String"/> representing
        /// the format to use to create the string.  This is the
        /// standard .net string format.</param>
        ///
        /// <returns>Returns a string with the time measurement
        /// appended with the desired unit.
        /// </returns>
        /////////////////////////////////////////////////////////
        protected String getTimeAsString(TimeSample ticks, TimeScale timeScale, String format)
        {
            String time = "";
            switch (timeScale)
            {
                case TimeScale.SECONDS:
                    time = String.Format(format, ticks.getTime(TimeScale.SECONDS), "s");
                    break;
                case TimeScale.MILLISECONDS:
                    time = String.Format(format, ticks.getTime(TimeScale.MILLISECONDS), "ms");
                    break;
                case TimeScale.MICROSECONDS:
                    time = String.Format(format, ticks.getTime(TimeScale.MICROSECONDS), "us");
                    break;
                case TimeScale.NANOSECONDS:
                    time = String.Format(format, ticks.getTime(TimeScale.NANOSECONDS), "ns");
                    break;
                case TimeScale.PICOSECONDS:
                    time = String.Format(format, ticks.getTime(TimeScale.PICOSECONDS), "ps");
                    break;
            }
            return time;
        }

        /////////////////////////////////////////////////////////
        /// <summary>updates the display based on the new scroll
        /// bar position.</summary>
        ///
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void vs_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            calcViewable();
        }

        /////////////////////////////////////////////////////////
        /// <summary>updates the display based on the new scroll
        /// bar position.</summary>
        ///
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void hs_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            calcViewable();
        }

        /////////////////////////////////////////////////////////
        /// <summary>updates the display based on the new window
        /// size.</summary>
        ///
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void WaveformCtrl_Resize(object sender, EventArgs e)
        {
            calcViewable();
        }

        /////////////////////////////////////////////////////////
        /// <summary>Captures the mouse location and forces a redraw
        /// of the control.  When the redraw occurs, the mouse button
        /// state is taken into account and draws the cursors.</summary>
        ///
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void pbx_Waveform_MouseMove(object sender, MouseEventArgs e)
        {
            m_MouseState.m_Location = e.Location;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Captures the mouse button state. When the
        /// redraw occurs, the mouse button state is taken into
        /// account and draws the cursors.  Left button draws the
        /// left cursor and the right button draws the right cursor.
        /// </summary>
        ///
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void pbx_Waveform_MouseDown(object sender, MouseEventArgs e)
        {
            m_MouseState.m_Button = e.Button;
            m_MouseState.m_State = MouseButtonState.DOWN;
            tmr_MouseDrag.Enabled = true;
            MouseEvent.sendMouseEvent(this.Handle, e, true);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Created this function to perform debug testing to
        /// determine the call stack.
        /// </summary>
        /// 
        /// <param name="e"></param>
        /////////////////////////////////////////////////////////
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        /////////////////////////////////////////////////////////
        /// <summary>Releases the mouse button state stopping the
        /// redraw of the cursors.</summary>
        ///
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void pbx_Waveform_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseState.m_Button = e.Button;
            m_MouseState.m_State = MouseButtonState.UP;
            tmr_MouseDrag.Enabled = false;
            m_ViewChanged = true;
            Invalidate();
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Created this function to perform debug testing to
        /// determine the call stack.
        /// </summary>
        /// 
        /// <param name="e"></param>
        /////////////////////////////////////////////////////////
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MouseEvent.sendMouseEvent(pbx_Waveform.Handle, e, false);
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Event listener to capture the mouse wheel event. Scrolls
        /// the window on the change of the mouse wheel.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void pbx_Waveform_MouseWheel(object sender, MouseEventArgs e)
        {
            int value = vs_ScrollBar.Value - e.Delta/120;
            if (value >= vs_ScrollBar.Minimum &&
                value <= vs_ScrollBar.Maximum)
            {
                vs_ScrollBar.Value = value;
                m_ViewChanged = true;
                Invalidate();
            }
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// A timer event function to update the dragging of the
        /// mouse.  For some reason the mouse drag events don't
        /// register.  This is enabled when the left mouse button
        /// is held down.
        /// </summary>
        /// 
        /// <param name="sender">The control that called this
        /// function.</param>
        /// <param name="e">Any parameters that the calling control
        /// wants to pass to this function.</param>
        /////////////////////////////////////////////////////////
        private void tmr_MouseDrag_Tick(object sender, EventArgs e)
        {
            m_MouseState.m_Location.X = MousePosition.X;
            m_MouseState.m_Location.Y = MousePosition.Y;
            Point pt = new Point(m_MouseState.m_Location.X, m_MouseState.m_Location.Y);
            pt = pbx_Waveform.PointToClient(pt);
            m_MouseState.m_Location = pt;
            drawLiveCursors();
        }
    }
}