using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace CsgoDemoRenderer.Vtf
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
    }
    public struct Frame
    {
        public Face[] Faces;
    }
    public struct Face
    {
        public Slice[] Slices;
    }
    public struct Slice
    {
        public byte[] Data;
    }
    public class Texture
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
        public Texture(BinaryReader reader, int length)
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
                for (var mip = 0; mip < header.MipmapCount; mip++)
                {
                    var width = (int)(header.Width / Math.Pow(2, mip));
                    var height = (int)(header.Height / Math.Pow(2, mip));
                    var mipSize = width * height * info.TotalBits / 8;
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
    }
}
