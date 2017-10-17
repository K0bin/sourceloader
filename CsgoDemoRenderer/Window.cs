using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Platform;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using CsgoDemoRenderer.Bsp;
using System.IO;
using OpenTK.Input;

namespace CsgoDemoRenderer
{
    class Window: GameWindow
    {
        private Map map;
        private MapRenderer renderer;
        private Player player;
        private Dictionary<Key, bool> isKeyDown = new Dictionary<Key, bool>();
        public Window()
            : base(1280, 720, GraphicsMode.Default, "CS Renderer", GameWindowFlags.Default, DisplayDevice.Default, 4, 5, GraphicsContextFlags.ForwardCompatible | GraphicsContextFlags.Debug)
        {
            var reader = new BinaryReader(new FileStream(@"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\csgo\maps\aim_redline.bsp", FileMode.Open));
            //var reader = new BinaryReader(new FileStream(@"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\csgo\maps\de_overpass.bsp", FileMode.Open));
            //var reader = new BinaryReader(new FileStream(@"D:\testmap.bsp", FileMode.Open));
            map = Map.Load(reader);
            renderer = new MapRenderer(map);

            player = new Player(new System.Numerics.Vector3(0, 0, 5), new System.Numerics.Vector3(), 1.57f, 1280, 720);
            renderer.Camera = player.camera;

            var version = GL.GetString(StringName.Version);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            isKeyDown[e.Key] = true;
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            isKeyDown[e.Key] = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            renderer.Initialize();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            player.Update((float)e.Time, true, isKeyDown);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            renderer.Render();
            SwapBuffers();
        }
    }
}
