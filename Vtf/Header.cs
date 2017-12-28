﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CsgoDemoRenderer.ValveTextureFormat
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Header
    {
        internal const int Size73 = 80;
        internal const int Size72 = 80;
        internal const int Size71 = 64;

        internal static int ExpectedSignature = 0x00465456;
        internal int Signature;      // File signature ("VTF\0"). (or as little-endian integer, 0x00465456)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        internal uint[] Version;        // version[0].version[1] (currently 7.2).
        internal uint HeaderSize;        // Size of the header struct  (16 byte aligned; currently 80 bytes) + size of the resources dictionary (7.3+).
        internal ushort Width;           // Width of the largest mipmap in pixels. Must be a power of 2.
        internal ushort Height;          // Height of the largest mipmap in pixels. Must be a power of 2.
        internal TextureFlags Flags;         // VTF flags.
        internal ushort Frames;          // Number of frames, if animated (1 for no animation).
        internal ushort FirstFrame;      // First frame in animation (0 based).
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        internal byte[] Padding0;      // reflectivity padding (16 byte alignment).
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        internal float[] Reflectivity;  // reflectivity vector.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        internal byte[] Padding1;      // reflectivity padding (8 byte packing).
        internal float BumpmapScale;     // Bumpmap scale.
        internal ImageFormat HighResImageFormat;    // High resolution image format.
        internal byte MipmapCount;      // Number of mipmaps.
        internal ImageFormat LowResImageFormat; // Low resolution image format (always DXT1).
        internal byte LowResImageWidth; // Low resolution image width.
        internal byte LowResImageHeight;    // Low resolution image height.

        // 7.2+
        internal ushort Depth;           // Depth of the largest mipmap in pixels.
                                // Must be a power of 2. Can be 0 or 1 for a 2D texture (v7.2 only).

        // 7.3+
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        internal byte[] Padding2;      // depth padding (4 byte alignment).
        internal uint NumResources;		// Number of resources this vtf has

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
            var header = reader.ReadStructure<Header>((int)headerSize);
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
