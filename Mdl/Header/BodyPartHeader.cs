using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.Header
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BodyPartHeader
    {
        public int NameIndex;
        public int ModelCount;
        public int Base;
        public int ModelOffset;
    }
}
