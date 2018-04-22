using Csgo.Bsp;
using Csgo.Bsp.LumpData;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using SteamDatabase.ValvePak;
using System.IO;
using System.Linq;
using Csgo.Vtf;
using Csgo.Util;
using System.Threading;
using System.IO.Compression;

namespace Csgo.MapLoader
{
    public class MapLoader
    {
        public string MapName
        {
            get => mapName;
        }
        
        public MaterialManager Materials
        {
            get => materials;
        }

        public BrushModel Brushes
        {
            get; private set;
        }

        private readonly string mapName;
        private readonly Map map;
        private readonly ResourceManager resources;
        private readonly MaterialManager materials;

        public MapLoader(string csgoDirectory, string mapName)
        {
            this.mapName = mapName;

            var mapPath = Path.Combine(csgoDirectory, "csgo", "maps", mapName + ".bsp");
            using (var reader = new BinaryReader(new FileStream(mapPath, FileMode.Open)))
            {
                this.map = Map.Load(reader);
            }

            resources = new ResourceManager(csgoDirectory, map);
            materials = new MaterialManager(resources);

            Brushes = new BrushModel(map, materials);
        }
    }
}
