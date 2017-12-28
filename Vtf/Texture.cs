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
            header = reader.ReadStructure<Header>();
            //var testHeader = reader.ReadStructure<Header>();
            //reader.BaseStream.Position = startPosition;
            //header = Header.Read(reader);
            if (header.Signature != Header.ExpectedSignature)
            {
                throw new Exception("VTF signature doesn't match.");
            }
            reader.BaseStream.Position = header.HeaderSize;

            var lowResInfo = header.LowResImageFormat.GetInfo();
            var hasLowRes = header.LowResImageWidth != 0 && header.LowResImageHeight != 0 && lowResInfo != null && lowResInfo.HasValue;
            Mipmaps = new MipMap[header.MipmapCount];
            if (hasLowRes)
            {
                var data = reader.ReadBytes(header.LowResImageWidth * header.LowResImageHeight * lowResInfo.Value.TotalBits / 8);
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
            if (header.Version[0] >= 7 && header.Version[1] >= 3)
            {
                reader.ReadBytes((int)header.NumResources * 8);
            }

            var info = header.HighResImageFormat.GetInfo().Value;
            //for (var mip = header.MipmapCount - 1; mip >= 0; mip--)
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

            if (reader.BaseStream.Position != startPosition + length)
            {
                //throw new Exception($"{reader.BaseStream.Position - startPosition + length} bytes left in texture file");
            }
        }
    }
}
