using System;
using System.Collections.Generic;
using System.Text;

namespace CsgoDemoRenderer.Vtf
{
    public struct ImageFormatInfo {
        public byte? RedBits;
        public byte? GreenBits;
        public byte? BlueBits;
        public byte? AlphaBits;
        public byte TotalBits;
        public bool? IsCompressed;
        public bool IsSupported;

        public ImageFormatInfo(byte? redBits, byte? greenBits, byte? blueBits, byte? alphaBits, byte totalBits, bool? isCompressed, bool isSupported)
        {
            RedBits = redBits;
            GreenBits = greenBits;
            BlueBits = blueBits;
            AlphaBits = alphaBits;
            TotalBits = totalBits;
            IsCompressed = isCompressed;
            IsSupported = isSupported;
        }
    }

    internal static class FormatSizeExtension
    {
        private static Dictionary<ImageFormat, ImageFormatInfo> infos = new Dictionary<ImageFormat, ImageFormatInfo>()
        {
            { ImageFormat.A8, new ImageFormatInfo(0, 0, 0, 8, 8, false, true) },
            { ImageFormat.Argb8888, new ImageFormatInfo(8, 8, 8, 8, 32, false, true) },
            { ImageFormat.Abgr8888, new ImageFormatInfo(8, 8, 8, 8, 32, false, true) },
            { ImageFormat.Bgr565, new ImageFormatInfo(5, 6, 5, 0, 16, false, true) },
            { ImageFormat.Bgr888, new ImageFormatInfo(8, 8, 8, 0, 24, false, true) },
            { ImageFormat.Bgr888Bluescreen, new ImageFormatInfo(8, 8, 8, 0, 24, false, true) },
            { ImageFormat.Bgra4444, new ImageFormatInfo(4, 4, 4, 4, 16, false, true) },
            { ImageFormat.Bgra5551, new ImageFormatInfo(5, 5, 5, 1, 16, false, true) },
            { ImageFormat.Bgra8888, new ImageFormatInfo(8, 8, 8, 8, 32, null, true) },
            { ImageFormat.Bgrx5551, new ImageFormatInfo(5, 5, 5, 1, 16, false, true) },
            { ImageFormat.Bgrx8888, new ImageFormatInfo(8, 8, 8, 8, 32, false, true) },
            { ImageFormat.Dxt1, new ImageFormatInfo(null, null, null, 0, 4, true, true) },
            { ImageFormat.Dxt1OneBitAlpha, new ImageFormatInfo(null, null, null, 1, 4, true, true) },
            { ImageFormat.Dxt3, new ImageFormatInfo(null, null, null, 4, 8, true, true) },
            { ImageFormat.Dxt5, new ImageFormatInfo(null, null, null, 4, 8, true, true) },
            { ImageFormat.I8, new ImageFormatInfo(null, null, null, null, 8, false, true) },
            { ImageFormat.Ia88, new ImageFormatInfo(null, null, null, 8, 16, false, true) },
            { ImageFormat.P8, new ImageFormatInfo(null, null, null, null, 8, false, false) },
            { ImageFormat.Rgb565, new ImageFormatInfo(5, 6, 5, 0, 16, false, true) },
            { ImageFormat.Rgb888Bluescreen, new ImageFormatInfo(8, 8, 8, 0, 24, false, true) },
            { ImageFormat.Rgba16161616, new ImageFormatInfo(16, 16, 16, 16, 64, false, true) },
            { ImageFormat.Rgba16161616F, new ImageFormatInfo(16, 16, 16, 16, 64, false, true) },
            { ImageFormat.Rgba8888, new ImageFormatInfo(8, 8, 8, 8, 32, false, true) },
            { ImageFormat.Uv88, new ImageFormatInfo(null, null, null, null, 16, false, true) },
            { ImageFormat.Uvlx8888, new ImageFormatInfo(null, null, null, null, 32, false, true) },
            { ImageFormat.Uvwq8888, new ImageFormatInfo(null, null, null, null, 32, false, true) }
        };
        internal static ImageFormatInfo? GetInfo(this ImageFormat format)
        {
            ImageFormatInfo info;
            if (infos.TryGetValue(format, out info))
            {
                return info;
            }
            return null;
        }
    }
}
