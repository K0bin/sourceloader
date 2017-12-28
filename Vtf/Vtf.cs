using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace CsgoDemoRenderer.ValveTextureFormat
{
    public struct Thumbnail
    {
        public byte[] Data;
        public ImageFormat Format;
        public ImageFormatInfo Info;
        public int Width, Height;
    }
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
    public struct Frame
    {
        public Face[] Faces;
        public override string ToString()
        {
            return $"Frame with {Faces.Length} Faces";
        }
    }
    public struct Face
    {
        public Slice[] Slices;
        public override string ToString()
        {
            return $"Face with {Slices.Length} Slices";
        }
    }
    public struct Slice
    {
        public byte[] Data;
        public override string ToString()
        {
            return $"Slice with {Data.Length} bytes";
        }
    }
    public class VtfFile
    {
        private Header header;
        public ImageFormatInfo HighResFormat
        {
            get; private set;
        }
        public ImageFormatInfo LowResFormat
        {
            get; private set;
        }
        public MipMap[] Mipmaps
        {
            get; private set;
        }
        public Thumbnail Thumbnail
        {
            get; private set;
        }
        public VtfFile(BinaryReader reader, int length)
        {
            header = Header.Read(reader);
            var resourceDictionary = header.BuildResourceDictionary(reader);
            
            if (resourceDictionary.TryGetValue(Resources.Thumbnail, out int thumbnailOffset))
            {
                reader.BaseStream.Position = thumbnailOffset;
                var thumbnailFormatInfo = header.LowResImageFormat.GetInfo();
                var data = reader.ReadBytes(header.LowResImageWidth * header.LowResImageHeight * thumbnailFormatInfo.Value.TotalBits / 8);
                Thumbnail = new Thumbnail
                {
                    Data = data,
                    Format = header.LowResImageFormat,
                    Info = thumbnailFormatInfo.Value,
                    Width = header.LowResImageWidth,
                    Height = header.LowResImageHeight
                };
            }

            if (resourceDictionary.TryGetValue(Resources.Image, out int imageOffset))
            {
                reader.BaseStream.Position = imageOffset;
                var info = header.HighResImageFormat.GetInfo().Value;
                Mipmaps = new MipMap[header.MipmapCount];
                //for (var mip = 0; mip < header.MipmapCount; mip++)
                for (var mip = header.MipmapCount - 1; mip >= 0; mip--)
                {
                    var width = header.Width >> mip;
                    var height = header.Height >> mip;
                    var mipSize = CalculateImageSize(width, height, 1, info);
                    var frames = new Frame[header.Frames];
                    for (var frame = header.FirstFrame; frame < header.Frames; frame++)
                    {
                        frames[frame] = new Frame();
                        var faces = new Face[1];
                        for (var face = 0; face < 1; face++)
                        {
                            var slices = new Slice[Math.Max((int)header.Depth, 1)];
                            for (var slice = 0; slice < slices.Length; slice++)
                            {
                                slices[slice].Data = reader.ReadBytes(mipSize);
                            }
                            faces[face].Slices = slices;
                        }
                        frames[frame].Faces = faces;
                    }
                    Mipmaps[mip] = new MipMap
                    {
                        Frames = frames,
                        Format = header.HighResImageFormat,
                        Info = info,
                        Width = width,
                        Height = height
                    };
                }
            }
        }

        private int CalculateImageSize(int width, int height, int depth, ImageFormatInfo formatInfo)
        {
            switch (formatInfo.Format)
            {
                case ImageFormat.Dxt1:
                case ImageFormat.Dxt1OneBitAlpha:
                    if (width < 4 && width > 0) width = 4;
                    if (height < 4 && height > 0) height = 4;

                    return ((width + 3) / 4) * ((height + 3) / 4) * 8 * depth;
                case ImageFormat.Dxt3:
                case ImageFormat.Dxt5:
                    if (width < 4 && width > 0) width = 4;
                    if (height < 4 && height > 0) height = 4;

                    return ((width + 3) / 4) * ((height + 3) / 4) * 16 * depth;
                default:
                    return width * height * depth * formatInfo.TotalBits;
            }
        }

        public VtfFile(ImageFormatInfo format, MipMap[] mipmaps, ImageFormatInfo thumbnailFormat, Thumbnail thumbnail)
        {
            this.HighResFormat = format;
            this.LowResFormat = thumbnailFormat;
            this.Thumbnail = thumbnail;
            this.Mipmaps = mipmaps;
        }
    }
}
