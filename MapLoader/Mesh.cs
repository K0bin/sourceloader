using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Source.MapLoader
{
    public struct Mesh
    {
        public Vertex[] Vertices;
        public MeshPart[] Parts;

        /*
        public static Mesh CalculateNormals(Mesh mesh)
        {
            List<uint> indices = new List<uint>();
            Vertex[] vertices = new Vertex[mesh.Vertices.Length];
            foreach (var part in mesh.Parts)
            {
                foreach (var index in part.Indices)
                {
                    indices.Add(index);
                }
            }
            for (var i = 0; i < indices.Count - 2; i += 3)
            {
                var vertex0 = vertices[(int)indices[i]];
                var vertex1 = vertices[(int)indices[i + 1]];
                var vertex2 = vertices[(int)indices[i + 2]];

                var vec1 = vertex0.Position - vertex2.Position;
                var vec2 = vertex1.Position - vertex2.Position;
                var cross = Vector3.Cross(vec1, vec2);
                var surfaceNormal = Vector3.Normalize(cross);

                for (int ii = 0; ii < 3; ii++)
                {
                    var vertex = vertices[(int)indices[i + ii]];
                    vertex.Normal = surfaceNormal;
                    vertices[(int)indices[i + ii]] = vertex;
                }
            }            
        }
        */
    }

    public struct MeshPart
    {
        public uint[] Indices;
    }
}
