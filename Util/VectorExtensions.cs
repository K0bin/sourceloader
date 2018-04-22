using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Csgo.Util
{
    public static class VectorExtensions
    {
        public static Vector3 Xyz(this Vector4 vector) => new Vector3(vector.x, vector.y, vector.z);
        public static Vector3 Xzy(this Vector4 vector) => new Vector3(vector.x, vector.z, vector.y);
        public static Vector3 Xzy(this Vector3 vector) => new Vector3(vector.x, vector.z, vector.y);
        public static Vector4 Xzyw(this Vector4 vector) => new Vector4(vector.x, vector.z, vector.y, vector.w);
        public static Vector2 Xy(this Vector4 vector) => new Vector2(vector.x, vector.y);
        public static Vector2 Xy(this Vector3 vector) => new Vector2(vector.x, vector.y);
    }
}
