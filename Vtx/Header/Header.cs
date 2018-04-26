using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.MeshStrip
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header
    {
        public int Version;
        public int VertexCacheSize;
        public ushort MaxBonesPerStrip;
        public ushort MaxBonesPerTriangle;
        public int MaxBonesPerVertex;
        public int Checksum;
        public int LevelOfDetailCount;
        public int MaterialReplacementListOffset;
        public int BodyPartCount;
        public int BodyPartOffset;
    }
}
