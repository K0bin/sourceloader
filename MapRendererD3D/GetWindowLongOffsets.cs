using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotGame.Platform.Windows
{
    internal enum GetWindowLongOffsets : int
    {
        WNDPROC = (-4),
        HINSTANCE = (-6),
        HWNDPARENT = (-8),
        STYLE = (-16),
        EXSTYLE = (-20),
        USERDATA = (-21),
        ID = (-12),
    }
}
