using Source.Common;
using Source.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Source.Mdl
{
    public class SourceModel : Resource
    {
        public Header Header
        {
            get; private set;
        }
        public Header2 Header2
        {
            get; private set;
        }

        public List<string> Materials
        {
            get; private set;
        } = new List<string>();

        public List<string> MaterialPaths
        {
            get; private set;
        } = new List<string>();

        public Texture[] Textures
        {
            get; private set;
        }

        public SourceModel(BinaryReader reader, int length)
        {
            var start = reader.BaseStream.Position;
            Header = reader.ReadStruct<Header>();
            var postHeader = reader.BaseStream.Position;

            reader.BaseStream.Position = Header.StudioHDR2Index;
            Header2 = reader.ReadStruct<Header2>();

            reader.BaseStream.Position = start + Header.TextureDirOffset;
            for (var i = 0; i < Header.TextureDirCount; i++)
            {
                var offset = reader.ReadInt32();
                var pos = reader.BaseStream.Position;
                reader.BaseStream.Position = start + offset;
                var path = reader.ReadNullTerminatedAsciiString();
                MaterialPaths.Add(path);
                reader.BaseStream.Position = pos;
            }

            reader.BaseStream.Position = start + Header.TextureOffset;
            Textures = new Texture[Header.TextureCount];
            for (var i = 0; i < Textures.Length; i++)
            {
                var pos = reader.BaseStream.Position;
                Textures[i] = reader.ReadStruct<Texture>();
                reader.BaseStream.Position = pos + Textures[i].NameOffset;
                Materials.Add(reader.ReadNullTerminatedAsciiString());
                reader.BaseStream.Position = pos;
            }
        }
    }
}
