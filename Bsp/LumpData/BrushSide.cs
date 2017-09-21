using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class BrushSides: Data
    {
        public BrushSide[] Elements;
        private BrushSides() { }

        public static BrushSides Read(BinaryReader reader, int length)
        {
            var size = length / BrushSide.SIZE;
            
            var brushSides = new BrushSides();
            brushSides.Elements = new BrushSide[size];
            for (int i = 0; i < size; i++)
            {
                brushSides.Elements[i] = BrushSide.Read(reader);
            }
            return brushSides;
        }
    }
    public class BrushSide
    {
        public const int SIZE = 8;

        public ushort PlaneNumber;
        public short TextureInfo;
        public short DisplacementInfo;
        public bool IsBevelPlane;

        private BrushSide() { }
        public static BrushSide Read(BinaryReader reader)
        {
            var brushSide = new BrushSide();
            brushSide.PlaneNumber = reader.ReadUInt16();
            brushSide.TextureInfo = reader.ReadInt16();
            brushSide.DisplacementInfo = reader.ReadInt16();
            brushSide.IsBevelPlane = reader.ReadInt16() == 1;
            return brushSide;
        }
    }
}
