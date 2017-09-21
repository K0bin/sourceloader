using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class TextureInfos : Data
    {
        public TextureInfo[] Elements;
        private TextureInfos() { }

        public static TextureInfos Read(BinaryReader reader, int length)
        {
            var size = length / TextureInfo.SIZE;
            var infos = new TextureInfos();
            infos.Elements = new TextureInfo[size];
            for (var i = 0; i < size; i++)
            {
                infos.Elements[i] = TextureInfo.Read(reader);
            }
            return infos;
        }
    }

    public class TextureInfo
    {
        public const int SIZE = 72;
        public float[,] TextureVecs = new float[2, 4];
        public float[,] LightmapVecs = new float[2, 4];
        public int Flags;
        public int TextureData;
        private TextureInfo() { }

        public static TextureInfo Read(BinaryReader reader)
        {
            var texture = new TextureInfo();
            for (var i = 0; i < 2; i++)
            {
                for (var ii = 0; ii < 4; ii++)
                {
                    texture.TextureVecs[i, ii] = reader.ReadSingle();
                }
            }
            for (var i = 0; i < 2; i++)
            {
                for (var ii = 0; ii < 4; ii++)
                {
                    texture.LightmapVecs[i, ii] = reader.ReadSingle();
                }
            }
            texture.Flags = reader.ReadInt32();
            texture.TextureData = reader.ReadInt32();
            return texture;
        }
    }
}
