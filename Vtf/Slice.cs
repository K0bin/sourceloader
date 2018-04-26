using System;
using System.Collections.Generic;
using System.Text;

namespace Source.Vtf
{
    public struct Slice
    {
        public byte[] Data;
        public override string ToString()
        {
            return $"Slice with {Data.Length} bytes";
        }
    }
}
