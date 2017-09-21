using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Bsp.LumpData
{
    public class Planes: Data
    {
        public Plane[] PlanesArr;
        private Planes() { }

        public static Planes Read(BinaryReader reader, int length)
        {
            var size = length / Plane.SIZE;
            var start = reader.BaseStream.Position;
            
            var planes = new Planes();
            planes.PlanesArr = new Plane[size];
            for (int i = 0; i < size; i++)
            {
                reader.BaseStream.Position = start + i * Plane.SIZE;
                var plane = Plane.Read(reader);
                planes.PlanesArr[i] = plane;
            }
            return planes;
        }
    }

    public class Plane
    {
        public const int SIZE = 20;

        public Vector3 Normal;
        public float Dist;
        public int Type;
        internal Plane() { }

        public static Plane Read(BinaryReader reader)
        {
            var plane = new Plane();
            plane.Normal.X = reader.ReadSingle();
            plane.Normal.Y = reader.ReadSingle();
            plane.Normal.Z = reader.ReadSingle();
            plane.Dist = reader.ReadSingle();
            plane.Type = reader.ReadInt32();
            return plane;
        }
    }
}
