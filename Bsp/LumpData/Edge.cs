using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Edge
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public ushort[] VertexIndex;
    }
}
