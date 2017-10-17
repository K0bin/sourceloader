using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotGame.Platform.Windows
{
    internal enum Cursors : int
    {
        IDC_APPSTARTING = 32650,
        IDC_ARROW = 32512,
        IDC_CROSS = 32515,
        IDC_HAND = 32649,
        IDC_HELP = 32651,
        IDC_IBEAM = 32513,
        IDC_ICON = 32641,
        IDC_NO = 32648,
        [Obsolete("Obsolete for applications marked version 4.0 or later. Use IDC_SIZEALL.")]
        IDC_SIZE = 32640,
        IDC_SIZEALL = 32646,
        IDC_SIZENESW = 32643,
        IDC_SIZENS = 32645,
        IDC_SIZENWSE = 32642,
        IDC_SIZEWE = 32644,
        IDC_UPARROW = 32516,
        IDC_WAIT = 32514,
    }

    internal enum ImageCursors : int
    {
        OCR_APPSTARTING = 32650,
        OCR_NORMAL = 32512,
        OCR_CROSS = 32515,
        OCR_HAND = 32649,
        OCR_HELP = 32651,
        OCR_IBEAM = 32513,
        OCR_NO = 32648,
        OCR_SIZEALL = 32646,
        OCR_SIZENESW = 32643,
        OCR_SIZENS = 32645,
        OCR_SIZENWSE = 32642,
        OCR_SIZEWE = 32644,
        OCR_UP = 32516,
        OCR_WAIT = 32514,
    }
}
