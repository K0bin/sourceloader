using Csgo.MapLoader;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Csgo.MapLoader
{
    public class Model
    {
        public Mesh Mesh
        {
            get; protected set;
        }

        public SourceMaterial[] Materials
        {
            get;
            protected set;
        }

        public Vector3 Position
        { get; set; }
    }
}
