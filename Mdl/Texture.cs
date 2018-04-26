using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Texture
    {
        public int NameOffset;
        public int Flags;
        public int Used;
        public int Unused;
        public int Material;
        public int ClientMaterial;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] Unused1;
    }
}
