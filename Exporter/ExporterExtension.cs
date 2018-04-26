using Source.Bsp;
using Source.MapLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Source.Exporter
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
            foreach (var v in model.Mesh.Vertices)
            {
                var _v = TransformVertexForExport(v);
                writer.WriteLine($"v {_v.Position.X} {_v.Position.Y} {_v.Position.Z}");
                writer.WriteLine($"vt {_v.TextureCoord.X} {_v.TextureCoord.Y}");
            }
            foreach (var part in model.Mesh.Parts)
            {
                for (var i = 0; i <= part.Indices.Length - 3; i += 3)
                {
                    writer.WriteLine($"f {part.Indices[i] + 1} {part.Indices[i + 1] + 1} {part.Indices[i + 2] + 1}");
                    //writer.WriteLine($"f {map.Indices[i] + 1}/{map.Indices[i] + 1} {map.Indices[i + 1] + 1}/{map.Indices[i + 1] + 1} {map.Indices[i + 2] + 1}/{map.Indices[i + 2] + 1}");
                }
            }
            writer.Close();
        }
    }
}
