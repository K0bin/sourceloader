using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class Edges: Data
    {
        public Edge[] EdgeArr;
        private Edges() { }

        public static Edges Read(BinaryReader reader, int length)
        {
            var size = length / Edge.SIZE;
            var start = reader.BaseStream.Position;

            var edges = new Edges();
            edges.EdgeArr = new Edge[size];
            for (var i = 0; i < size; i++)
            {
                edges.EdgeArr[i] = new Edge();
                edges.EdgeArr[i].VertexIndex[0] = reader.ReadUInt16();
                edges.EdgeArr[i].VertexIndex[1] = reader.ReadUInt16();
            }
            return edges;
        }
    }
    public class Edge
    {
        public const int SIZE = 4;

        internal Edge() { }
        public ushort[] VertexIndex = new ushort[2];
    }
}
