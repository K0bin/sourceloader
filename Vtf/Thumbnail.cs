using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Vtf
{
    public struct Thumbnail
    {
        public byte[] Data;
        public ImageFormat Format;
        public ImageFormatInfo Info;
        public int Width, Height;
    }
}
