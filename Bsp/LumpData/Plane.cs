using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Csgo.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 20)]
    public struct Plane
    {
        public Vector3 Normal;
        public float Dist;
        public int Type;
    }
}
