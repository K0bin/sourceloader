using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Csgo.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
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
        /*[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public CDispNeighbor[] EdgeNeighbors;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public CDispCornerNeighbors[] CornerNeighbor;
        TODO!*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] AllowedVerts;
    }
}
