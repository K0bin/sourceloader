using Source.Bsp;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.MapLoader
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoord;
        public Vector2 LightmapTextureCoord;

        public static Vertex Read(Map map)
        {
            return new Vertex();
        }
    }
}
