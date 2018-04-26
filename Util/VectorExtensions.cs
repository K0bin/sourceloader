using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Source.Util
{
    public static class VectorExtensions
    {
        public static Vector3 Xyz(this Vector4 vector) => new Vector3(vector.X, vector.Y, vector.Z);
        public static Vector3 Xzy(this Vector4 vector) => new Vector3(vector.X, vector.Z, vector.Y);
        public static Vector3 Xzy(this Vector3 vector) => new Vector3(vector.X, vector.Z, vector.Y);
        public static Vector4 Xzyw(this Vector4 vector) => new Vector4(vector.X, vector.Z, vector.Y, vector.W);
        public static Vector2 Xy(this Vector4 vector) => new Vector2(vector.X, vector.Y);
        public static Vector2 Xy(this Vector3 vector) => new Vector2(vector.X, vector.Y);
    }
}
