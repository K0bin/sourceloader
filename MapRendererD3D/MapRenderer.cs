using CsgoDemoRenderer.Bsp;
using CsgoDemoRenderer.MapLoader;
using System;
using System.Collections.Generic;
using System.Text;
using DotGame.Platform.Windows;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using System.IO;
using SharpDX.Mathematics.Interop;
using SharpDX;
using System.Numerics;

namespace CsgoDemoRenderer.MapRendererD3D
{
    public class MapRenderer
    {
        private readonly Map map;
        private readonly MapLoader.MapLoader parser;
        private Buffer vertexBuffer;
        private Buffer indexBuffer;
        private Buffer constantBuffer;

        private Window window;
        private Device device;
        private SwapChain swapChain;

        private Texture2D backBuffer;
        private RenderTargetView renderView;
        private Texture2D depthBuffer;
        private DepthStencilView depthView;

        public Camera Camera
        {
            get; set;
        }

        public MapRenderer(Map map, Window window)
        {
            this.map = map;
            this.parser = new MapLoader.MapLoader(map, @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive");
            parser.Load();
            parser.Export("Export");
            this.window = window;
        }

        public void Initialize()
        {
            // SwapChain description
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
                    new ModeDescription((int)window.Width, (int)window.Height,
                                        new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = window.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Create Device and SwapChain
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.Debug, desc, out device, out swapChain);
            var context = device.ImmediateContext;

            // Ignore all windows events
            var factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(window.Handle, WindowAssociationFlags.IgnoreAll);

            // Compile Vertex and Pixel shaders
            var vertexShaderByteCode = ShaderBytecode.CompileFromFile(Path.Combine("Shader", "BaseShader.hlsl"), "VS", "vs_4_0");
            var vertexShader = new VertexShader(device, vertexShaderByteCode);

            var pixelShaderByteCode = ShaderBytecode.CompileFromFile(Path.Combine("Shader", "BaseShader.hlsl"), "PS", "ps_4_0");
            var pixelShader = new PixelShader(device, pixelShaderByteCode);

            var signature = new ShaderSignature(vertexShaderByteCode);
            //var signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
            // Layout from VertexShader input signature
            var layout = new InputLayout(device, signature, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                        new InputElement("NORMAL", 0, Format.R32G32B32_Float, 12, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 24, 0)
                    });

            // Instantiate Vertex buiffer from vertex data
            vertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, parser.Vertices);
            indexBuffer = Buffer.Create(device, BindFlags.IndexBuffer, parser.Indices);
            /*vertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, new Vertex[] {
                new Vertex
                {
                    Position = new Vector3(-1,-1,1)
                },
                new Vertex
                {
                    Position = new Vector3(1,-1,1)
                },
                new Vertex
                {
                    Position = new Vector3(1,1,1)
                },
                new Vertex
                {
                    Position = new Vector3(-1,1,1)
                },


                new Vertex
                {
                    Position = new Vector3(-1,-1,-1)
                },
                new Vertex
                {
                    Position = new Vector3(1,-1,-1)
                },
                new Vertex
                {
                    Position = new Vector3(1,1,-1)
                },
                new Vertex
                {
                    Position = new Vector3(-1,1,-1)
                },


                new Vertex
                {
                    Position = new Vector3(-1,-1,-1)
                },
                new Vertex
                {
                    Position = new Vector3(-1,-1,1)
                },
                new Vertex
                {
                    Position = new Vector3(-1,1,1)
                },
                new Vertex
                {
                    Position = new Vector3(-1,1,-1)
                },


                new Vertex
                {
                    Position = new Vector3(1,-1,-1)
                },
                new Vertex
                {
                    Position = new Vector3(1,-1,1)
                },
                new Vertex
                {
                    Position = new Vector3(1,1,1)
                },
                new Vertex
                {
                    Position = new Vector3(1,1,-1)
                },


                new Vertex
                {
                    Position = new Vector3(-1,1,-1)
                },
                new Vertex
                {
                    Position = new Vector3(-1,1,1)
                },
                new Vertex
                {
                    Position = new Vector3(1,1,1)
                },
                new Vertex
                {
                    Position = new Vector3(1,1,-1)
                },
                new Vertex
                {
                    Position = new Vector3(-1,-1,-1)
                },
                new Vertex
                {
                    Position = new Vector3(-1,-1,1)
                },
                new Vertex
                {
                    Position = new Vector3(1,-1,1)
                },
                new Vertex
                {
                    Position = new Vector3(1,-1,-1)
                },
            });
            indexBuffer = Buffer.Create(device, BindFlags.IndexBuffer, new uint[] { 0,1,2, 2,3,0, 4,5,6, 6,7,4, 8,9,10, 10,11,8, 12,13,14, 14,15,12, 16,17,18, 18,19,16, 20,21,22, 22,23,20 });*/


            var cb = new ConstantBuffer()
            {
                World = Matrix4x4.Identity,
                ViewProjection = Matrix4x4.Transpose(Matrix4x4.CreateLookAt(new Vector3(0,0, -5), new Vector3(), new Vector3(0,1,0)) * Matrix4x4.CreatePerspectiveFieldOfView(1.57f, 16f / 9f, 0.1f, 10f))
                //ViewProjection = Matrix4x4.CreateLookAt(new Vector3(0, 0, -5), new Vector3(), new Vector3(0, 1, 0)) * Matrix4x4.Identity
                //ViewProjection = Matrix4x4.Transpose(Matrix4x4.CreateLookAt(new Vector3(0, 0, -5), new Vector3(), new Vector3(0, 1, 0)) * Matrix4x4.Identity)
                //ViewProjection = Matrix4x4.Identity
            };
            constantBuffer = Buffer.Create(device, BindFlags.ConstantBuffer, ref cb);

            context.InputAssembler.InputLayout = layout;
            context.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, Utilities.SizeOf<Vertex>(), 0));
            context.VertexShader.SetConstantBuffer(0, constantBuffer);
            context.VertexShader.Set(vertexShader);
            context.InputAssembler.SetIndexBuffer(indexBuffer, Format.R32_UInt, 0);
            context.PixelShader.Set(pixelShader);

            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);

            // Renderview on the backbuffer
            renderView = new RenderTargetView(device, backBuffer);

            // Create the depth buffer
            depthBuffer = new Texture2D(device, new Texture2DDescription()
            {
                Format = Format.D32_Float_S8X24_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = (int)window.Width,
                Height = (int)window.Height,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            });

            // Create the depth buffer view
            depthView = new DepthStencilView(device, depthBuffer);

            // Setup targets and viewport for rendering
            context.Rasterizer.SetViewport(new RawViewportF
            {
                 Width = window.Width,
                 Height = window.Height,
                 MinDepth = 0.0f,
                 MaxDepth = 1.0f,
            });
            context.OutputMerger.SetTargets(depthView, renderView);
        }

        public void Render()
        {
            var context = device.ImmediateContext;
            context.ClearDepthStencilView(depthView, DepthStencilClearFlags.Depth, 1.0f, 0);
            context.ClearRenderTargetView(renderView, new RawColor4(0f, 0.5f, 0.5f, 1));

            var cb = new ConstantBuffer
            {
                ViewProjection = Matrix4x4.Transpose(Camera.ViewProjection),
                //world = Matrix4x4.CreateRotationX(1.57f, new Vector3())
                World = Matrix4x4.Identity
            };
            context.UpdateSubresource<ConstantBuffer>(ref cb, constantBuffer);

            var rootNode = map.Lumps.GetNodes()[0];
            var position = 0;
            for (int i = 0; i < parser.Parts.Length; i++)
            {
                var part = parser.Parts[i];
                //context.DrawIndexed(part.IndicesCount, position * 4, 0);
                position += parser.Parts[i].IndicesCount;
            }
            context.DrawIndexed(parser.Indices.Length, 0, 0);
            //context.DrawIndexed(6 * 6, 0, 0);
            swapChain.Present(0, PresentFlags.None);
        }
    }

    public struct ConstantBuffer
    {
        public Matrix4x4 World;
        public Matrix4x4 ViewProjection;
    }
}
