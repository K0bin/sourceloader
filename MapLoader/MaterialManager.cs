using Csgo.Vtf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Csgo.MapLoader
{
    public class MaterialManager
    {
        private ResourceManager resourceManager;
        private readonly Dictionary<string, SourceMaterial> materials = new Dictionary<string, SourceMaterial>();
        private readonly Dictionary<string, SourceTexture> textures = new Dictionary<string, SourceTexture>();

        public MaterialManager(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public SourceMaterial this[string name]
        {
            get
            {
                return LoadMaterial(name);
            }
        }

        public SourceMaterial LoadMaterial(string name)
        {
            if (name == null) return null;

            if (!materials.TryGetValue(name, out SourceMaterial material))
            {
                var data = resourceManager["materials/" + name.ToLower() + ".vmt"];
                if (data != null)
                {
                    using (var reader = new BinaryReader(new MemoryStream(data)))
                    {
                        material = new SourceMaterial(reader, data.Length, name);
                    }
                    materials.Add(name, material);
                    Console.WriteLine("Loaded material: " + name);
                }
                else
                {
                    var texture = LoadTexture(name);
                    material = new SourceMaterial(name);
                    if (texture == null)
                    {
                        Console.WriteLine($"Couldn't find texture or material {name}");
                    }
                }
            }
            return material;
        }

        public SourceTexture LoadTexture(string name)
        {
            if (name == null) return null;

            if (!textures.TryGetValue(name, out SourceTexture texture))
            {
                var data = resourceManager["materials/" + name.ToLower() + ".vtf"];
                if (data != null)
                {
                    var reader = new BinaryReader(new MemoryStream(data));
                    texture = new Vtf.SourceTexture(reader);
                    reader.Close();

                    textures.TryAdd(name, texture);
                    Console.WriteLine("Loaded texture: " + name);
                }
            }
            return texture;
        }
    }
}
