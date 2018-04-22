using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var csgoDir = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Path.Combine("C:\\", "Program Files (x86)"), "Steam"), "steamapps"), "common"), "Counter-Strike Global Offensive");
            var loader = new Csgo.MapLoader.MapLoader(csgoDir, "aim_redline");
        }
    }
}
