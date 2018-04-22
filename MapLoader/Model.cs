using Csgo.MapLoader;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Csgo.MapLoader
{
    public class Model
    {
        public List<(SourceMaterial material, Mesh mesh)> Meshes
        {
            get; protected set;
        } = new List<(SourceMaterial material, Mesh mesh)>();

        public Vector3 Position
        { get; set; }
    }
}
