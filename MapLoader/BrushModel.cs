using Source.Bsp;
using Source.Bsp.LumpData;
using Source.Util;
using Source.Vtf;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Source.MapLoader
{
    public class BrushModel: Model
    {
        private readonly Map map;
        private readonly ResourceManager resourceManager;

        private readonly Dictionary<string, List<uint>> indicesByTexture = new Dictionary<string, List<uint>>();
        private readonly List<Vertex> vertices = new List<Vertex>();

        public BrushModel(Map map, ResourceManager resourceManager)
        {
            this.map = map;
            this.resourceManager = resourceManager;
            var rootNode = map.Lumps.GetNodes()[0];
            Read(rootNode);

            var meshParts = new MeshPart[indicesByTexture.Count];
            var modelMaterials = new SourceMaterial[indicesByTexture.Count];
            var i = 0;
            foreach (var (materialName, indices) in indicesByTexture)
            {
                var indexPositions = new Dictionary<uint, uint>();

                if (indices.Count % 3 != 0)
                {
                    throw new Exception("Broken triangles");
                }

                meshParts[i] = new MeshPart { Indices = indices.ToArray() };
                modelMaterials[i] = resourceManager.Get<SourceMaterial>(materialName);
                i++;
            }

            Mesh = new Mesh
            {
                Vertices = vertices.ToArray(),
                Parts = meshParts
            };
            Materials = modelMaterials;
        }

        private void Read(Node node)
        {
            var nodes = map.Lumps.GetNodes();
            var leafs = map.Lumps.GetLeafs();
            var leftChild = node.Children[0];
            ReadChild(leftChild);
            var rightChild = node.Children[1];
            ReadChild(rightChild);
        }

        private void ReadChild(int childIndex)
        {
            var nodes = map.Lumps.GetNodes();
            var leafs = map.Lumps.GetLeafs();
            if (childIndex < 0)
            {
                Read(leafs[-1 - childIndex]);
            }
            else
            {
                Read(nodes[childIndex]);
            }
        }

#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
        private void Read(Leaf leaf)
        {
            var bspVertices = map.Lumps.GetVertices();

            var leafFaces = map.Lumps.GetLeafFaces();
            var faces = map.Lumps.GetFaces();
            var edges = map.Lumps.GetEdges();
            var surfEdges = map.Lumps.GetSurfaceEdges();
            var dispInfos = map.Lumps.GetDisplacementInfos();
            for (var leafFaceIndex = leaf.FirstLeafFace; leafFaceIndex < leaf.FirstLeafFace + leaf.LeafFacesCount; leafFaceIndex++)
            {
                var faceIndex = leafFaces[leafFaceIndex];
                var face = faces[faceIndex];
                var texInfo = map.Lumps.GetTextureInfo()[face.TextureInfo];
                var texData = map.Lumps.GetTextureData()[texInfo.TextureData];
                var textureOffset = map.Lumps.GetTextureDataStringTable()[texData.NameStringTableId];
                var textureName = map.Lumps.GetTextureDataString()[textureOffset];
                uint rootVertex = 0;

                SourceMaterial material = resourceManager.Get<SourceMaterial>(textureName);
                SourceTexture texture = resourceManager.Get<SourceTexture>(material?.BaseTextureName);

                float texWidth = texData.Width;
                float texHeight = texData.Height;
                if (texture != null)
                {
                    if (texture.Header.Width != texData.Width || texture.Header.Height != texData.Height)
                    {
                        texWidth = texture.Header.Width;
                        texHeight = texture.Header.Height;
                    }
                }

                var faceVertices = new Dictionary<uint, uint>(); //Just to make sure that there's no duplicates

                for (var surfEdgeIndex = face.FirstEdge; surfEdgeIndex < face.FirstEdge + face.EdgesCount; surfEdgeIndex++)
                {
                    var edgeIndex = surfEdges[surfEdgeIndex];
                    var edge = edges[Math.Abs(edgeIndex)];

                    //Push the two vertices of the first edge
                    if (surfEdgeIndex == face.FirstEdge)
                    {
                        if (!faceVertices.ContainsKey(edge.VertexIndex[edgeIndex > 0 ? 0 : 1]))
                        {
                            rootVertex = edge.VertexIndex[edgeIndex > 0 ? 0 : 1];

                            var position = bspVertices[rootVertex];
                            var vertex = new Vertex
                            {
                                Position = position,
                                TextureCoord = CalculateUV(position, texInfo.TextureVecsS, texInfo.TextureVecsT, texWidth, texHeight)
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

                    //Edge is on opposite side of the first edge => push the 
                    for (var i = 0; i < 2; i++)
                    {
                        if (!faceVertices.ContainsKey(edge.VertexIndex[i]))
                        {
                            var position = bspVertices[edge.VertexIndex[i]];
                            var vertex = new Vertex
                            {
                                Position = position,
                                TextureCoord = CalculateUV(position, texInfo.TextureVecsS, texInfo.TextureVecsT, texWidth, texHeight)
                            };
                            faceVertices.Add(edge.VertexIndex[i], (uint)vertices.Count);
                            vertices.Add(vertex);
                        }
                    }

                    var indices = GetIndices(textureName);

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

                if (face.DisplacementInfo != -1)
                {
                    var dispInfo = dispInfos[face.DisplacementInfo];
                    Read(dispInfo, texInfo);
                }
            }
        }
#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator

        private void Read(DisplacementInfo dispInfo, TextureInfo texInfo)
        {
            var vertices = map.Lumps.GetDisplacementVertices();
            var triangles = map.Lumps.GetDisplacementTriangles();
            var texData = map.Lumps.GetTextureData()[texInfo.TextureData];
            var textureOffset = map.Lumps.GetTextureDataStringTable()[texData.NameStringTableId];
            var textureName = map.Lumps.GetTextureDataString()[textureOffset];
            
            var vertCount = ((1 << (dispInfo.Power)) + 1) * ((1 << (dispInfo.Power)) + 1);
            var triCount = (1 << (dispInfo.Power)) * (1 << (dispInfo.Power)) * 2;

            for (var i = dispInfo.DisplacementVertexStart; i < dispInfo.DisplacementVertexStart + vertCount; i++)
            {
                /*this.vertices.Add(new Vertex
                {
                    Position = vertices[i].Vector
                });*/

               //var indices = GetIndices(textureName);
               // indices.Add((uint)i);
            }
        }

        private Vector2 CalculateUV(Vector3 position, Vector4 s, Vector4 t, float texWidth, float texHeight)
        {
            if (texWidth != 0 && texHeight != 0)
            {
                return new Vector2
                {
                    X = Vector4.Dot(new Vector4(position, 1), s) / texWidth,
                    Y = Vector4.Dot(new Vector4(position, 1), t) / texHeight,
                };
            }
            else
            {
                return new Vector2();
            }
        }

        private List<uint> GetIndices(string textureName)
        {
            List<uint> indices;
            if (!indicesByTexture.TryGetValue(textureName, out indices))
            {
                indices = new List<uint>();
                indicesByTexture[textureName] = indices;
            }
            return indices;
        }
    }
}
