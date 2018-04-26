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

            reader.BaseStream.Position = Header.TextureDirOffset;
            for (var i = 0; i < Header.TextureDirCount; i++)
            {
                var offset = reader.ReadInt32();
                var pos = reader.BaseStream.Position;
                reader.BaseStream.Position = offset;
                Materials.Add(reader.ReadNullTerminatedAsciiString());
                reader.BaseStream.Position = pos;
            }

            Textures = new Texture[Header.TextureCount];
            reader.BaseStream.Position = Header.TextureOffset;
            for (var i = 0; i < Header.TextureCount; i++)
            {
                var tex = reader.ReadStruct<Texture>();
                Textures[i] = tex;
                var pos = reader.BaseStream.Position;
                reader.BaseStream.Position = tex.NameOffset;
                Materials.Add(reader.ReadNullTerminatedAsciiString());
                reader.BaseStream.Position = pos;
            }
        }
    }
}
