using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Source.Util;

namespace Source.Vtf
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header
    {
        public const int Size73 = 80;
        public const int Size72 = 80;
        public const int Size71 = 64;

        public static int ExpectedSignature = 0x00465456;
        public int Signature;      // File signature ("VTF\0"). (or as little-endian integer, 0x00465456)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Version;        // version[0].version[1] (currently 7.2).
        public uint HeaderSize;        // Size of the header struct  (16 byte aligned; currently 80 bytes) + size of the resources dictionary (7.3+).
        public ushort Width;           // Width of the largest mipmap in pixels. Must be a power of 2.
        public ushort Height;          // Height of the largest mipmap in pixels. Must be a power of 2.
        public TextureFlags Flags;         // VTF flags.
        public ushort Frames;          // Number of frames, if animated (1 for no animation).
        public ushort FirstFrame;      // First frame in animation (0 based).
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Padding0;      // reflectivity padding (16 byte alignment).
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] Reflectivity;  // reflectivity vector.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Padding1;      // reflectivity padding (8 byte packing).
        public float BumpmapScale;     // Bumpmap scale.
        public ImageFormat HighResImageFormat;    // High resolution image format.
        public byte MipmapCount;      // Number of mipmaps.
        public ImageFormat LowResImageFormat; // Low resolution image format (always DXT1).
        public byte LowResImageWidth; // Low resolution image width.
        public byte LowResImageHeight;    // Low resolution image height.

        // 7.2+
        public ushort Depth;           // Depth of the largest mipmap in pixels.
                                // Must be a power of 2. Can be 0 or 1 for a 2D texture (v7.2 only).

        // 7.3+
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Padding2;      // depth padding (4 byte alignment).
        public uint NumResources;		// Number of resources this vtf has

        public static Header Read(BinaryReader reader)
        {
            var startPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = startPosition + 4;
            var version = new Version((int)reader.ReadUInt32(), (int)reader.ReadUInt32());
            var headerSize = Header.Size73;
            if (version < new Version(7, 2))
            {
                headerSize = Header.Size71;
            }
            else if (version < new Version(7, 3))
            {
                headerSize = Header.Size72;
            }
            reader.BaseStream.Position = startPosition;
            var header = reader.ReadStruct<Header>((int)headerSize);
            if (version < new Version(7,3) && header.HeaderSize != headerSize)
            {
                throw new Exception("Header sizes don't match.");
            }
            else if (version < new Version(7,3) && (header.HeaderSize - headerSize) / 8 != header.NumResources)
            {
                throw new Exception("Header size doesn't match the number of resources.");
            }
            if (header.Signature != Header.ExpectedSignature)
            {
                throw new Exception("VTF signature doesn't match.");
            }
            return header;
        }
    }
}
