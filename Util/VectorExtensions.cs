using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Util
{
    public static class VectorExtensions
    {
        public static Vector3 Xyz(this Vector4 vector) => new Vector3(vector.X, vector.Y, vector.Z);
        public static Vector2 Xy(this Vector4 vector) => new Vector2(vector.X, vector.Y);
        public static Vector2 Xy(this Vector3 vector) => new Vector2(vector.X, vector.Y);
    }
}
