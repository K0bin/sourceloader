using Csgo.Bsp;
using Csgo.Bsp.LumpData;
using System;
using System.Collections.Generic;
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
        
        public MaterialManager MaterialManager
        {
            get => materialManager;
        }

        public BrushModel Brushes
        {
            get; private set;
        }

        private readonly string mapName;
        private readonly Map map;
        private readonly ResourceManager resourceManager;
        private readonly MaterialManager materialManager;

        public MapLoader(string csgoDirectory, string mapName)
        {
            this.mapName = mapName;

            var mapPath = Path.Combine(Path.Combine(Path.Combine(csgoDirectory, "csgo"), "maps"), mapName + ".bsp");
            using (var reader = new BinaryReader(new FileStream(mapPath, FileMode.Open)))
            {
                this.map = Map.Load(reader);
            }

            resourceManager = new ResourceManager(csgoDirectory, map);
            materialManager = new MaterialManager(resourceManager);

            Brushes = new BrushModel(map, MaterialManager);
        }
    }
}
