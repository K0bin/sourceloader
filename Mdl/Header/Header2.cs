using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Source.Mdl.Header
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Header2
    {
        public int SrcBoneTransformCount;
        public int SrcBoneTransformIndex;
        public int IllumPositionAttachmentIndex;
        public float FlMaxEyeDeflection;
        public int LinearBoneIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public int[] Unknown;                
    }
}
