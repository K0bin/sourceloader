using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Source.Util;

namespace Source.Bsp.LumpData.GameLumps
{
    public struct SubLump
    {
        public int Id;
        public ushort Flags;
        public ushort Version;
        public int FileOffset;
        public int FileLength;

        public LumpData Data;

        public string IdName
        {
            get
            {
                var bytes = BitConverter.GetBytes(Id);
                return Encoding.ASCII.GetString(bytes).Reverse();
            }
        }

        public static SubLump Read(BinaryReader reader)
        {
            var lump = new SubLump();
            lump.Id = reader.ReadInt32();
            lump.Flags = reader.ReadUInt16();
            lump.Version = reader.ReadUInt16();
            lump.FileOffset = reader.ReadInt32();
            lump.FileLength = reader.ReadInt32();

            var position = reader.BaseStream.Position;
            reader.BaseStream.Position = lump.FileOffset;
            switch (lump.IdName)
            {
                case "sprp":
                    lump.Data = new StaticProps(reader, lump.FileLength, lump.Version);
                    break;
            }
            reader.BaseStream.Position = position;
            return lump;
        }

        public override string ToString()
        {
            return "SubLump: " + IdName;
        }
    }
}
