using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static Bsp.LumpData.Brush;

namespace Bsp.LumpData
{
    public class Leafs: Data
    {
        public Leaf[] Elements;
        private Leafs() { }
        
        public static Leafs Read(BinaryReader reader, int length)
        {
            var size = length / Leaf.SIZE;
            var start = reader.BaseStream.Position;

            var leafs = new Leafs();
            leafs.Elements = new Leaf[size];
            for (var i = 0; i < size; i++)
            {
                leafs.Elements[i] = Leaf.Read(reader);
            }
            return leafs;
        }
    }
    public class Leaf
    {
        //public const int SIZE = 56;
        public const int SIZE = 32;
        //public const int SIZE = 30;

        public BrushContents Contents;
        public short Cluster;
        //public short Area;
        //public short Flags;
        public short AreaFlags;
        public short[] Mins = new short[3];
        public short[] Maxs = new short[3];
        public ushort FirstLeafFace;
        public ushort LeafFacesCount;
        public ushort FirstLeafBrush;
        public ushort LeafBrushesCount;
        public short LeafWaterDataId;
        private Leaf() { }

        public static Leaf Read(BinaryReader reader)
        {
            var leaf = new Leaf();
            leaf.Contents = (BrushContents)reader.ReadInt32();
            leaf.Cluster = reader.ReadInt16();
            //leaf.Area = reader.ReadInt16();
            //leaf.Flags = reader.ReadInt16();
            leaf.AreaFlags = reader.ReadInt16();
            leaf.Mins[0] = reader.ReadInt16();
            leaf.Mins[1] = reader.ReadInt16();
            leaf.Mins[2] = reader.ReadInt16();
            leaf.Maxs[0] = reader.ReadInt16();
            leaf.Maxs[1] = reader.ReadInt16();
            leaf.Maxs[2] = reader.ReadInt16();
            leaf.FirstLeafFace = reader.ReadUInt16();
            leaf.LeafFacesCount = reader.ReadUInt16();
            leaf.FirstLeafBrush = reader.ReadUInt16();
            leaf.LeafBrushesCount = reader.ReadUInt16();
            leaf.LeafWaterDataId = reader.ReadInt16();

            //Padding
            reader.ReadBytes(2);

            return leaf;
        }
    }
}
