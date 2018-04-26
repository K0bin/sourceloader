using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.MeshStrip
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StripHeader
    {
        public int IndexCount;
        public int IndexOffset;

        public int VertexCount;
        public int VertexOffset;

        public short BonesCount;

        public byte Flags;

        public int BoneStateChangeCount;
        public int BoneStateChangeOffset;
    }
}
