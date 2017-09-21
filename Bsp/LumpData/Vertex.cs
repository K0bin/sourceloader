using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace Bsp.LumpData
{
    class Vertices: Data
    {
        public Vector3[] Elements;

        public static Vertices Read(BinaryReader reader, int length)
        {
            var size = length / 12;
            
            var vertices = new Vertices();
            vertices.Elements = new Vector3[size];
            for (int i = 0; i < size; i++)
            {
                vertices.Elements[i] = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            }
            return vertices;
        }
    }
}
