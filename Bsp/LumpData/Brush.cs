using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class Brushes: Data
    {
        public Brush[] Elements;
        private Brushes() { }

        public static Brushes Read(BinaryReader reader, int length)
        {
            var size = length / Brush.SIZE;
            
            var brushes = new Brushes();
            brushes.Elements = new Brush[size];
            for (var i = 0; i < size; i++)
            {
                brushes.Elements[i] = Brush.Read(reader);
            }
            return brushes;
        }
    }

    public class Brush
    {
        public const int SIZE = 12;

        public int firstSide;
        public int SidesCount;
        public BrushContents Contents;
        private Brush() { }

        public static Brush Read(BinaryReader reader)
        {
            var brush = new Brush();
            brush.firstSide = reader.ReadInt32();
            brush.SidesCount = reader.ReadInt32();
            brush.Contents = (BrushContents)reader.ReadInt32();
            return brush;
        }

        [Flags]
        public enum BrushContents
        {
            Empty = 0,
            Solid = 0x1,
            Window = 0x2,
            Aux = 0x4,
            Grate = 0x8,
            Slime = 0x10,
            Water = 0x20,
            Mist = 0x40,
            Opaque = 0x80,
            TestFogVolume = 0x100,
            Unused = 0x200,
            Unused6 = 0x400,
            Team1 = 0x800,
            Team2 = 0x1000,
            IgnoreNodrawOpaque = 0x2000,
            Movable = 0x4000,
            AreaPortal = 0x8000,
            Playerclip = 0x10000,
            Monsterclip = 0x20000,
            Current0 = 0x40000,
            Current90 = 0x80000,
            Current180 = 0x100000,
            Current270 = 0x200000,
            CurrentUp = 0x400000,
            CurrentDown = 0x800000,
            Origin = 0x1000000,
            Monster = 0x2000000,
            Debris = 0x4000000,
            Detail = 0x8000000,
            Translucent = 0x10000000,
            Ladder = 0x20000000,
            Hitbox = 0x40000000
        }
    }
}
