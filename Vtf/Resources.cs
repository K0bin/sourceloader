using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Source.Vtf
{
    enum Resources
    {
        Unknown,
        Thumbnail,
        Image,
        CRC,
    }

    public static class ResourcesExtension
    {
        internal static Dictionary<Resources, int> BuildResourceDictionary(this Header header, BinaryReader reader)
        {
            var thumbnailInfo = header.LowResImageFormat.GetInfo();
            var hasThumbnail = header.LowResImageWidth != 0 && header.LowResImageHeight != 0 && thumbnailInfo != null && thumbnailInfo.HasValue;
            var resourceDictionary = new Dictionary<Resources, int>();
            if (header.Version[0] >= 7 && header.Version[1] >= 3)
            {
                for (int i = 0; i < header.NumResources; i++)
                {
                    var a = reader.ReadByte();
                    var b = reader.ReadByte();
                    var c = reader.ReadByte();
                    var d = reader.ReadByte();
                    var value = reader.ReadInt32();

                    var id = a | (b << 8) | (c << 16) | (d << 24);
                    Resources resource = Resources.Unknown;
                    switch (id)
                    {
                        case 0x01:
                            resource = Resources.Thumbnail;
                            break;

                        case 0x30:
                            resource = Resources.Image;
                            break;
                    }

                    if (resource != Resources.Unknown)
                    {
                        resourceDictionary.Add(resource, value);
                    }
                }
            }
            else
            {
                if (hasThumbnail)
                {
                    resourceDictionary.Add(Resources.Thumbnail, (int)header.HeaderSize);
                }
                resourceDictionary.Add(Resources.Image, (int)header.HeaderSize + header.LowResImageWidth * header.LowResImageHeight * thumbnailInfo.Value.TotalBits / 8);
            }
            return resourceDictionary;
        }
    }

}
