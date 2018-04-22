using System;
using System.Collections.Generic;
using System.Text;

namespace Csgo.MapLoader
{
    public struct Mesh
    {
        public Vertex[] Vertices;
        public MeshPart[] Parts;
    }

    public struct MeshPart
    {
        public uint[] Indices;
    }
}
