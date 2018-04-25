using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Csgo.Bsp.LumpData.GameLumps
{
    public struct SubLump
    {
        public int Id;
        public ushort Flags;
        public ushort Version;
        public int FileOffset;
        public int FileLength;

        public LumpData Data;

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
            switch (lump.Id)
            {
                case 1936749168: //"sprp" ascii
                    lump.Data = new StaticProps(reader, lump.FileLength, lump.Version);
                    break;
            }
            reader.BaseStream.Position = position;
            return lump;
        }

        public override string ToString()
        {
            var bytes = BitConverter.GetBytes(Id);
            return "SubLump: " + Encoding.ASCII.GetString(bytes);
        }
    }
}
