using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsgoDemoRenderer.Bsp.LumpData
{
    public class LumpData
    {

    }
    public class ArrayLumpData<T>: LumpData where T: struct
    {
        public T[] Elements
        {
            get; protected set;
        }
        public ArrayLumpData(BinaryReader reader, int length)
        {
            if (reader == null) { return; }
            Elements = reader.ReadStructureArray<T>(length);
        }
    }
}
