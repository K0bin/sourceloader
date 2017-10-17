using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using SharpDX.DirectInput;

namespace CsgoDemoRenderer
{
    struct MouseState
    {
        public int X;
        public int Y;
    }
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
        MouseState currentState;

        private DirectInput directInput;
        private Mouse mouse;
        private Keyboard keyboard;

        public Player(Vector3 _position, Vector3 _rotation, float fov, int width, int height)
        {
            position = _position;
            rotation = _rotation;
            Initialize(fov, width, height);
        }

        void Initialize(float fov, int width, int height)
        {
            directInput = new DirectInput();
            mouse = new Mouse(directInput);
            mouse.Acquire();
            keyboard = new Keyboard(directInput);
            keyboard.Acquire();
            camera = new Camera(fov, width, height);
        }

        public void Update(float delta, bool hasFocus)
        {
            // rotation.Z: Kamera nach rechts/links kippen
            // rotation.X: hoch/runter schauen
            // rotation.Y: rechts/links drehen

            //Mouse.SetPosition(128, 128);
            if (hasFocus)
            {
                oldState = currentState;
                currentState = new CsgoDemoRenderer.MouseState()
                {
                    X = mouse.GetCurrentState().X,
                    Y = mouse.GetCurrentState().Y
                };
                var isKeyDown = new Dictionary<Key, bool>();
                var keyboardState = keyboard.GetCurrentState();
                foreach (var key in keyboardState.AllKeys)
                {
                    isKeyDown[key] = keyboardState.IsPressed(key);
                }

                #region Mouse
                Vector2 deltaState = new Vector2((currentState.X - oldState.X) * rotationSpeed * delta, (currentState.Y - oldState.Y) * rotationSpeed * delta);
                deltaState *= 10;

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

                Matrix4x4 rotationMat = Matrix4x4.CreateRotationX(DegreesToRadians(rotation.X)) * Matrix4x4.CreateRotationY(DegreesToRadians(rotation.Y)) * Matrix4x4.CreateRotationZ(DegreesToRadians(rotation.Z));
                #endregion

                #region Keyboard
                var wDown = isKeyDown.ContainsKey(Key.W) && isKeyDown[Key.W];
                var aDown = isKeyDown.ContainsKey(Key.A) && isKeyDown[Key.A];
                var sDown = isKeyDown.ContainsKey(Key.S) && isKeyDown[Key.S];
                var dDown = isKeyDown.ContainsKey(Key.D) && isKeyDown[Key.D];

                //W based Movement
                if (wDown && ((!aDown && !dDown) || (aDown && dDown)) && !sDown)
                {
                    float angle = rotation.Y;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (wDown && aDown && !dDown && !sDown)
                {
                    float angle = rotation.Y + 45;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (wDown && !aDown && dDown && !sDown)
                {
                    float angle = rotation.Y - 45;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }

                //S based Movement
                if (!wDown && ((!aDown && !dDown) || (aDown && dDown)) && sDown)
                {
                    float angle = (float)(rotation.Y + 180);
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (!wDown && !aDown && dDown && sDown)
                {
                    float angle = rotation.Y + 225;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (!wDown && aDown && !dDown && sDown)
                {
                    float angle = rotation.Y + 135;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }

                //A/ D Movement
                if (!aDown && dDown && ((!sDown && !wDown) || (sDown && wDown)))
                {
                    float angle = rotation.Y - 90;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(DegreesToRadians(angle)) * hypothenuse);
                    speed.Z -= adjacent;
                    speed.X -= opposite;
                }
                if (aDown && !dDown && ((!sDown && !wDown) || (sDown && wDown)))
                {
                    float angle = rotation.Y + 90;
                    float hypothenuse = (float)(movementSpeed * 0.1f);
                    float adjacent = (float)(hypothenuse * (float)Math.Cos(DegreesToRadians(angle)));
                    float opposite = (float)(Math.Sin(DegreesToRadians(angle)) * hypothenuse);
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

                if (isKeyDown.ContainsKey(Key.LeftShift) && isKeyDown[Key.LeftShift])
                {
                    movementSpeed = runningSpeed;
                }
                else if (isKeyDown.ContainsKey(Key.LeftControl) && isKeyDown[Key.LeftControl])
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
            }
            camera.Update(position, target);
        }

        private float DegreesToRadians(float degree)
        {
            return (float)((Math.PI / 180) * degree);
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
            projection = Matrix4x4.CreatePerspectiveFieldOfView(fov, (float)width / (float)height, zNear, zFar);
        }

        public void Update(Vector3 _position, Vector3 target)
        {
            this.target = target;
            Position = _position;
            View = Matrix4x4.CreateLookAt(Position, target, new Vector3(0, 1, 0));
            ViewProjection = View * projection;
        }
    }
}
