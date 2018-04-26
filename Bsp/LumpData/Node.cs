using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Bsp.LumpData
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Node
    {
        public int PlaneNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] Children;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] Mins;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] Maxs;
        public ushort FirstFace;
        public ushort FacesCount;
        public short Area;
        public short Padding;
    }
}
