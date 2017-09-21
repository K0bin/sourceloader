using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class SurfaceEdges: Data
    {
        public int[] Elements;
        private SurfaceEdges() { }

        public static SurfaceEdges Read(BinaryReader reader, int length)
        {
            var size = length / 4;
            
            var surfedges = new SurfaceEdges();
            surfedges.Elements = new int[size];
            for (var i = 0; i < size; i++)
            {
                surfedges.Elements[i] = reader.ReadInt32();
            }
            return surfedges;
        }
    }
}
