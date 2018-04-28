using Source.Mdl.Header;
using Source.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Source.Mdl
{
    public class BodyPart
    {
        public BodyPartHeader Header
        { get; private set; }

        public Model[] Models
        { get; private set; }

        public string Name
        { get; private set; }

        public BodyPart(BodyPartHeader header, BinaryReader reader)
        {
            Header = header;
            var start = reader.BaseStream.Position;

            reader.BaseStream.Position = start + header.NameIndex;
            Name = reader.ReadNullTerminatedAsciiString();
            reader.BaseStream.Position = start;

            reader.BaseStream.Position = start + header.ModelOffset;
            Models = new Model[header.ModelCount];
            for (var i = 0; i < header.ModelCount; i++)
            {
                var modelHeader = reader.ReadStruct<ModelHeader>();
                Models[i] = new Model(modelHeader, reader);
            }
        }

        public override string ToString()
        {
            return $"Body part {Name} with {Header.ModelCount} models";
        }
    }
}
