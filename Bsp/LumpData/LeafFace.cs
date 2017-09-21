using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bsp.LumpData
{
    public class LeafFaces: Data
    {
        public ushort[] Elements;
        private LeafFaces() { }

        public static LeafFaces Read(BinaryReader reader, int length)
        {
            var size = length / 2;
            
            var faces = new LeafFaces();
            faces.Elements = new ushort[size];
            for (var i = 0; i < size; i++)
            {
                faces.Elements[i] = reader.ReadUInt16();
            }
            return faces;
        }
    }

    public class LeafBrushes : Data
    {
        public ushort[] Elements;
        private LeafBrushes() { }

        public static LeafBrushes Read(BinaryReader reader, int length)
        {
            var size = length / 2;
            
            var brushes = new LeafBrushes();
            brushes.Elements = new ushort[size];
            for (var i = 0; i < size; i++)
            {
                brushes.Elements[i] = reader.ReadUInt16();
            }
            return brushes;
        }
    }
}
