using System;
using System.Collections.Generic;
using System.Text;

namespace TEV_WaveformControl
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// This class is used for simple benchmarking.
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
    class PerfMon
    {
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The for which the benchmarking should start.
        /// </summary>
        /////////////////////////////////////////////////////////
        protected static DateTime m_StartTime;

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sets the start time for the benchmarking.
        /// </summary>
        /////////////////////////////////////////////////////////
        public static void start()
        {
            m_StartTime = DateTime.Now;
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Stops the clock and reports to the console how long 
        /// it has been since start() was called.
        /// </summary>
        /// 
        /// <param name="label">A string to represent the time
        /// that will be written to the console.</param>
        /////////////////////////////////////////////////////////
        public static void stop(String label)
        {
            if (m_StartTime == null)
                return;
            DateTime endTime = DateTime.Now;
            TimeSpan ts2 = endTime - m_StartTime;
            TimeSample duration = new TimeSample((float)(ts2.TotalMilliseconds), TimeScale.MILLISECONDS);
            System.Diagnostics.Debug.WriteLine(label + ": " + getTimeAsString(duration, TimeScale.MICROSECONDS, "{0}{1}"));
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
        protected static String getTimeAsString(TimeSample ticks, TimeScale timeScale, String format)
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
    }
}
