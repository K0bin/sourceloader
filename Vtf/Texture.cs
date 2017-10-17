using System;
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
            var startPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = startPosition + 12;
            var size = reader.ReadUInt32();
            reader.BaseStream.Position = startPosition;
            header = reader.ReadStructure<Header>((int)size);

            if (header.Version[1] >= 3)
            {
                reader.ReadBytes(8 * (int)header.NumResources);
            }

            var lowResInfo = header.LowResImageFormat.GetInfo();
            var hasLowRes = header.LowResImageWidth != 0 && header.LowResImageHeight != 0 && lowResInfo != null && lowResInfo.HasValue;
            Mipmaps = new MipMap[header.MipmapCount + (hasLowRes ? 1 : 0)];
            if (hasLowRes)
            {
                var data = reader.ReadBytes(header.LowResImageWidth * header.LowResImageHeight * lowResInfo.Value.TotalBits / 8);
                var mipData = new byte[1, 1, data.Length];
                for (var dI = 0; dI < data.Length; dI++)
                {
                    mipData[0, 0, dI] = data[dI];
                }
                Thumbnail = new Thumbnail
                {
                    Data = data,
                    Format = header.LowResImageFormat,
                    Info = lowResInfo.Value,
                    Width = header.LowResImageWidth,
                    Height = header.LowResImageHeight
                };
            }

            //OTHER RESOURCE DATA

            var info = header.HighResImageFormat.GetInfo().Value;
            for (var mip = 0; mip < header.MipmapCount; mip++)
            {
                var width = Math.Max((int)(header.Width / Math.Pow(2, mip)), 4);
                var height = Math.Max((int)(header.Height / Math.Pow(2, mip)), 4);
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

            if (reader.BaseStream.Position != startPosition + length)
            {
                //throw new Exception($"{reader.BaseStream.Position - startPosition + length} bytes left in texture file");
            }
        }
    }
}
