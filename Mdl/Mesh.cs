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

        public Mesh(MeshHeader header, BinaryReader reader)
        {
            Header = header;
            var start = reader.BaseStream.Position;
        }

        public override string ToString()
        {
            return $"Mesh";
        }
    }
}
