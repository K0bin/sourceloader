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
        public List<MaterialMesh> Meshes
        {
            get; protected set;
        } = new List<MaterialMesh>();

        public Vector3 Position
        { get; set; }
    }
}
