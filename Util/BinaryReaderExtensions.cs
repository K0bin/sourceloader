using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Util
{
    public static class BinaryReaderExtensions
    {
        public static T ReadStruct<T>(this BinaryReader reader, int size = 0) where T: struct
        {
            if (size <= 0)
            {
                size = Marshal.SizeOf<T>();
            }
            var data = reader.ReadBytes(size);
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var structure = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();
            return structure;
        }
        public static T[] ReadStructArray<T>(this BinaryReader reader, int length, int structSize = 0) where T : struct
        {
            if (structSize <= 0)
            {
                structSize = Marshal.SizeOf<T>();
            }
            var count = length / structSize;

            var array = new T[count];
            for (var i = 0; i < count; i++)
            {
                array[i] = reader.ReadStruct<T>(structSize);
            }
            return array;
        }
        public static T[] ReadStructArrayCount<T>(this BinaryReader reader, int count, int structSize = 0) where T : struct
        {
            if (structSize <= 0)
            {
                structSize = Marshal.SizeOf<T>();
            }

            var array = new T[count];
            for (var i = 0; i < count; i++)
            {
                array[i] = reader.ReadStruct<T>(structSize);
            }
            return array;
        }
        public static string ReadNullTerminatedAsciiString(this BinaryReader reader, int maxLength = -1)
        {
            StringBuilder builder = new StringBuilder();
            var i = 0;
            byte nextChar;
            while ((nextChar = reader.ReadByte()) != 0 && maxLength == -1 || i < maxLength)
            {
                builder.Append((char)nextChar);
                i++;
            }
            return builder.ToString();
        }
    }
}
