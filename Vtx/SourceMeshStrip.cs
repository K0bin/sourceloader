using Source.Common;
using Source.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Source.Mdl.MeshStrip
{
    public class SourceMeshStrip : Resource
    {
        public Header Header
        {
            get; private set;
        }

        public SourceMeshStrip(BinaryReader reader, int length)
        {
            var start = reader.BaseStream.Position;
            Header = reader.ReadStruct<Header>();
        }
    }
}
