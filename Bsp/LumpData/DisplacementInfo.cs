using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Size = 176, Pack = 1)]
    public struct DisplacementInfo
    {
        public Vector3 StartPosition;
        public int DisplacementVertexStart;
        public int DisplacementTriangleStart;
        public int Power;
        public int MinTesseleation;
        public float SmoothAngle;
        public int Contents;
        public ushort MapFace;
        public int LightmapAlphaStart;
        public int LightmapSamplePositionStart;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public DisplacementNeighbor[] EdgeNeighbors;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public DisplacementCornerNeighbors[] CornerNeighbor;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] AllowedVerts;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DisplacementSubNeighbor
    {
        public ushort NeighborIndex;
        public byte NeighborOrientation;
        public byte Span;
        public byte NeighborSpan;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DisplacementNeighbor
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public DisplacementSubNeighbor[] SubNeighbors;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DisplacementCornerNeighbors
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] Neighbors;
        public byte NeighborsCount;
    }
}
