using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Source.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TextureData
    {
        public Vector3 Reflectivity;
        public int NameStringTableId;
        public int Width;
        public int Height;
        public int ViewWidth;
        public int viewHeight;
    }
}
