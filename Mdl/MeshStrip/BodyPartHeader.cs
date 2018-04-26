using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.MeshStrip
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BodyPartHeader
    {
        public int ModelCount;
        public int ModelOffset;
    }
}
