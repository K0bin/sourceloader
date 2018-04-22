using Csgo.Bsp;
using System;
using System.IO;

namespace Csgo.Exporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string csgoDir = Path.Combine("C:\\", "Program Files (x86)", "Steam", "steamapps", "common", "Counter-Strike Global Offensive");
            string path = Path.Combine(csgoDir, "csgo", "maps", "aim_redline.bsp");

            Map map;
            try
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open)))
                {
                    map = Map.Load(reader);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            var loader = new MapLoader.MapLoader(csgoDir, "aim_redline");
            //loader.Load();
            loader.Export("export");
            Console.ReadKey();
        }
    }
}
