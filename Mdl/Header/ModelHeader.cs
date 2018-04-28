using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.Header
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ModelHeader
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Name;

        public int Type;
        public float BoundingRadius;
        public int MeshCount;
        public int MeshIndex;

        public int VertexCount;
        public int VertexIndex;
        public int TangentIndex;

        public int AttachmentCount;
        public int AttachmentIndex;

        public int EyeBallCount;
        public int EyeBallIndex;

        public ModelVertexData VertexData;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] Unused;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    public struct ModelVertexData
    {

    }
}
