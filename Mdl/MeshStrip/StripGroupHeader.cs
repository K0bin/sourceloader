using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.MeshStrip
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StripGroupHeader
    {
        public int VertexCount;
        public int VertexOffset;

        public int IndexCount;
        public int IndexOffset;

        public int StripCount;
        public int StripOffset;

        public byte Flags;
    }
}
