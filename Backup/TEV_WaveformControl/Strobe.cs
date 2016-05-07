using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TEV_WaveformControl
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// Represents a strobe marker on the graph.  A strobe is
    /// a marker that shows the direction that the signal
    /// should be going in.  This class also supports a window
    /// for the strobe.  If it is not this class has a
    /// fail flag that will need to be set to indicate that
    /// the fail has occurred.  This class does not check
    /// for signal state.
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
    ///         <description>0.1</description>
    ///     </item>
    /// </list>
    /////////////////////////////////////////////////////////
    public class Strobe
    {
        // Strobe States
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Represents the states that this strobe can be in.
        /// </summary>
        /////////////////////////////////////////////////////////
        public enum StrobeState
        {
            /////////////////////////////////////////////////////////
            /// <summary>Denotes that the strobe should indicate a 
            /// rising state of the pin.</summary>
            /////////////////////////////////////////////////////////
            RISING = 1,
            HIGH = 1,
            /////////////////////////////////////////////////////////
            /// <summary>Denotes that the strobe should indicate a 
            /// falling state of the pin.</summary>
            /////////////////////////////////////////////////////////
            FALLING = -1,
            LOW = -1,
            /////////////////////////////////////////////////////////
            /// <summary>Denotes that the strobe should indicate
            /// either a rising or falling state of the pin.</summary>
            /////////////////////////////////////////////////////////
            BOTH = 0,
            VALID = 0,
            /////////////////////////////////////////////////////////
            /// <summary>
            /// Denotes that the strobe should indicate
            /// mid value state of the pin.
            /// </summary>
            /////////////////////////////////////////////////////////
            MIDBAND = 2
        };

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes when the strobe should start looking for the
        /// desired state.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected TimeSample m_StartTime;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes when the strobe should stop looking for the
        /// desired state.  This is set to the start time initially.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected TimeSample m_EndTime;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The direction of that the strobe is looking for.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected StrobeState m_State;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not the signal state failed to be
        /// the same as the strobe.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected bool m_Failed;
        /////////////////////////////////////////////////////////
        /// <summary>The color of the strobe indicators that will
        /// be displayed to the user when a fail occurred.</summary>
        /////////////////////////////////////////////////////////
        protected Pen m_StrobeFailColor; // Strobe color.
        /////////////////////////////////////////////////////////
        /// <summary>The color of the strobe indicators that will
        /// be displayed to the user when a fail occurred.</summary>
        /////////////////////////////////////////////////////////
        protected Pen m_StrobePassColor; // Strobe color.

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the <see cref="TimeSample"/> representing when this
        /// strobe starts.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="TimeSample"/> representing
        /// when this strobe starts.</returns>
        /////////////////////////////////////////////////////////
        public TimeSample getStartTime()
        {
            return m_StartTime;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets where the strobe will start based on the provided
        /// <see cref="TimeSample"/>.
        /// </summary>
        /// 
        /// <param name="sample">The <see cref="TimeSample"/> indicating
        /// when this strobe occurs.</param>
        /// 
        /// <returns>Returns 0 for success and -1 to indicate an
        /// error has occurred.</returns>
        /////////////////////////////////////////////////////////
        public int setStartTime(TimeSample sample)
        {
            int status = -1;
            if (sample != null)
            {
                m_StartTime = sample;
                if (m_EndTime == null)
                    setEndTime(m_StartTime);
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the <see cref="TimeSample"/> representing when this
        /// strobes ends.
        /// </summary>
        /// 
        /// <returns>Returns the <see cref="TimeSample"/> representing
        /// when this strobes ends.</returns>
        /////////////////////////////////////////////////////////
        public TimeSample getEndTime()
        {
            return m_EndTime;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the end time of this strobe based on the
        /// <see cref="TimeSample"/> provided.
        /// </summary>
        /// 
        /// <param name="sample">A <see cref="TimeSample"/> representing
        /// the end time for this strobe.</param>
        /// 
        /// <returns>Returns an instanc of <see cref="TimeSample"/>
        /// representing the end time for this strobe.</returns>
        /////////////////////////////////////////////////////////
        public int setEndTime(TimeSample sample)
        {
            int status = -1;
            if (sample != null)
            {
                m_EndTime = sample;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Gets a <see cref="StrobeState"/> representing the
        /// state that this strobe is set to.
        /// </summary>
        /// 
        /// <returns>Returns a <see cref="StrobeState"/> representing
        /// the state that this strobe is set to.</returns>
        /////////////////////////////////////////////////////////
        public StrobeState getStrobeState()
        {
            return m_State;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the state of this strobe to the provided
        /// <see cref="StrobState"/> value.
        /// </summary>
        /// 
        /// <param name="state">A <see cref="StrobState"/> representing
        /// the state that this strobe should be at.</param>
        /// 
        /// <returns>Returns 0 for success and a -1 if an error
        /// occurs.</returns>
        /////////////////////////////////////////////////////////
        public int setStrobeState(StrobeState state)
        {
            int status = -1;
            m_State = state;
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes whether or not the signal that this strobe
        /// belongs to has failed at this strobe.
        /// </summary>
        /// 
        /// <returns>Returns a bool where true indicates that this
        /// strobe failed or false indicating that it has not.</returns>
        /////////////////////////////////////////////////////////
        public bool isFailed()
        {
            return m_Failed;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets whether or not this strobe should indicate that a
        /// failure has occurred on the signal.
        /// </summary>
        /// 
        /// <param name="failed">A bool where true indicates that this
        /// strobe failed or false indicating that it has not.</param>
        /// 
        /// <returns>Returns a -1, Not yet implemented.</returns>
        /////////////////////////////////////////////////////////
        public int setFailed(bool failed)
        {
            int status = -1;
            m_Failed = failed;
            return status;
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
            return m_StrobePassColor;
        }

        /////////////////////////////////////////////////////////
        /// <summary>The color used to display the strobes that
        /// have passed.</summary>
        ///
        /// <returns>Returns a Pen instance containing the color
        /// to be used to draw a strobe.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen getStrobePassColor()
        {
            return m_StrobePassColor;
        }

        /////////////////////////////////////////////////////////
        /// <summary>The color used to display the strobes that
        /// have failed.</summary>
        ///
        /// <returns>Returns a Pen instance containing the color
        /// to be used to draw a strobe.
        /// </returns>
        /////////////////////////////////////////////////////////
        public Pen getStrobeFailColor()
        {
            return m_StrobeFailColor;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the color that will be used to display
        /// the strobes.</summary>
        ///
        /// <param name="pen">A <see cref="Pen"/> instance containing the color.</param>
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
                m_StrobePassColor = pen;
                m_StrobeFailColor = pen;
                status = 0;
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Sets the color that will be used to display
        /// the strobes that pass.</summary>
        ///
        /// <param name="pen">A <see cref="Pen"/> instance
        /// containing the color.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurres.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setStrobePassColor(Pen pen)
        {
            int status = -1;
            if (pen != null)
            {
                m_StrobePassColor = pen;
                status = 0;
            }
            return status;
        }
        /////////////////////////////////////////////////////////
        /// <summary>Sets the color that will be used to display
        /// the strobes that fail.</summary>
        ///
        /// <param name="pen">A <see cref="Pen"/> instance
        /// containing the color.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 if an error occurres.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setStrobeFailColor(Pen pen)
        {
            int status = -1;
            if (pen != null)
            {
                m_StrobeFailColor = pen;
                status = 0;
            }
            return status;
        }
    }
}
