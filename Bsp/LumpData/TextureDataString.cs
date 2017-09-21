using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bsp.LumpData
{
    public class TextureDataString: Data
    {
        public string All;
        private TextureDataString() { }
        public static TextureDataString Read(BinaryReader reader, int length)
        {
            var textures = new TextureDataString();
            var bytes = reader.ReadBytes(length);
            var str = Encoding.ASCII.GetString(bytes);
            textures.All = str;
            return textures;
        }

        private string GetPart(int offset)
        {
            var textureName = "";
            for (var i = offset; i < All.Length; i++)
            {
                if (All[i] == '\0')
                {
                    break;
                }
                textureName += All[i];
            }
            return textureName;
        }

        public string this[int i]
        {
            get { return GetPart(i); }
        }
    }
}
