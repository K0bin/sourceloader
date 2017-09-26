using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CsgoDemoRenderer.Bsp.LumpData
{
    public struct Edge
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public ushort[] VertexIndex;
    }
}
