using Source.Mdl.Header;
using Source.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace Source.Mdl
{
    public class Mesh
    {
        public MeshHeader Header
        { get; private set; }

        public Triangle[] Triangles
        { get; private set; }

        public Vector3[] Normals
        { get; private set; }

        public Mesh(MeshHeader header, BinaryReader reader)
        {
            Header = header;
            var start = reader.BaseStream.Position;

            reader.BaseStream.Position = start + header.TriangleIndex;
            Triangles = reader.ReadStructArray<Triangle>(header.TriangleCount);

            reader.BaseStream.Position = start + header.TriangleIndex;
            Triangles = reader.ReadStructArray<Triangle>(header.TriangleCount);
        }

        public override string ToString()
        {
            return $"Mesh";
        }
    }
}
