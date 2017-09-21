using Bsp;
using Bsp.LumpData;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace CsgoDemoRenderer
{
    public class MapRenderer
    {
        private readonly Map map;
        private int bspVao;
        private int bspBuffer;
        private int bspIndexBuffer;
        private int indicesCount;
        private int program;

        public Camera Camera
        {
            get; set;
        }

        public MapRenderer(Map map)
        {
            this.map = map;
            var textureInfo = map.Lumps.GetTextureInfo();
            var textureData = map.Lumps.GetTextureData();
            var textureDataString = map.Lumps.GetTextureDataString();
            var textureDataTable = map.Lumps.GetTextureDataStringTable();
        }

        public void Initialize()
        {
            GL.DebugMessageCallback((source, type, id, severity, length, message, userParam) => {
                Console.ReadKey();
            }, IntPtr.Zero);
            GL.DebugMessageControl(DebugSourceControl.DontCare, DebugTypeControl.DontCare, DebugSeverityControl.DontCare, 0, new int[0], true);

            GL.CreateVertexArrays(1, out bspVao);

            program = GL.CreateProgram();
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Shader", "basic.vertex.glsl")));
            GL.AttachShader(program, vertexShader);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Shader", "basic.fragment.glsl")));
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);
            var infolog = GL.GetProgramInfoLog(program);

            TraverseBsp();
        }

        public void Render()
        {
            GL.ClearColor(OpenTK.Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.CullFace);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.DepthTest);
            GL.UseProgram(program);
            GL.BindVertexArray(bspVao);

            var mat = Camera.ViewProjection;
            var arr = new float[] { mat.M11, mat.M12, mat.M13, mat.M14, mat.M21, mat.M22, mat.M23, mat.M24, mat.M31, mat.M32, mat.M33, mat.M34, mat.M41, mat.M42, mat.M43, mat.M44 };
            GL.UniformMatrix4(GL.GetUniformLocation(program, "mvp"), 1, false, arr);
            GL.Uniform3(GL.GetUniformLocation(program, "cameraPos"), new OpenTK.Vector3
            {
                X = Camera.Position.X,
                Y = Camera.Position.Y,
                Z = Camera.Position.Z
            });

            var rootNode = map.Lumps.GetNodes()[0];
            //RenderNode(rootNode);
            GL.DrawElements(BeginMode.Triangles, indicesCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
        
        private void TraverseBsp()
        {
            var rootNode = map.Lumps.GetNodes()[0];
            var indices = new List<uint>();
            var vertices = new List<Vertex>();

            Build(rootNode, vertices, indices);

            GL.CreateBuffers(1, out bspIndexBuffer);
            //GL.NamedBufferStorage(bspIndexBuffer, indices.Count * 4, indices.ToArray(), BufferStorageFlags.ClientStorageBit);
            indicesCount = indices.Count;
            GL.NamedBufferData(bspIndexBuffer, indicesCount * 4, indices.ToArray(), BufferUsageHint.StaticDraw);

            GL.CreateBuffers(1, out bspBuffer);
            //GL.NamedBufferStorage(bspIndexBuffer, indices.Count * 4, indices.ToArray(), BufferStorageFlags.ClientStorageBit);
            GL.NamedBufferData(bspBuffer, vertices.Count * Marshal.SizeOf<Vertex>(), vertices.ToArray(), BufferUsageHint.StaticDraw);
            
            GL.EnableVertexArrayAttrib(bspVao, 0);
            GL.VertexArrayAttribFormat(bspVao, 0, 3, VertexAttribType.Float, false, 0);
            GL.VertexArrayAttribBinding(bspVao, 0, 0);
            GL.EnableVertexArrayAttrib(bspVao, 1);
            GL.VertexArrayAttribFormat(bspVao, 1, 3, VertexAttribType.Float, false, 12);
            GL.VertexArrayAttribBinding(bspVao, 1, 0);
            GL.EnableVertexArrayAttrib(bspVao, 2);
            GL.VertexArrayAttribFormat(bspVao, 2, 2, VertexAttribType.Float, false, 24);
            GL.VertexArrayAttribBinding(bspVao, 2, 0);
            GL.VertexArrayVertexBuffer(bspVao, 0, bspBuffer, IntPtr.Zero, Marshal.SizeOf<Vertex>());
            GL.VertexArrayElementBuffer(bspVao, bspIndexBuffer);

            var error = GL.GetError();
        }

        private void Build(Leaf leaf, List<Vertex> vertices, List<uint> indices)
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

                    for (var i = 0; i < 2; i++) {
                        if (!faceVertices.ContainsKey(edge.VertexIndex[i]))
                        {
                            var vertex = new Vertex();
                            vertex.Position = bspVertices[edge.VertexIndex[i]];
                            vertex.Normal = Vector3.Cross(bspVertices[rootVertex] - bspVertices[edge.VertexIndex[edgeIndex > 0 ? 0 : 1]], bspVertices[rootVertex] - bspVertices[edge.VertexIndex[edgeIndex > 0 ? 1 : 0]]);

                            faceVertices.Add(edge.VertexIndex[i], (uint)vertices.Count);
                            vertices.Add(vertex);
                        }
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

        private void Build(Node node, List<Vertex> vertices, List<uint> indices)
        {
            var nodes = map.Lumps.GetNodes();
            var leafs = map.Lumps.GetLeafs();
            var leftChild = node.Children[0];
            BuildChild(leftChild, vertices, indices);
            var rightChild = node.Children[1];
            BuildChild(rightChild, vertices, indices);
        }

        private void BuildChild(int childIndex, List<Vertex> vertices, List<uint> indices)
        {
            var nodes = map.Lumps.GetNodes();
            var leafs = map.Lumps.GetLeafs();
            if (childIndex < 0)
            {
                Build(leafs[-1 - childIndex], vertices, indices);
            }
            else
            {
                Build(nodes[childIndex], vertices, indices);
            }

        }
    }
}
