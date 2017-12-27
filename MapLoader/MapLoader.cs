using CsgoDemoRenderer.Bsp;
using CsgoDemoRenderer.Bsp.LumpData;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using SteamDatabase.ValvePak;
using System.IO;
using CsgoDemoRenderer.Vtf;
using Util;

namespace CsgoDemoRenderer.MapLoader
{
    public struct RendererPart //awful name
    {
        public string Name;
        public int IndicesCount;
        public Material Material;
        public Texture Texture;
    }

    public class MapLoader
    {
        public Vertex[] Vertices
        { get; private set; }
        public uint[] Indices
        { get; private set; }
        public RendererPart[] Parts
        { get; private set; }

        private readonly Dictionary<string, Material> materials = new Dictionary<string, Material>();
        private readonly Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        private readonly Dictionary<string, List<uint>> indicesByTexture = new Dictionary<string, List<uint>>();
        private readonly List<Vertex> vertices = new List<Vertex>();
        private readonly Map map;
        private readonly Package package;
        public MapLoader(Map map, string csgoDirectory)
        {
            this.map = map;
            package = new Package();
            var file = Path.Combine(csgoDirectory, "csgo", "pak01_dir.vpk");
            package.Read(file);
        }

        public void Load()
        {
            var rootNode = map.Lumps.GetNodes()[0];

            BuildBuffers(rootNode);
            this.Vertices = vertices.ToArray();
            var indices = new List<uint>();
            var parts = new List<RendererPart>();
            foreach (var (key, value) in indicesByTexture)
            {
                indices.AddRange(value);
                Material material;
                Texture texture;
                string textureName = key;
                if (!materials.TryGetValue(key, out material))
                {
                    texture = null;
                }
                else
                {
                    textureName = material.BaseTextureName ?? key;
                }
                if (!textures.TryGetValue(key, out texture))
                {
                    texture = null;
                }

                parts.Add(new RendererPart()
                {
                    Name = key,
                    Texture = texture,
                    Material = material,
                    IndicesCount = value.Count
                });
            }
            Indices = indices.ToArray();
            Parts = parts.ToArray();
        }

        private void BuildBuffers(Leaf leaf)
        {
            var bspVertices = map.Lumps.GetVertices();

            var leafBrushes = map.Lumps.GetLeafBrushes();
            var brushes = map.Lumps.GetBrushes();
            var sides = map.Lumps.GetBrusheSides();
            for (var leafBrushIndex = leaf.FirstLeafBrush; leafBrushIndex < leaf.FirstLeafBrush + leaf.LeafBrushesCount; leafBrushIndex++)
            {
                break;
                var brush = brushes[leafBrushes[leafBrushIndex]];
                for (var sideIndex = brush.firstSide; sideIndex < brush.firstSide + brush.SidesCount; sideIndex++)
                {
                    var side = sides[sideIndex];
                }
            }

            var leafFaces = map.Lumps.GetLeafFaces();
            var faces = map.Lumps.GetFaces();
            var edges = map.Lumps.GetEdges();
            var surfEdges = map.Lumps.GetSurfaceEdges();
            for (var leafFaceIndex = leaf.FirstLeafFace; leafFaceIndex < leaf.FirstLeafFace + leaf.LeafFacesCount; leafFaceIndex++)
            {
                var faceIndex = leafFaces[leafFaceIndex];
                var face = faces[faceIndex];
                var texInfo = map.Lumps.GetTextureInfo()[face.TextureInfo];
                var texData = map.Lumps.GetTextureData()[texInfo.TextureData];
                var textureOffset = map.Lumps.GetTextureDataStringTable()[texData.NameStringTableId];
                var textureName = map.Lumps.GetTextureDataString()[textureOffset];
                uint rootVertex = 0;

                Texture texture;
                Material material;
                if (!textures.TryGetValue(textureName, out texture) && !materials.TryGetValue(textureName, out material))
                {
                    var entry = package.FindEntry("materials/" + textureName.ToLower() + ".vtf");
                    if (entry != null)
                    {
                        byte[] textureData;
                        package.ReadEntry(entry, out textureData);
                        var reader = new BinaryReader(new MemoryStream(textureData));
                        texture = new Texture(reader, textureData.Length);
                        reader.Close();
                        textures.Add(textureName, texture);
                        Console.WriteLine("Loaded texture: " + textureName);
                    }
                    else
                    {
                        entry = package.FindEntry("materials/" + textureName.ToLower() + ".vmt");
                        if (entry != null)
                        {
                            byte[] materialData;
                            package.ReadEntry(entry, out materialData);
                            var reader = new BinaryReader(new MemoryStream(materialData));
                            material = new Material(reader, materialData.Length);
                            materials.Add(textureName, material);
                            Console.WriteLine("Loaded material: " + textureName);
                        }
                        else
                        {
                            Console.WriteLine($"Couldn't find texture or material {textureName}");
                        }
                    }
                }

                var faceVertices = new Dictionary<uint, uint>();

                for (var surfEdgeIndex = face.FirstEdge; surfEdgeIndex < face.FirstEdge + face.EdgesCount; surfEdgeIndex++)
                {
                    var edgeIndex = surfEdges[surfEdgeIndex];
                    var edge = edges[Math.Abs(edgeIndex)];

                    if (surfEdgeIndex == face.FirstEdge)
                    {
                        if (!faceVertices.ContainsKey(edge.VertexIndex[edgeIndex > 0 ? 0 : 1]))
                        {
                            rootVertex = edge.VertexIndex[edgeIndex > 0 ? 0 : 1];
                            var vertex = new Vertex();
                            vertex.Position = bspVertices[rootVertex];
                            vertex.TextureCoord = new Vector2
                            {
                                X = (Vector3.Dot(vertex.Position, texInfo.TextureVecsS.Xyz()) + texInfo.TextureVecsS.W) / texData.Width,
                                Y = (Vector3.Dot(vertex.Position, texInfo.TextureVecsT.Xyz()) + texInfo.TextureVecsT.W) / texData.Height,
                            };
                            faceVertices.Add(rootVertex, (uint)vertices.Count);
                            vertices.Add(vertex);
                        }
                        continue;
                    }

                    //Edge must not be connected to the root vertex
                    if (edge.VertexIndex[0] == rootVertex || edge.VertexIndex[1] == rootVertex)
                    {
                        continue;
                    }

                    for (var i = 0; i < 2; i++)
                    {
                        if (!faceVertices.ContainsKey(edge.VertexIndex[i]))
                        {
                            var vertex = new Vertex();
                            vertex.Position = bspVertices[edge.VertexIndex[i]];
                            vertex.Normal = Vector3.Normalize(Vector3.Cross(bspVertices[rootVertex] - bspVertices[edge.VertexIndex[edgeIndex > 0 ? 0 : 1]], bspVertices[rootVertex] - bspVertices[edge.VertexIndex[edgeIndex > 0 ? 1 : 0]]));
                            vertex.TextureCoord = new Vector2
                            {
                                //X = (texInfo.TextureVecs[0] * vertex.Position.X + texInfo.TextureVecs[1] * vertex.Position.Y + texInfo.TextureVecs[2] * vertex.Position.Z + texInfo.TextureVecs[3]) / (texData.Width * 1.0f),
                                X = (Vector3.Dot(vertex.Position, texInfo.TextureVecsS.Xyz()) + texInfo.TextureVecsS.W) / texData.Width,
                                Y = (Vector3.Dot(vertex.Position, texInfo.TextureVecsT.Xyz()) + texInfo.TextureVecsT.W) / texData.Height,
                            //Y = (texInfo.TextureVecs[4] * vertex.Position.X + texInfo.TextureVecs[5] * vertex.Position.Y + texInfo.TextureVecs[6] * vertex.Position.Z + texInfo.TextureVecs[7]) / (texData.Height * 1.0f),
                        };
                            vertex.LightmapTextureCoord = new Vector2
                            {
                                X = (Vector3.Dot(vertex.Position, texInfo.LightmapVecsS.Xyz()) + texInfo.LightmapVecsS.W) / texData.Width,
                                Y = (Vector3.Dot(vertex.Position, texInfo.LightmapVecsT.Xyz()) + texInfo.LightmapVecsT.W) / texData.Height,
                            };
                            faceVertices.Add(edge.VertexIndex[i], (uint)vertices.Count);
                            vertices.Add(vertex);
                        }
                    }

                    List<uint> indices;
                    if (!indicesByTexture.TryGetValue(textureName, out indices))
                    {
                        indices = new List<uint>();
                        indicesByTexture[textureName] = indices;
                    }

                    indices.Add(faceVertices[rootVertex]);
                    if (edgeIndex < 0)
                    {
                        indices.Add(faceVertices[edge.VertexIndex[1]]);
                        indices.Add(faceVertices[edge.VertexIndex[0]]);
                    }
                    else
                    {
                        indices.Add(faceVertices[edge.VertexIndex[0]]);
                        indices.Add(faceVertices[edge.VertexIndex[1]]);
                    }
                }
            }
        }

        private void BuildBuffers(Node node)
        {
            var nodes = map.Lumps.GetNodes();
            var leafs = map.Lumps.GetLeafs();
            var leftChild = node.Children[0];
            BuildChild(leftChild);
            var rightChild = node.Children[1];
            BuildChild(rightChild);
        }

        private void BuildChild(int childIndex)
        {
            var nodes = map.Lumps.GetNodes();
            var leafs = map.Lumps.GetLeafs();
            if (childIndex < 0)
            {
                BuildBuffers(leafs[-1 - childIndex]);
            }
            else
            {
                BuildBuffers(nodes[childIndex]);
            }
        }
    }
}
