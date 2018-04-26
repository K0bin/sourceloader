using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Triangle
    {
        public short VertexIndex;
        public short NormalIndex;
        public short S;
        public short T;
    }
}
