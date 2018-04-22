using Csgo.Bsp;
using System;
using System.IO;

namespace Csgo.Exporter
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");

            string csgoDir = Path.Combine("C:\\", "Program Files (x86)", "Steam", "steamapps", "common", "Counter-Strike Global Offensive");
            string path = Path.Combine(csgoDir, "csgo", "maps", "aim_redline.bsp");

            var loader = new MapLoader.MapLoader(csgoDir, "aim_redline");
            //loader.Load();
            loader.Export("export");
            Console.ReadKey();
        }
    }
}
