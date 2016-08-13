using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace ModernUI.Common.Utility
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Point
    {
        public int X;
        public int Y;
    }

    public class MouseEx
    {

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Win32Point pos);

        /// <summary>
        /// Returns the position of the mouse cursor in screen coordinates
        /// </summary>
        /// <returns></returns>
        public static Point Win32GetCursorPos()
        {
            Win32Point position = new Win32Point();
            GetCursorPos(ref position);
            return new Point((double)position.X, (double)position.Y);
        }
    }
}
