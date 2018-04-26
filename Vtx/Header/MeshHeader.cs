using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.MeshStrip
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MeshHeader
    {
        public int StripGroupCount;
        public int StripGroupOffset;

        public MeshHeaderFlags Flags;
    }

    [Flags]
    public enum MeshHeaderFlags: byte
    {
        IsFlexed = 1,
        IsHardwareSkinned = 2,
        IsDeltaFixed = 4,
        SupresssHardwareMorph = 8
    }
}
