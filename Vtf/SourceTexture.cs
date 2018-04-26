using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Source.Common;

namespace Source.Vtf
{
    public class SourceTexture: Resource
    {
        public Header Header
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

        public SourceTexture(BinaryReader reader, int length) //length parameter is necessary for instance creation via reflection
        {
            Header = Header.Read(reader);
            var resourceDictionary = Header.BuildResourceDictionary(reader);
            
            if (resourceDictionary.TryGetValue(Resources.Thumbnail, out int thumbnailOffset))
            {
                reader.BaseStream.Position = thumbnailOffset;
                var thumbnailFormatInfo = Header.LowResImageFormat.GetInfo();
                var data = reader.ReadBytes(Header.LowResImageWidth * Header.LowResImageHeight * thumbnailFormatInfo.Value.TotalBits / 8);
                Thumbnail = new Thumbnail
                {
                    Data = data,
                    Format = Header.LowResImageFormat,
                    Info = thumbnailFormatInfo.Value,
                    Width = Header.LowResImageWidth,
                    Height = Header.LowResImageHeight
                };
            }

            if (resourceDictionary.TryGetValue(Resources.Image, out int imageOffset))
            {
                reader.BaseStream.Position = imageOffset;
                var info = Header.HighResImageFormat.GetInfo().Value;
                Mipmaps = new MipMap[Header.MipmapCount];
                //for (var mip = 0; mip < header.MipmapCount; mip++)
                for (var mip = Header.MipmapCount - 1; mip >= 0; mip--)
                {
                    var width = Header.Width >> mip;
                    var height = Header.Height >> mip;
                    var mipSize = CalculateImageSize(width, height, 1, info);
                    var frames = new Frame[Header.Frames];
                    for (var frame = Header.FirstFrame; frame < Header.Frames; frame++)
                    {
                        frames[frame] = new Frame();
                        var faces = new Face[1];
                        for (var face = 0; face < 1; face++)
                        {
                            var slices = new Slice[Math.Max((int)Header.Depth, 1)];
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
                        Format = Header.HighResImageFormat,
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
    }
}
