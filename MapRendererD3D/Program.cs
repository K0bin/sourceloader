using CsgoDemoRenderer;
using CsgoDemoRenderer.Bsp;
using DotGame.Platform.Windows;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Diagnostics;
using System.IO;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;

namespace MapRendererD3D
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new Window("CSGO", 0, 0, 1280, 720);
            var reader = new BinaryReader(new FileStream(@"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\csgo\maps\de_overpass.bsp", FileMode.Open));
            var map = Map.Load(reader);
            var renderer = new CsgoDemoRenderer.MapRendererD3D.MapRenderer(map, window);
            renderer.Initialize();
            var player = new Player(new System.Numerics.Vector3(0, 0, 5), new System.Numerics.Vector3(), 1.57f, 1280, 720);
            renderer.Camera = player.camera;
            var watch = new Stopwatch();
            watch.Start();
            long previousElapsed = 0L;
            while (true)
            {
                var time = (watch.ElapsedMilliseconds - previousElapsed) / 1000f;
                previousElapsed = watch.ElapsedMilliseconds;
                player.Update(time, true);
                renderer.Render();
            }
        }
    }
}
