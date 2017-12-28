﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CsgoDemoRenderer.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BrushSide
    {
        public ushort PlaneNumber;
        public short TextureInfo;
        public short DisplacementInfo;
        [MarshalAs(UnmanagedType.U1)]
        public bool IsBevelPlane;
    }
}
