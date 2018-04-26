using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Vtf
{
    public struct MipMap
    {
        public Frame[] Frames;
        public ImageFormat Format;
        public ImageFormatInfo Info;
        public int Width, Height;

        public override string ToString()
        {
            return $"Mipmap with {Frames.Length} Frames, Format: {Format.ToString()}, Width: {Width}, Height: {Height}";
        }
    }
}
