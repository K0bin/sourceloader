using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 56)]
    public struct Face
    {
        public ushort PlaneNumber;
        public byte Side;
        [MarshalAs(UnmanagedType.U1)]
        public bool IsOnNode;
        public int FirstEdge;
        public short EdgesCount;
        public short TextureInfo;
        public short DisplacementInfo;
        public short SurfaceFogVolumeId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Styles;
        public int LightOffset;
        public float Area;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] LightmapTextureMinsInLuxels;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] LightmapTextureSizeInLuxels;
        public int originalFace;
        public ushort PrimitivesCount;
        public ushort FirstPrimitiveId;
        public uint SmoothingGroup;
    }
}
