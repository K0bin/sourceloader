using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotGame.Platform.Windows
{
    using LONG = System.Int32;

    /// <summary>
    /// Defines the coordinates of the upper-left and lower-right corners of a rectangle.
    /// </summary>
    /// <remarks>
    /// By convention, the right and bottom edges of the rectangle are normally considered exclusive. In other words, the pixel whose coordinates are (right, bottom) lies immediately outside of the the rectangle. For example, when RECT is passed to the FillRect function, the rectangle is filled up to, but not including, the right column and bottom row of pixels. This structure is identical to the RECTL structure.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Rectangle
    {
        /// <summary>
        /// Specifies the x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        internal LONG left;
        /// <summary>
        /// Specifies the y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        internal LONG top;
        /// <summary>
        /// Specifies the x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        internal LONG right;
        /// <summary>
        /// Specifies the y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        internal LONG bottom;

        internal int Width { get { return right - left; } }
        internal int Height { get { return bottom - top; } }

        public override string ToString()
        {
            return String.Format("({0},{1})-({2},{3})", left, top, right, bottom);
        }

        internal static Win32Rectangle From(Rectangle value)
        {
            Win32Rectangle rect = new Win32Rectangle();
            rect.left = (int)value.Left;
            rect.right = (int)value.Right;
            rect.top = (int)value.Top;
            rect.bottom = (int)value.Bottom;
            return rect;
        }

        internal static Win32Rectangle From(Size value)
        {
            Win32Rectangle rect = new Win32Rectangle();
            rect.left = 0;
            rect.right = value.Width;
            rect.top = 0;
            rect.bottom = value.Height;
            return rect;
        }
    }
}
