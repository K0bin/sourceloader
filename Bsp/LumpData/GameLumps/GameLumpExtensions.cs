using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Bsp.LumpData.GameLumps
{
    public static class GameLumpExtensions
    {
        public static StaticProps GetStaticProps(this GameLump lump)
        {
            return lump.Lumps[StaticProps.IdName].Data as StaticProps;
        }
    }
}
