using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static CsgoDemoRenderer.Bsp.LumpData.Brush;
using System.Runtime.InteropServices;

namespace CsgoDemoRenderer.Bsp.LumpData
{
    public struct Leaf
    {
        public BrushContents Contents;
        public short Cluster;
        //public short Area;
        //public short Flags;
        public short AreaFlags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] Mins;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public short[] Maxs;
        public ushort FirstLeafFace;
        public ushort LeafFacesCount;
        public ushort FirstLeafBrush;
        public ushort LeafBrushesCount;
        public short LeafWaterDataId;
    }
}
