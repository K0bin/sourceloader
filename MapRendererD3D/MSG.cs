using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotGame.Platform.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSG
    {
        public IntPtr hwnd;
        public UInt32 message;
        public IntPtr wParam;
        public IntPtr lParam;
        public UInt32 time;
        public POINT pt;
    }
}
