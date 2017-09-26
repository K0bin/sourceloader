using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CsgoDemoRenderer.Bsp.LumpData
{
    public struct TextureInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public float[,] TextureVecs;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public float[,] LightmapVecs;
        public int Flags;
        public int TextureData;
    }
}
