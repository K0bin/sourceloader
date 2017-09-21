using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class TextureDataStringTable: Data
    {
        public int[] Elements;
        private TextureDataStringTable() { }
        public static TextureDataStringTable Read(BinaryReader reader, int length)
        {
            var size = length / 4;
            var textures = new TextureDataStringTable();
            textures.Elements = new int[size];
            for (var i = 0; i < size; i++)
            {
                textures.Elements[i] = reader.ReadInt32();
            }
            return textures;
        }
    }
}
