using Source.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Source.MapLoader
{
    public class SourceMaterial: Resource
    {
        private const string ShaderLightMappedGeneric = "lightmappedGeneric";

        public string ShaderName
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
                if (Values.TryGetValue("basetexture", out string name))
                {
                    return name;
                }
                else
                {
                    if (Values.TryGetValue("phong", out string name1))
                    {
                        return name;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public SourceMaterial(string textureName)
        {
            Values.Add("basetexture", textureName);
            ShaderName = ShaderLightMappedGeneric;
        }

        public SourceMaterial(BinaryReader reader, int length)
        {
            var text = Encoding.ASCII.GetString(reader.ReadBytes(length));
            var blockStart = text.IndexOf('{');
            ShaderName = text.Substring(0, blockStart).Replace("\"", "").Trim();
            var blockEnd = text.IndexOf('}');
            var block = text.Substring(blockStart, blockEnd - blockStart).Trim();
            var lines = block.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var trimmedLine = line.Replace("$", "").Replace("%", "").Replace("\"", "").Replace("'", "").Trim();
                var keyEnd = trimmedLine.IndexOf(' ');
                if (keyEnd == -1)
                {
                    continue;
                }
                var key = trimmedLine.Substring(0, keyEnd).ToLower();
                var value = trimmedLine.Substring(keyEnd + 1);
                Values[key] = value;
            }
        }
    }
}
