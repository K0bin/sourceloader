using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.MeshStrip
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vertex
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] BoneWeightIndex;
        public byte BoneCount;

        public short OriginMeshVertexId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] BoneId;
    }
}
