using CsgoDemoRenderer.Bsp;
using CsgoDemoRenderer.Bsp.LumpData;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using System.Numerics;
using System.Linq;
using System.Runtime.InteropServices;
using CsgoDemoRenderer.MapLoader;

namespace CsgoDemoRenderer
{
    public class MapRenderer
    {
        private readonly Map map;
        private readonly MapLoader.MapLoader parser;
        private int bspVao;
        private int bspBuffer;
        private int bspIndexBuffer;
        private int program;
        private Dictionary<string, int> textures = new Dictionary<string, int>();

        public Camera Camera
        {
            get; set;
        }

        public MapRenderer(Map map)
        {
            this.map = map;
            this.parser = new MapLoader.MapLoader(map, @"D:\Games\Steam\steamapps\common\Counter-Strike Global Offensive");
            parser.Load();
            parser.Export("Export");
        }

        public void Initialize()
        {
            GL.DebugMessageCallback((source, type, id, severity, length, message, userParam) => {
                // Ignores
                if (id == 0x00020071) return; // memory usage
                if (id == 0x00020084) return; // Texture state usage warning: Texture 0 is base level inconsistent. Check texture size.
                if (id == 0x00020061) return; // Framebuffer detailed info: The driver allocated storage for renderbuffer 1.
                if (id == 0x00020004) return; // Usage warning: Generic vertex attribute array ... uses a pointer with a small value (...). Is this intended to be used as an offset into a buffer object?
                if (id == 0x00020072) return; // Buffer performance warning: Buffer object ... (bound to ..., usage hint is GL_STATIC_DRAW) is being copied/moved from VIDEO memory to HOST memory.
                if (id == 0x00020074) return; // Buffer usage warning: Analysis of buffer object ... (bound to ...) usage indicates that the GPU is the primary producer and consumer of data for this buffer object.  The usage hint s upplied with this buffer object, GL_STATIC_DRAW, is inconsistent with this usage pattern.  Try using GL_STREAM_COPY_ARB, GL_STATIC_COPY_ARB, or GL_DYNAMIC_COPY_ARB instead.
                Console.WriteLine($"Error: Source: {source}, Type: {type}, Id: {id}, Severity: {severity}, Message: {Marshal.PtrToStringAnsi(message, length)}");
                //Console.ReadKey();
            }, IntPtr.Zero);
            GL.DebugMessageControl(DebugSourceControl.DontCare, DebugTypeControl.DontCare, DebugSeverityControl.DontCare, 0, new int[0], true);

            int count = parser.Parts.Count(p => p.Texture != null);
            int[] oglTextures = new int[count];
            GL.CreateTextures(TextureTarget.Texture2D, count, oglTextures);
            var texI = 0;
            foreach (var part in parser.Parts)
            {
                var texture = part.Texture;
                if (texture == null || textures.ContainsKey(part.Name))
                {
                    continue;
                }
                var oglTexture = oglTextures[texI];
                GL.TextureParameter(oglTexture, TextureParameterName.TextureBaseLevel, 0);
                GL.TextureParameter(oglTexture, TextureParameterName.TextureMaxLevel, texture.Mipmaps.Length);
                texI++;
                if (texture.Mipmaps[0].Format == Vtf.ImageFormat.Dxt1)
                {
                    if (part.Texture.Mipmaps.Length < 1 || part.Texture.Mipmaps[0].Width < 1 || part.Texture.Mipmaps[0].Height < 1 || oglTexture == 0)
                    {
                        continue;
                    }
                    GL.TextureStorage2D(oglTexture, part.Texture.Mipmaps.Length, (SizedInternalFormat)All.CompressedRgbS3tcDxt1Ext, part.Texture.Mipmaps[0].Width, part.Texture.Mipmaps[0].Height);
                    if (GL.GetError() != ErrorCode.NoError)
                    {
                        //throw new Exception();
                    }

                    for (var i = 0; i < texture.Mipmaps.Length; i++)
                    {
                        var mipmap = texture.Mipmaps[i];
                        var slice = mipmap.Frames?.FirstOrDefault().Faces?.FirstOrDefault().Slices?.FirstOrDefault();
                        if (slice == null || mipmap.Width < 1 || mipmap.Height < 1)
                        {
                            continue;
                        }
                        GL.CompressedTextureSubImage2D(oglTexture, i, 0, 0, mipmap.Width, mipmap.Height, (PixelFormat)All.CompressedRgbS3tcDxt1Ext, slice.Value.Data.Length, slice.Value.Data);
                        if (GL.GetError() != ErrorCode.NoError)
                        {
                            //throw new Exception();
                        }
                    }
                    /*var _mipmap = texture.Mipmaps[0];
                    var _data = new byte[_mipmap.Data.GetLength(2)];
                    for (var ii = 0; ii < _data.Length; ii++)
                    {
                        _data[ii] = _mipmap.Data[0, 0, ii];
                    }
                    GL.BindTexture(TextureTarget.Texture2D, oglTexture);
                    GL.CompressedTexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.CompressedRgbaS3tcDxt1Ext, part.Texture.Mipmaps[0].Width, part.Texture.Mipmaps[0].Height, 0, _data.Length, _data);
                    for (var i = 0; i < texture.Mipmaps.Length; i++)
                    {
                        var mipmap = texture.Mipmaps[i];
                        var data = new byte[mipmap.Data.GetLength(2)];
                        for (var ii = 0; ii < data.Length; ii++)
                        {
                            data[ii] = mipmap.Data[0, 0, ii];
                        }
                        GL.CompressedTexSubImage2D(TextureTarget.Texture2D, i, 0, 0, mipmap.Width, mipmap.Height, PixelFormat.Rgb, data.Length, data);
                    }*/
                }
                else
                {
                    Console.WriteLine($"Unsupported format: {texture.Mipmaps[0].Format}");
                }
                GL.TextureParameter(oglTexture, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TextureParameter(oglTexture, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                textures[part.Name] = oglTexture;
            }

            GL.CreateVertexArrays(1, out bspVao);

            program = GL.CreateProgram();
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Shader", "basic.vertex.glsl")));
            GL.CompileShader(vertexShader);
            var vertexLog = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(vertexLog);
            GL.AttachShader(program, vertexShader);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Shader", "basic.fragment.glsl")));
            GL.CompileShader(fragmentShader);
            var fragmentLog = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(fragmentLog);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);
            GL.ValidateProgram(program);
            var infolog = GL.GetProgramInfoLog(program);
            Console.WriteLine(infolog);

            GL.CreateBuffers(1, out bspIndexBuffer);
            //GL.NamedBufferStorage(bspIndexBuffer, indices.Count * 4, indices.ToArray(), BufferStorageFlags.ClientStorageBit);
            GL.NamedBufferData(bspIndexBuffer, parser.Indices.Length * 4, parser.Indices, BufferUsageHint.StaticDraw);

            GL.CreateBuffers(1, out bspBuffer);
            //GL.NamedBufferStorage(bspIndexBuffer, indices.Count * 4, indices.ToArray(), BufferStorageFlags.ClientStorageBit);
            GL.NamedBufferData(bspBuffer, parser.Vertices.Length * Marshal.SizeOf<Vertex>(), parser.Vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexArrayAttrib(bspVao, 0);
            GL.VertexArrayAttribFormat(bspVao, 0, 3, VertexAttribType.Float, false, 0);
            GL.VertexArrayAttribBinding(bspVao, 0, 0);
            GL.EnableVertexArrayAttrib(bspVao, 1);
            GL.VertexArrayAttribFormat(bspVao, 1, 3, VertexAttribType.Float, false, 12);
            GL.VertexArrayAttribBinding(bspVao, 1, 0);
            GL.EnableVertexArrayAttrib(bspVao, 2);
            GL.VertexArrayAttribFormat(bspVao, 2, 2, VertexAttribType.Float, false, 24);
            GL.VertexArrayAttribBinding(bspVao, 2, 0);
            GL.VertexArrayAttribFormat(bspVao, 3, 2, VertexAttribType.Float, false, 32);
            GL.VertexArrayAttribBinding(bspVao, 2, 0);
            GL.VertexArrayVertexBuffer(bspVao, 0, bspBuffer, IntPtr.Zero, Marshal.SizeOf<Vertex>());
            GL.VertexArrayElementBuffer(bspVao, bspIndexBuffer);

            var error = GL.GetError();
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
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.Uniform1(GL.GetUniformLocation(program, "tex"), 0);

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
            var position = 0;
            for (int i = 0; i < parser.Parts.Length; i++)
            {
                int texture;
                if (textures.TryGetValue(parser.Parts[i].Name, out texture)) {
                    GL.BindTextureUnit(0, texture);
                }
                GL.DrawElements(BeginMode.Triangles, parser.Parts[i].IndicesCount, DrawElementsType.UnsignedInt, position * 4);
                position += parser.Parts[i].IndicesCount;
            }
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}
