using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Csgo.Util
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
    }
}
