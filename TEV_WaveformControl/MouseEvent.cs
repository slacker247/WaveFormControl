using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TEV_WaveformControl
{
    /////////////////////////////////////////////////////////
    /// <summary>
    /// Allows for nested controls to propagate the event to
    /// the appropriate controls.
    /// </summary>
    /////////////////////////////////////////////////////////
    class MouseEvent
    {
        protected static uint WM_MOUSEMOVE = 0x200;
        protected static uint WM_LBUTTONDOWN = 0x201;
        protected static uint WM_LBUTTONUP = 0x202;
        protected static uint WM_LBUTTONDBLCLK = 0x203;
        protected static uint WM_RBUTTONDOWN = 0x204;
        protected static uint WM_RBUTTONUP = 0x205;
        protected static uint WM_RBUTTONDBLCLK = 0x206;
        protected static uint WM_MBUTTONDOWN = 0x207;
        protected static uint WM_MBUTTONUP = 0x208;
        protected static uint WM_MBUTTONDBLCLK = 0x209;
        protected static uint WM_MOUSEWHEEL = 0x20A;
        protected static uint WM_XBUTTONDOWN = 0x20B;
        protected static uint WM_XBUTTONUP = 0x20C;
        protected static uint WM_XBUTTONDBLCLK = 0x20D;
        protected static uint WM_MOUSEHWHEEL = 0x20E;

        /////////////////////////////////////////////////////////
        /// <summary>
        /// Sends the appropriate mouse message to the given
        /// handle.
        /// </summary>
        /// 
        /// <param name="handle">The handle of the control.  This
        /// is the intended reciever of this message.</param>
        /// <param name="e">The mouse state.</param>
        /// <param name="upDown">Whether or not the button is up
        /// or down. Down is true and up is false.</param>
        /////////////////////////////////////////////////////////
        public static void sendMouseEvent(IntPtr handle, MouseEventArgs e, bool upDown)
        {
            IntPtr lParam = (IntPtr)((e.Y << 16) | e.X);
            IntPtr wParam = IntPtr.Zero;
            uint msg = 0;
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                    msg = WM_LBUTTONDBLCLK;
                else
                {
                    if (upDown)
                        msg = WM_LBUTTONDOWN;
                    else
                        msg = WM_LBUTTONUP;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (e.Clicks == 2)
                    msg = WM_RBUTTONDBLCLK;
                else
                {
                    if (upDown)
                        msg = WM_RBUTTONDOWN;
                    else
                        msg = WM_RBUTTONUP;
                }
            }
            if (e.Button == MouseButtons.Middle)
            {
                if (e.Clicks == 2)
                    msg = WM_MBUTTONDBLCLK;
                else
                {
                    if (upDown)
                        msg = WM_MBUTTONDOWN;
                    else
                        msg = WM_MBUTTONUP;
                }
            }
            SendMessage(handle, msg, wParam, lParam);
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    }
}
