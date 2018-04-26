using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Bsp.LumpData
{
    public class TextureDataString: LumpData
    {
        public string All
        {
            get; private set;
        }
        public TextureDataString(BinaryReader reader, int length)
        {
            var bytes = reader.ReadBytes(length);
            All = Encoding.ASCII.GetString(bytes);
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
