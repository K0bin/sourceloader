using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Platform;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Csgo.Bsp;
using System.IO;
using OpenTK.Input;

namespace Csgo.Renderer
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
            renderer = new MapRenderer();

            player = new Player(new System.Numerics.Vector3(0, 0, 5), new System.Numerics.Vector3(), 1.57f, 1280, 720);
            //renderer.Camera = player.camera;

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
            //renderer.Initialize();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            player.Update((float)e.Time, true, isKeyDown);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            //renderer.Render();
            SwapBuffers();
        }
    }
}
