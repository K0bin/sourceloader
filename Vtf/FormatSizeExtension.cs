using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Vtf
{
    public struct ImageFormatInfo {
        public ImageFormat Format;

        public byte? RedBits;
        public byte? GreenBits;
        public byte? BlueBits;
        public byte? AlphaBits;
        public byte TotalBits;
        public bool? IsCompressed;
        public bool IsSupported;

        public ImageFormatInfo(ImageFormat format, byte? redBits, byte? greenBits, byte? blueBits, byte? alphaBits, byte totalBits, bool? isCompressed, bool isSupported)
        {
            Format = format;
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
            { ImageFormat.A8, new ImageFormatInfo(ImageFormat.A8, 0, 0, 0, 8, 8, false, true) },
            { ImageFormat.Argb8888, new ImageFormatInfo(ImageFormat.Argb8888, 8, 8, 8, 8, 32, false, true) },
            { ImageFormat.Abgr8888, new ImageFormatInfo(ImageFormat.Abgr8888, 8, 8, 8, 8, 32, false, true) },
            { ImageFormat.Bgr565, new ImageFormatInfo(ImageFormat.Bgr565, 5, 6, 5, 0, 16, false, true) },
            { ImageFormat.Bgr888, new ImageFormatInfo(ImageFormat.Bgr888, 8, 8, 8, 0, 24, false, true) },
            { ImageFormat.Bgr888Bluescreen, new ImageFormatInfo(ImageFormat.Bgr888Bluescreen, 8, 8, 8, 0, 24, false, true) },
            { ImageFormat.Bgra4444, new ImageFormatInfo(ImageFormat.Bgra4444, 4, 4, 4, 4, 16, false, true) },
            { ImageFormat.Bgra5551, new ImageFormatInfo(ImageFormat.Bgra5551, 5, 5, 5, 1, 16, false, true) },
            { ImageFormat.Bgra8888, new ImageFormatInfo(ImageFormat.Bgra8888, 8, 8, 8, 8, 32, null, true) },
            { ImageFormat.Bgrx5551, new ImageFormatInfo(ImageFormat.Bgrx5551, 5, 5, 5, 1, 16, false, true) },
            { ImageFormat.Bgrx8888, new ImageFormatInfo(ImageFormat.Bgrx8888, 8, 8, 8, 8, 32, false, true) },
            { ImageFormat.Dxt1, new ImageFormatInfo(ImageFormat.Dxt1, null, null, null, 0, 4, true, true) },
            { ImageFormat.Dxt1OneBitAlpha, new ImageFormatInfo(ImageFormat.Dxt1OneBitAlpha, null, null, null, 1, 4, true, true) },
            { ImageFormat.Dxt3, new ImageFormatInfo(ImageFormat.Dxt3, null, null, null, 4, 8, true, true) },
            { ImageFormat.Dxt5, new ImageFormatInfo(ImageFormat.Dxt5, null, null, null, 4, 8, true, true) },
            { ImageFormat.I8, new ImageFormatInfo(ImageFormat.I8, null, null, null, null, 8, false, true) },
            { ImageFormat.Ia88, new ImageFormatInfo(ImageFormat.Ia88, null, null, null, 8, 16, false, true) },
            { ImageFormat.P8, new ImageFormatInfo(ImageFormat.P8, null, null, null, null, 8, false, false) },
            { ImageFormat.Rgb565, new ImageFormatInfo(ImageFormat.Rgb565, 5, 6, 5, 0, 16, false, true) },
            { ImageFormat.Rgb888Bluescreen, new ImageFormatInfo(ImageFormat.Rgb888Bluescreen, 8, 8, 8, 0, 24, false, true) },
            { ImageFormat.Rgba16161616, new ImageFormatInfo(ImageFormat.Rgba16161616, 16, 16, 16, 16, 64, false, true) },
            { ImageFormat.Rgba16161616F, new ImageFormatInfo(ImageFormat.Rgba16161616F, 16, 16, 16, 16, 64, false, true) },
            { ImageFormat.Rgba8888, new ImageFormatInfo(ImageFormat.Rgba8888, 8, 8, 8, 8, 32, false, true) },
            { ImageFormat.Uv88, new ImageFormatInfo(ImageFormat.Uv88, null, null, null, null, 16, false, true) },
            { ImageFormat.Uvlx8888, new ImageFormatInfo(ImageFormat.Uvlx8888, null, null, null, null, 32, false, true) },
            { ImageFormat.Uvwq8888, new ImageFormatInfo(ImageFormat.Uvwq8888, null, null, null, null, 32, false, true) }
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
