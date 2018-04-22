using Csgo.MapLoader;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Csgo.MapLoader
{
    public struct MaterialMesh
    {
        public SourceMaterial Material;
        public Mesh Mesh;

        public MaterialMesh(SourceMaterial material, Mesh mesh)
        {
            this.Material = material;
            this.Mesh = mesh;
        }
    }

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
