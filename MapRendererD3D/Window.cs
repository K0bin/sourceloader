using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotGame.Platform.Windows
{
    public class Window
    {
        private Win32.WindowProcedure callback;

        private Thread thread;

        private const string ClassName = "dotgamewindow";

        private bool isTitleDirty = false;
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value == null)
                    value = string.Empty;

                if (title != value)
                {
                    isTitleDirty = true;
                    title = value;
                }
            }
        }

        private bool isWidthDirty = false;
        private uint width;
        public uint Width
        {
            get { return width; }
            set
            {
                if (width != value)
                {
                    width = value;
                    isWidthDirty = true;
                }
            }
        }

        private bool isHeightDirty = false;
        private uint height;
        public uint Height
        {
            get { return height; }
            set
            {
                if (height != value)
                {
                    height = value;
                    isHeightDirty = true;
                }
            }
        }

        private bool isXDirty = false;
        private uint x;
        public uint X
        {
            get { return x; }
            set
            {
                if (x != value)
                {
                    x = value;
                    isXDirty = true;
                }
            }
        }
        private bool isYDirty = false;
        private uint y;
        public uint Y
        {
            get { return y; }
            set
            {
                if (y != value)
                {
                    y = value;
                    isYDirty = true;
                }
            }
        }

        public IntPtr Handle
        {
            private set;
            get;
        }

        internal Window(string title, uint x, uint y, uint width, uint height)
        {
            Title = title;
            Width = width;
            Height = height;
            X = x;
            Y = y;
            thread = new Thread(CreateWindow);
            thread.Start();
        }

        private void CreateWindow()
        {
            callback = Callback;

            var error = Marshal.GetLastWin32Error();

            IntPtr icon = Win32.LoadIcon(IntPtr.Zero, ((int)IconName.IDI_APPLICATION).ToString());
            //if(icon == IntPtr.Zero)
            //    throw new Exception(GetError());

            IntPtr cursor = Win32.LoadCursor(IntPtr.Zero, ((int)Cursors.IDC_ARROW).ToString());
            //if (cursor == IntPtr.Zero)
            //    throw new Exception(GetError());

            IntPtr background = Win32.GetStockObject(StockObject.WHITE_BRUSH);
            if (background == IntPtr.Zero)
                throw new Exception(GetError());

            WndClassEx ex = new WndClassEx()
            {
                cbSize = (uint)Marshal.SizeOf<WndClassEx>(),
                style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw | ClassStyle.OwnDC,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(callback),
                cbClsExtra = 0,
                cbWndExtra = 0,
                lpszClassName = ClassName,
                lpszMenuName = null,
                hInstance = Process.GetCurrentProcess().Handle,
                hbrBackground = background,
                //hIcon = icon /*Win32.LoadImage(IntPtr.Zero, ((int)IconName.IDI_APPLICATION).ToString(), ImageType.IMAGE_ICON, 16, 16, FuLoad.LR_SHARED)*/,
                //hIconSm = icon/*Win32.LoadImage(IntPtr.Zero, ((int)IconName.IDI_APPLICATION).ToString(), ImageType.IMAGE_ICON, 16, 16, FuLoad.LR_SHARED)*/,
                //hCursor = cursor/*Win32.LoadImage(IntPtr.Zero, ((int)ImageCursors.OCR_NORMAL).ToString(), ImageType.IMAGE_CURSOR, 16, 16, FuLoad.LR_SHARED)*/,
            };
            if (Win32.RegisterClassExW(ref ex) == 0)
                throw new Exception(GetError());

            IntPtr hWnd = Win32.CreateWindowExW(WindowStylesEx.WS_EX_OVERLAPPEDWINDOW | WindowStylesEx.WS_EX_APPWINDOW, ClassName, Title, WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, (int)Width, (int)Height, IntPtr.Zero, IntPtr.Zero, Process.GetCurrentProcess().Handle, IntPtr.Zero);
            if (hWnd == IntPtr.Zero)
                throw new Exception(GetError());

            Win32.ShowWindow(hWnd, ShowWindowCommands.Normal);
            Win32.UpdateWindow(hWnd);

            Handle = hWnd;

            MSG message;
            while (Win32.GetMessageW(out message, hWnd, 0, 0) > 0)
            {
                Win32.TranslateMessage(ref message);
                Win32.DispatchMessageW(ref message);
            }
        }

        private IntPtr Callback(IntPtr hwnd, WindowMessage uMsg, IntPtr wParam, IntPtr LParam)
        {
            switch (uMsg)
            {
                case WindowMessage.DESTROY:
                    Environment.Exit(0);
                    return IntPtr.Zero;

                case WindowMessage.PAINT:
                    if (isWidthDirty || isHeightDirty || isXDirty || isYDirty)
                        Win32.MoveWindow(Handle, (int)x, (int)y, (int)width, (int)height, false);

                    if (isTitleDirty)
                        Win32.SetWindowTextW(Handle, title);

                    isWidthDirty = false;
                    isHeightDirty = false;
                    isXDirty = false;
                    isYDirty = false;
                    isTitleDirty = false;
                    break;
            }

            return Win32.DefWindowProcW(hwnd, uMsg, wParam, LParam);
        }

        private string GetError()
        {
            int error = Marshal.GetLastWin32Error();

            Process proc = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = @"C:\windows\system32\cmd.exe",
                    Arguments = "/c net helpmsg " + error.ToString(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                }
            };

            proc.Start();
            string errorMessage = "Code: "+error + Environment.NewLine + "Message: ";
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                errorMessage += line;
            }

            return errorMessage;
        }
    }
}
