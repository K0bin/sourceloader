using System;
using System.Collections.Generic;
using System.Text;

namespace Csgo.Renderer
{
    public static class MapLoaderExtension
    {
        public static RendererPart[] PrepareForRendering(this MapLoader.MapLoader loader)
        {/*
            var parts = new List<RendererPart>();
            foreach (var (key, value) in indicesByTexture)
            {
                indices.AddRange(value);
                string textureName = key;
                SourceMaterial material = materials[key];

                parts.Add(new RendererPart()
                {
                    Name = key,
                    Texture = materials.LoadTexture(material?.BaseTextureName),
                    Material = material,
                    IndicesCount = value.Count
                });
            }
            Parts = parts.ToArray();*/
            return null;
        }
    }
}
