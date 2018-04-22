﻿using Csgo.Bsp;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Csgo.MapLoader
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoord;
        public Vector2 LightmapTextureCoord;
    }
}
