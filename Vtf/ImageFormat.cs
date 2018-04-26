using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Vtf
{
    public enum ImageFormat: uint
    {
        //None = -1,
        Rgba8888 = 0,
        Abgr8888,
        Rgb888,
        Bgr888,
        Rgb565,
        I8,
        Ia88,
        P8,
        A8,
        Rgb888Bluescreen,
        Bgr888Bluescreen,
        Argb8888,
        Bgra8888,
        Dxt1,
        Dxt3,
        Dxt5,
        Bgrx8888,
        Bgr565,
        Bgrx5551,
        Bgra4444,
        Dxt1OneBitAlpha,
        Bgra5551,
        Uv88,
        Uvwq8888,
        Rgba16161616F,
        Rgba16161616,
        Uvlx8888
    }
}
