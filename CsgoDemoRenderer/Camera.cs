using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CsgoDemoRenderer
{
    class Player
    {
        public Camera camera;

        public Vector3 position;
        Vector3 target = Vector3.Zero;
        Vector4 speed = Vector4.Zero;

        public Vector3 rotation = Vector3.Zero;
        float maxYaw = 65;
        float minYaw = -65;

        float rotationSpeed = 5f;

        float movementSpeed = 1f;

        float crouchingSpeed = 10f;
        float walkingSpeed = 20f;
        float runningSpeed = 40f;

        //Input 
        MouseState oldState;

        public Player(Vector3 _position, Vector3 _rotation, float fov, int width, int height)
        {
            position = _position;
            rotation = _rotation;
            Initialize(fov, width, height);
        }

        void Initialize(float fov, int width, int height)
        {
            oldState = Mouse.GetState();
            camera = new Camera(fov, width, height);
        }

        public void Update(float delta, bool hasFocus, Dictionary<Key, bool> isKeyDown)
        {
            // rotation.Z: Kamera nach rechts/links kippen
            // rotation.X: hoch/runter schauen
            // rotation.Y: rechts/links drehen

            //Mouse.SetPosition(128, 128);
            if (hasFocus)
            {
                #region Mouse
                Vector2 deltaState = new Vector2((Mouse.GetState().X - oldState.X) * rotationSpeed * delta, (Mouse.GetState().Y - oldState.Y) * rotationSpeed * delta);

                rotation.X -= deltaState.Y * 2;
                rotation.Y -= deltaState.X * 2;


                if (rotation.X > maxYaw)
                {
                    rotation.X = maxYaw;
                }
                else if (rotation.X < minYaw)
                {
                    rotation.X = minYaw;
                }

                Matrix4x4 rotationMat = Matrix4x4.CreateRotationX((float)OpenTK.MathHelper.DegreesToRadians(rotation.X)) * Matrix4x4.CreateRotationY((float)OpenTK.MathHelper.DegreesToRadians(rotation.Y)) * Matrix4x4.CreateRotationZ((float)OpenTK.MathHelper.DegreesToRadians(rotation.Z));
                #endregion

                #region Keyboard
                KeyboardState keyboard = Keyboard.GetState();
                var wDown = isKeyDown.ContainsKey(Key.W) && isKeyDown[Key.W];
                var aDown = isKeyDown.ContainsKey(Key.A) && isKeyDown[Key.A];
                var sDown = isKeyDown.ContainsKey(Key.S) && isKeyDown[Key.S];
                var dDown = isKeyDown.ContainsKey(Key.D) && isKeyDown[Key.D];

                //W based Movement
                if (wDown && ((!aDown && !dDown) || (aDown && dDown)) && !sDown)
                {
                    float angle = rotation.Y;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(OpenTK.MathHelper.DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(OpenTK.MathHelper.DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (wDown && aDown && !dDown && !sDown)
                {
                    float angle = rotation.Y + 45;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(OpenTK.MathHelper.DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(OpenTK.MathHelper.DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (wDown && !aDown && dDown && !sDown)
                {
                    float angle = rotation.Y - 45;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(OpenTK.MathHelper.DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(OpenTK.MathHelper.DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }

                //S based Movement
                if (!wDown && ((!aDown && !dDown) || (aDown && dDown)) && sDown)
                {
                    float angle = (float)(rotation.Y + 180);
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(OpenTK.MathHelper.DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(OpenTK.MathHelper.DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (!wDown && !aDown && dDown && sDown)
                {
                    float angle = rotation.Y + 225;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(OpenTK.MathHelper.DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(OpenTK.MathHelper.DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (!wDown && aDown && !dDown && sDown)
                {
                    float angle = rotation.Y + 135;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(OpenTK.MathHelper.DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(OpenTK.MathHelper.DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }

                //A/ D Movement
                if (!aDown && dDown && ((!sDown && !wDown) || (sDown && wDown)))
                {
                    float angle = rotation.Y - 90;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(OpenTK.MathHelper.DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(OpenTK.MathHelper.DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (aDown && !dDown && ((!sDown && !wDown) || (sDown && wDown)))
                {
                    float angle = rotation.Y + 90;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(OpenTK.MathHelper.DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(OpenTK.MathHelper.DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }

                if (isKeyDown.ContainsKey(Key.Q) && isKeyDown[Key.Q])
                {
                    position.Y += movementSpeed * delta * 0.1f;
                }
                else if (isKeyDown.ContainsKey(Key.E) && isKeyDown[Key.E])
                {
                    position.Y -= movementSpeed * delta * 0.1f;
                }

                if (isKeyDown.ContainsKey(Key.ShiftLeft) && isKeyDown[Key.ShiftLeft])
                {
                    movementSpeed = runningSpeed;
                }
                else if (isKeyDown.ContainsKey(Key.ControlLeft) && isKeyDown[Key.ControlLeft])
                {
                    movementSpeed = crouchingSpeed;
                }
                else
                {
                    movementSpeed = walkingSpeed;
                }

                position += new Vector3(speed.X, speed.Y, speed.Z) * delta;
                target = Vector3.Transform(new Vector3(0, 0, -1), rotationMat) + position;
                speed = Vector4.Zero;
                #endregion

                camera.Update(position, target);

                oldState = Mouse.GetState();
            }
            camera.Update(position, target);
        }
    }

    public class Camera
    {
        private Matrix4x4 projection = new Matrix4x4();
        public Matrix4x4 View = new Matrix4x4();
        public Matrix4x4 ViewProjection = new Matrix4x4();

        public Vector3 Position;
        private Vector3 target;
        private Vector3 up = new Vector3(0, 1, 0);

        private float zNear = 0.1f;
        private float zFar = 100.0f;

        public float Exposure = 1f;
        public float AdaptationSpeed = 0.001f;

        public int WindowWidth = 0;
        public int WindowHeight = 0;

        public Camera(float fov, int width, int height)
        {
            projection = Matrix4x4.CreatePerspectiveFieldOfView(1.57f, 1280 / 720, 0.01f, 100f);
        }

        public void Update(Vector3 _position, Vector3 target)
        {
            this.target = target;
            Position = _position;
            View = Matrix4x4.CreateLookAt(Position, target, new Vector3(0, 1, 0));
            ViewProjection = Matrix4x4.CreateRotationX(-1.57f) * Matrix4x4.CreateScale(0.01f) * View * projection;
        }
    }
}
