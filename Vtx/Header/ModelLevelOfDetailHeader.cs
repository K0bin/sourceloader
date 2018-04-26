using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.MeshStrip
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ModelLevelOfDetailHeader
    {
        public int MeshCount;
        public int MeshOffset;

        public float SwitchPoint;
    }
}
