using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Csgo.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DisplacementVertex
    {
        public Vector3 Vector;
        public float Distance;
        public float Alpha;
    }
}
