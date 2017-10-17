using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CsgoDemoRenderer.Vtf
{
    public struct MipMap
    {
        public byte[,,] Data;
        public ImageFormat Format;
        public ImageFormatInfo Info;
        public int Width, Height;
    }
    public class Texture
    {
        private Header header;
        public MipMap[] Mipmaps
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
                Mipmaps[0] = new MipMap {
                    Data = mipData,
                    Format = header.LowResImageFormat,
                    Info = lowResInfo.Value,
                    Width = header.LowResImageWidth,
                    Height = header.LowResImageHeight
                };
            }
            if (header.Version[1] > 2)
            {
            }

            var info = header.HighResImageFormat.GetInfo().Value;
            for (var mip = 0; mip < header.MipmapCount; mip++)
            {
                var width = (int)(header.Width / Math.Pow(2, mip));
                var height = (int)(header.Height / Math.Pow(2, mip));
                var mipSize = width * height * info.TotalBits / 8;
                var mipData = new byte[header.Frames, Math.Max(header.Depth, (ushort)1), mipSize];
                for (var frame = 0; frame < header.Frames; frame++)
                {
                    for (var face = 0; face < Math.Max(header.Depth, (ushort)1); face++)
                    {
                        var data = reader.ReadBytes(mipSize);
                        for (var dI = 0; dI < data.Length; dI++)
                        {
                            mipData[frame, face, dI] = data[dI];
                        }

                    }
                }
                Mipmaps[mip] = new MipMap
                {
                    Data = mipData,
                    Format = header.HighResImageFormat,
                    Info = info,
                    Width = width,
                    Height = height
                };
            }

            if (reader.BaseStream.Position > startPosition + length)
            {
                throw new Exception("meh");
            }
        }
    }
}
