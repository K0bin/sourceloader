using Csgo.Bsp;
using Csgo.MapLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Csgo.Exporter
{
    public static class ExporterExtension
    {
        public static void Export(this MapLoader.MapLoader map, string path)
        {
            ExportModel(map.Brushes, Path.Combine(path, "brushes.obj"));

            /*foreach (var p in map.Parts)
            {
                var t = p.Texture;
                var mip = t?.Mipmaps[0];
                if (mip != null && mip.HasValue)
                {
                    var data = new byte[mip.Value.Data.GetLength(2)];
                    for (var i = 0; i < mip.Value.Data.GetLength(2); i++)
                    {
                        data[i] = mip.Value.Data[0, 0, i];
                    }
                }
            }*/
        }

        private static MapLoader.Vertex TransformVertexForExport(MapLoader.Vertex vertex)
        {
            return new MapLoader.Vertex
            {
                Position = new System.Numerics.Vector3
                {
                    X = vertex.Position.X * 0.1f,
                    Y = vertex.Position.Z * 0.1f,
                    Z = vertex.Position.Y * 0.1f
                },
                TextureCoord = vertex.TextureCoord
            };
        }

        private static void ExportModel(Model model, string path)
        {
            StreamWriter writer = File.CreateText(path);
            int indexOffset = 0;
            foreach (var (material, mesh) in model.Meshes)
            {
                foreach (var v in mesh.Vertices)
                {
                    var _v = TransformVertexForExport(v);
                    writer.WriteLine($"v {_v.Position.X} {_v.Position.Y} {_v.Position.Z}");
                    writer.WriteLine($"vt {_v.TextureCoord.X} {_v.TextureCoord.Y}");
                }
                for (var i = 0; i <= mesh.Indices.Length - 3; i += 3)
                {
                    if (mesh.Indices[i] > mesh.Vertices.Length)
                    {
                        break;
                    }
                    writer.WriteLine($"f {indexOffset + mesh.Indices[i] + 1} {indexOffset + mesh.Indices[i + 1] + 1} {indexOffset + mesh.Indices[i + 2] + 1}");
                    //writer.WriteLine($"f {map.Indices[i] + 1}/{map.Indices[i] + 1} {map.Indices[i + 1] + 1}/{map.Indices[i + 1] + 1} {map.Indices[i + 2] + 1}/{map.Indices[i + 2] + 1}");
                }
                indexOffset += mesh.Vertices.Length;
            }
            writer.Close();
        }
    }
}
