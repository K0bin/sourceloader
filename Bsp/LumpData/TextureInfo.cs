using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TextureInfo
    {
        public Vector4 TextureVecsS;
        public Vector4 TextureVecsT;
        public Vector4 LightmapVecsS;
        public Vector4 LightmapVecsT;
        public int Flags;
        public int TextureData;
    }
}
