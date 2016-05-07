using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TEV_WaveformControl
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// The time measurement units that can be displayed by
    /// the <see cref="TimeSample"/> class.
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
    public enum TimeScale
    {
        /////////////////////////////////////////////////////////
        /// <summary>Used to denote time in seconds.</summary>
        /////////////////////////////////////////////////////////
        SECONDS = 12,
        /////////////////////////////////////////////////////////
        /// <summary>Used to denote time in  milliseconds
        /// 10^(-3) seconds </summary>
        /////////////////////////////////////////////////////////
        MILLISECONDS = 9,
        /////////////////////////////////////////////////////////
        /// <summary>Used to denote time in microseconds.
        /// 10^(-6) seconds</summary>
        /////////////////////////////////////////////////////////
        MICROSECONDS = 6,
        /////////////////////////////////////////////////////////
        /// <summary>Used to denote time in nanoseconds.
        /// 10^(-9) seconds</summary>
        /////////////////////////////////////////////////////////
        NANOSECONDS = 3,
        /////////////////////////////////////////////////////////
        /// <summary>Used to denote time in picoseconds.
        /// 10^(-12) seconds</summary>
        /////////////////////////////////////////////////////////
        PICOSECONDS = 0
    };

    /////////////////////////////////////////////////////////
    /// <summary>
    /// This class was created to support a smaller
    /// time slice down to PicoSeconds unlike the built in
    /// TimeSpan which only supports down to microseconds.
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
    public class TimeSample
    {
        /////////////////////////////////////////////////////////
        /// <summary>The number of picoseconds this class represents.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected float m_PicoSeconds;

        /////////////////////////////////////////////////////////
        /// <summary>Constructor: Initializes the class to the given
        /// Time value and the specified time measurement.
        /// </summary>
        ///
        /// <param name="time">A float representing the amount of
        /// time to be stored.</param>
        /// <param name="scale">A <see cref="TimeScale"/> referencing
        /// the units that the time param is in.
        /// </param>
        ///
        /// <returns>Returns an instance of the class.
        /// </returns>
        /////////////////////////////////////////////////////////
        public TimeSample(float time, TimeScale scale)
        {
            setTime(time, scale);
        }

        #region Operators
        public static TimeSample operator +(TimeSample lhs, TimeSample rhs)
        {
            TimeSample ts = null;
            if (lhs != null && rhs != null)
                ts = new TimeSample(lhs.getTime() + rhs.getTime(),
                                                TimeScale.PICOSECONDS);
            return ts;
        }

        public static TimeSample operator -(TimeSample lhs, TimeSample rhs)
        {
            TimeSample ts = null;
            if (lhs != null && rhs != null)
                ts = new TimeSample(lhs.getTime() - rhs.getTime(),
                                                TimeScale.PICOSECONDS);
            return ts;
        }

        public static TimeSample operator *(TimeSample lhs, TimeSample rhs)
        {
            TimeSample ts = null;
            if (lhs != null && rhs != null)
                ts = new TimeSample(lhs.getTime() * rhs.getTime(),
                                                TimeScale.PICOSECONDS);
            return ts;
        }

        public static TimeSample operator *(double lhs, TimeSample rhs)
        {
            TimeSample ts = null;
            if (rhs != null)
                ts = new TimeSample((int)(lhs * rhs.getTime()),
                                                TimeScale.PICOSECONDS);
            return ts;
        }

        public static TimeSample operator *(TimeSample lhs, double rhs)
        {
            return rhs * lhs;
        }

        public static TimeSample operator /(TimeSample lhs, TimeSample rhs)
        {
            TimeSample ts = null;
            if (lhs != null && rhs != null)
                ts = new TimeSample(lhs.getTime() / rhs.getTime(),
                                                TimeScale.PICOSECONDS);
            return ts;
        }

        public static TimeSample operator /(TimeSample lhs, int rhs)
        {
            TimeSample ts = null;
            if (lhs != null && rhs != null)
                ts = new TimeSample(lhs.getTime() / rhs,
                                    TimeScale.PICOSECONDS);
            return ts;
        }

        //public static bool operator ==(TimeSample lhs, TimeSample rhs)
        //{
        //    bool status = false;
        //    status = lhs != rhs;
        //    if (lhs != null && rhs != null)
        //        status = lhs.getTime(TimeScale.PICOSECONDS) == rhs.getTime(TimeScale.PICOSECONDS);
        //    return status;
        //}
        
        public static bool operator >(TimeSample lhs, TimeSample rhs)
        {
            bool status = false;
            if (lhs != null && rhs != null)
                status = lhs.getTime(TimeScale.PICOSECONDS) > rhs.getTime(TimeScale.PICOSECONDS);
            return status;
        }

        public static bool operator <(TimeSample lhs, TimeSample rhs)
        {
            bool status = false;
            if (lhs != null && rhs != null)
                status = lhs.getTime(TimeScale.PICOSECONDS) < rhs.getTime(TimeScale.PICOSECONDS);
            return status;
        }

        public static bool operator >=(TimeSample lhs, TimeSample rhs)
        {

            bool status = false;
            if (lhs != null && rhs != null)
                status = lhs.getTime(TimeScale.PICOSECONDS) >= rhs.getTime(TimeScale.PICOSECONDS);
            return status;
        }

        public static bool operator <=(TimeSample lhs, TimeSample rhs)
        {
            bool status = false;
            if (lhs != null && rhs != null)
                status = lhs.getTime(TimeScale.PICOSECONDS) <= rhs.getTime(TimeScale.PICOSECONDS);
            return status;
        }

        //public static bool operator !=(TimeSample lhs, TimeSample rhs)
        //{
        //    bool status = false;
        //    status = lhs != rhs;
        //    if (lhs != null && rhs != null)
        //        status = lhs.getTime(TimeScale.PICOSECONDS) != rhs.getTime(TimeScale.PICOSECONDS);
        //    return status;
        //}
        #endregion

        /////////////////////////////////////////////////////////
        /// <summary>Parses a string representing the time measurement.
        /// This string should follow the format #.## with a trailing
        /// unit:'s', 'ms', 'us', 'ns', 'ps'</summary>
        ///
        /// <param name="time">A String representing the time
        /// measurement.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 indicating an error.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int parseTime(String time)
        {
            int status = -1;
            String pattern = "[0-9]*[.]?[0-9]+[pnum]?s";
            if (time.Length > 0 &&
                Regex.IsMatch(time, pattern))
            {
                if (time.Contains("ps"))
                {
                    status = setTime((float)Convert.ToDouble(time.Substring(0, time.Length - 2)), TimeScale.PICOSECONDS);
                }
                else if (time.Contains("ns"))
                {
                    status = setTime((float)Convert.ToDouble(time.Substring(0, time.Length - 2)), TimeScale.NANOSECONDS);
                }
                else if (time.Contains("us"))
                {
                    status = setTime((float)Convert.ToDouble(time.Substring(0, time.Length - 2)), TimeScale.MICROSECONDS);
                }
                else if (time.Contains("ms"))
                {
                    status = setTime((float)Convert.ToDouble(time.Substring(0, time.Length - 2)), TimeScale.MILLISECONDS);
                }
                else if (time.Contains("s"))
                {
                    status = setTime((float)Convert.ToDouble(time.Substring(0, time.Length - 2)), TimeScale.SECONDS);
                }
            }
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Takes the given time and the unit of measure
        /// and sets this class to those values.</summary>
        ///
        /// <param name="time">A float representing the time measurement.
        /// </param>
        /// <param name="scale">A <see cref="TimeScale"/> value
        /// representing the unit of measure.</param>
        ///
        /// <returns>Returns 0 for a success. Otherwise the
        /// value will be -1 indicating an error.
        /// </returns>
        /////////////////////////////////////////////////////////
        public int setTime(float time, TimeScale scale)
        {
            int status = -1;
            m_PicoSeconds = (float)(time * Math.Pow(10, (int)scale));
            return status;
        }

        /////////////////////////////////////////////////////////
        /// <summary>Returns the time measurement in Picoseconds.
        /// </summary>
        ///
        /// <returns>Returns the time measurement in the requested
        /// unit of measure as a float.
        /// </returns>
        /////////////////////////////////////////////////////////
        public float getTime()
        {
            return getTime(TimeScale.PICOSECONDS);
        }

        /////////////////////////////////////////////////////////
        /// <summary>Returns the time measurement in the requested
        /// unit of measure.</summary>
        ///
        /// <param name="scale">A <see cref="TimeScale"/> that
        /// represents the unit of measure to return the time
        /// value in.</param>
        ///
        /// <returns>Returns the time measurement in the requested
        /// unit of measure as a float.
        /// </returns>
        /////////////////////////////////////////////////////////
        public float getTime(TimeScale scale)
        {
            return (float)(m_PicoSeconds / Math.Pow(10, (int)scale));
        }

        public String ToString(TimeScale timeScale)
        {
            String time = "";
            String format = "{0}{1}";
            switch (timeScale)
            {
                case TimeScale.SECONDS:
                    time = String.Format(format, getTime(TimeScale.SECONDS), "s");
                    break;
                case TimeScale.MILLISECONDS:
                    time = String.Format(format, getTime(TimeScale.MILLISECONDS), "ms");
                    break;
                case TimeScale.MICROSECONDS:
                    time = String.Format(format, getTime(TimeScale.MICROSECONDS), "us");
                    break;
                case TimeScale.NANOSECONDS:
                    time = String.Format(format, getTime(TimeScale.NANOSECONDS), "ns");
                    break;
                case TimeScale.PICOSECONDS:
                    time = String.Format(format, getTime(TimeScale.PICOSECONDS), "ps");
                    break;
            }
            return time;
        }
    }
}
