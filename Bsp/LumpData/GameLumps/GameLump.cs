using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Source.Bsp.LumpData.GameLumps
{
    public class GameLump: LumpData
    {
        public int LumpCount
        { get; private set; }

        public Dictionary<string, SubLump> Lumps
        {
            get; private set;
        } = new Dictionary<string, SubLump>();

        public GameLump(BinaryReader reader)
        {
            LumpCount = reader.ReadInt32();

            for (int i = 0; i < LumpCount; i++)
            {
                var lump = SubLump.Read(reader);
                Lumps[lump.IdName] = lump;
            }
        }
    }
}
