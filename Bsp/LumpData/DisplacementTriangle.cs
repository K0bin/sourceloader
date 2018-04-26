using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DisplacementTriangle
    {
        public ushort Tags;
    }
}
