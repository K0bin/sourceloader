using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.Header
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    class ModelHeader
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Name;

        public int Type;
        public float BoundingRadius;
        public int MeshCount;
        public int MeshIndex;

        public int VertexCount;
        public int VertexInfoIndex;
        public int VertexIndex;
        public int NormalCount;
        public int NormalInfoIndex;
        public int NormalIndex;

        public int GroupCount;
        public int GroupIndex;
    }
}
