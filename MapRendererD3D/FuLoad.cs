using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotGame.Platform.Windows
{
    [Flags]
    internal enum FuLoad : uint
    {
        LR_CREATEDIBSECTION = 0x00002000,
        LR_DEFAULTCOLOR = 0x00000000,
        LR_DEFAULTSIZE = 0x00000040,
        LR_LOADFROMFILE = 0x00000010,
        LR_LOADMAP3DCOLORS = 0x00001000,
        LR_LOADTRANSPARENT = 0x00000020,
        LR_MONOCHROME = 0x00000001,
        LR_SHARED = 0x00008000,
        LR_VGACOLOR = 0x00000080
    }
}
