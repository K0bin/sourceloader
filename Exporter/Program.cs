using Source.Bsp;
using System;
using System.IO;

namespace Source.Exporter
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");

            //string csgoDir = Path.Combine("C:\\", "Program Files (x86)", "Steam", "steamapps", "common", "Counter-Strike Global Offensive");
            //string csgoDir = Path.Combine("D:\\", "Games", "Steam", "steamapps", "common", "Counter-Strike Global Offensive");
            string csgoDir = Path.Combine("E:\\", "Games", "Steam Games", "steamapps", "common", "Counter-Strike Global Offensive");

            var loader = new MapLoader.MapLoader(csgoDir, "de_overpass");
            //loader.Load();
            loader.Export("export");
            Console.ReadKey();
        }
    }
}
