using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class Nodes: Data
    {
        public Node[] Elements;
        private Nodes() { }

        public static Nodes Read(BinaryReader reader, int length)
        {
            var size = length / Node.SIZE;
            var start = reader.BaseStream.Position;

            var nodes = new Nodes();
            nodes.Elements = new Node[size];
            for (var i = 0; i < size; i++)
            {
                nodes.Elements[i] = Node.Read(reader);
            }
            return nodes;
        }
    }

    public class Node
    {
        public const int SIZE = 32;

        public int PlaneNumber;
        public int[] Children = new int[2];
        public short[] Mins = new short[3];
        public short[] Maxs = new short[3];
        public ushort FirstFace;
        public ushort FacesCount;
        public short Area;
        public short Padding;
        private Node() { }

        public static Node Read(BinaryReader reader)
        {
            var node = new Node();
            node.PlaneNumber = reader.ReadInt32();
            node.Children[0] = reader.ReadInt32();
            node.Children[1] = reader.ReadInt32();
            node.Mins[0] = reader.ReadInt16();
            node.Mins[1] = reader.ReadInt16();
            node.Mins[2] = reader.ReadInt16();
            node.Maxs[0] = reader.ReadInt16();
            node.Maxs[1] = reader.ReadInt16();
            node.Maxs[2] = reader.ReadInt16();
            node.FirstFace = reader.ReadUInt16();
            node.FacesCount = reader.ReadUInt16();
            node.Area = reader.ReadInt16();
            node.Padding = reader.ReadInt16();
            return node;
        }
    }
}
