using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.Header
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MeshHeader
    {
        public int TriangleCount;
        public int TriangleIndex;
        public int SkinReference;
        public int NormalCount;
        public int NormalIndex;
    }
}
