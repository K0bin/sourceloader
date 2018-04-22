using Csgo.MapLoader;
using System;
using System.Collections.Generic;
using System.Text;

namespace Csgo.Renderer
{
    public struct RendererPart //awful name
    {
        public string Name;
        public int IndicesCount;
        public SourceMaterial Material;
        public ValveTextureFormat.SourceTexture Texture;
    }
}
