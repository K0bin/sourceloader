using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotGame.Platform.Windows
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct WndClassEx
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint cbSize;
        [MarshalAs(UnmanagedType.U4)]
        public ClassStyle style;
        public IntPtr lpfnWndProc; // not WndProc
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpszClassName;
        public IntPtr hIconSm;

        //Use this function to make a new one with cbSize already filled in.
        //For example:
        //var WndClss = WNDCLASSEX.Build()
        public static WndClassEx Build()
        {
            var nw = new WndClassEx();
            nw.cbSize = (uint)Marshal.SizeOf(typeof(WndClassEx));
            return nw;
        }
    }

}
