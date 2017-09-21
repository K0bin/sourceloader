using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Numerics;

namespace Bsp.LumpData
{
    public class TextureDatas : Data
    {
        public TextureData[] Elements;
        private TextureDatas() { }

        public static TextureDatas Read(BinaryReader reader, int length)
        {
            var size = length / TextureData.SIZE;
            var infos = new TextureDatas();
            infos.Elements = new TextureData[size];
            for (var i = 0; i < size; i++)
            {
                infos.Elements[i] = TextureData.Read(reader);
            }
            return infos;
        }
    }

    public class TextureData
    {
        public const int SIZE = 32;
        public Vector3 Reflectivity;
        public int NameStringTableId;
        public int Width;
        public int Height;
        public int ViewWidth;
        public int viewHeight;
        private TextureData() { }

        public static TextureData Read(BinaryReader reader)
        {
            var texture = new TextureData();
            texture.Reflectivity = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            texture.NameStringTableId = reader.ReadInt32();
            texture.Width = reader.ReadInt32();
            texture.Height = reader.ReadInt32();
            texture.ViewWidth = reader.ReadInt32();
            texture.viewHeight = reader.ReadInt32();
            return texture;
        }
    }
}
