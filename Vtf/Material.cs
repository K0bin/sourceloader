using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsgoDemoRenderer.ValveTextureFormat
{
    public class Material
    {
        public string Name
        {
            get; private set;
        }
        public Dictionary<string, string> Values
        {
            get; private set;
        } = new Dictionary<string, string>();

        public string BaseTextureName
        {
            get
            {
                string name;
                if (Values.TryGetValue("basetexture", out name))
                {
                    return name;
                }
                else
                {
                    return null;
                }
            }
        }

        public Material(BinaryReader reader, int length)
        {
            var text = Encoding.ASCII.GetString(reader.ReadBytes(length));
            var blockStart = text.IndexOf('{');
            Name = text.Substring(0, blockStart).Replace("\"", "").Trim();
            var blockEnd = text.IndexOf('}');
            var block = text.Substring(blockStart, blockEnd - blockStart).Trim();
            var lines = block.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var trimmedLine = line.Replace("$", "").Replace("\"", "").Replace("'", "");
                var keyEnd = trimmedLine.IndexOf(' ');
                if (keyEnd == -1)
                {
                    continue;
                }
                var key = trimmedLine.Substring(0, keyEnd).ToString();
                var value = trimmedLine.Substring(keyEnd + 1);
                Values[key] = value;
            }
        }
    }
}
