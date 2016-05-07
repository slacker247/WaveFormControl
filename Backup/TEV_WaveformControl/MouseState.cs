using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TEV_WaveformControl
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// Tracks the state of the mouse button.  Either up or
    /// down.
    /// </summary>
    /////////////////////////////////////////////////////////
    public enum MouseButtonState { UP, DOWN };

    /////////////////////////////////////////////////////////
    /// <summary>
    /// Tracks the state of the mouse.  The standard .net
    /// mouse state doesn't track whether or not the mouse
    /// button is up or down.
    /// </summary>
    /////////////////////////////////////////////////////////
    public class MouseState
    {
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes which mouse button is active.
        /// </summary>
        /////////////////////////////////////////////////////////
        public MouseButtons m_Button;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Denotes the location of the mouse cursor.
        /// </summary>
        /////////////////////////////////////////////////////////
        public Point m_Location;
        /////////////////////////////////////////////////////////
        /// <summary>
        /// The state of the mouse button.  Denotes if the button
        /// is up or down.
        /// </summary>
        /////////////////////////////////////////////////////////
        public MouseButtonState m_State;

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor.
        /// </summary>
        /////////////////////////////////////////////////////////
        public MouseState()
        {
            // TODO : Should make this a child class of the MouseEventArgs class.
        }

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor that initializes the values to the provided
        /// ones.
        /// </summary>
        /// 
        /// <param name="mb">The mouse button.</param>
        /// <param name="loc">The location of the mouse</param>
        /// <param name="state">The state of the button.</param>
        /////////////////////////////////////////////////////////
        public MouseState(MouseButtons mb, Point loc, MouseButtonState state)
        {
            m_Button = mb;
            m_Location = loc;
            m_State = state;
        }
    }
}
