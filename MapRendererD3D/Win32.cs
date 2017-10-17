using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DotGame.Platform.Windows
{
    using HWND = System.IntPtr;
    using HINSTANCE = System.IntPtr;
    using HMENU = System.IntPtr;
    using HICON = System.IntPtr;
    using HBRUSH = System.IntPtr;
    using HCURSOR = System.IntPtr;
    using HKEY = System.IntPtr;
    using PHKEY = System.IntPtr;

    using LRESULT = System.IntPtr;
    using LPVOID = System.IntPtr;
    using LPCTSTR = System.String;

    using WPARAM = System.IntPtr;
    using LPARAM = System.IntPtr;
    using HANDLE = System.IntPtr;
    using HRAWINPUT = System.IntPtr;

    using BYTE = System.Byte;
    using SHORT = System.Int16;
    using USHORT = System.UInt16;
    using LONG = System.Int32;
    using ULONG = System.UInt32;
    using WORD = System.Int16;
    using DWORD = System.Int32;
    using BOOL = System.Boolean;
    using INT = System.Int32;
    using UINT = System.UInt32;
    using LONG_PTR = System.IntPtr;
    using ATOM = System.UInt16;

    using COLORREF = System.Int32;
    using WNDPROC = System.IntPtr;
    using HDEVNOTIFY = System.IntPtr;

    using HRESULT = System.IntPtr;
    using HMONITOR = System.IntPtr;

    using DWORD_PTR = System.IntPtr;
    using UINT_PTR = System.UIntPtr;

    using REGSAM = System.UInt32;

    internal static class Win32
    {
        /// <summary>
        /// The CreateWindowEx function creates an overlapped, pop-up, or child window with an extended window style; otherwise, this function is identical to the CreateWindow function. 
        /// </summary>
        /// <param name="dwExStyle">Specifies the extended window style of the window being created.</param>
        /// <param name="lpClassName">Pointer to a null-terminated string or a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order word must be zero. If lpClassName is a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, provided that the module that registers the class is also the module that creates the window. The class name can also be any of the predefined system class names.</param>
        /// <param name="lpWindowName">Pointer to a null-terminated string that specifies the window name. If the window style specifies a title bar, the window title pointed to by lpWindowName is displayed in the title bar. When using CreateWindow to create controls, such as buttons, check boxes, and static controls, use lpWindowName to specify the text of the control. When creating a static control with the SS_ICON style, use lpWindowName to specify the icon name or identifier. To specify an identifier, use the syntax "#num". </param>
        /// <param name="dwStyle">Specifies the style of the window being created. This parameter can be a combination of window styles, plus the control styles indicated in the Remarks section.</param>
        /// <param name="x">Specifies the initial horizontal position of the window. For an overlapped or pop-up window, the x parameter is the initial x-coordinate of the window's upper-left corner, in screen coordinates. For a child window, x is the x-coordinate of the upper-left corner of the window relative to the upper-left corner of the parent window's client area. If x is set to CW_USEDEFAULT, the system selects the default position for the window's upper-left corner and ignores the y parameter. CW_USEDEFAULT is valid only for overlapped windows; if it is specified for a pop-up or child window, the x and y parameters are set to zero.</param>
        /// <param name="y">Specifies the initial vertical position of the window. For an overlapped or pop-up window, the y parameter is the initial y-coordinate of the window's upper-left corner, in screen coordinates. For a child window, y is the initial y-coordinate of the upper-left corner of the child window relative to the upper-left corner of the parent window's client area. For a list box y is the initial y-coordinate of the upper-left corner of the list box's client area relative to the upper-left corner of the parent window's client area.
        /// <para>If an overlapped window is created with the WS_VISIBLE style bit set and the x parameter is set to CW_USEDEFAULT, then the y parameter determines how the window is shown. If the y parameter is CW_USEDEFAULT, then the window manager calls ShowWindow with the SW_SHOW flag after the window has been created. If the y parameter is some other value, then the window manager calls ShowWindow with that value as the nCmdShow parameter.</para></param>
        /// <param name="nWidth">Specifies the width, in device units, of the window. For overlapped windows, nWidth is the window's width, in screen coordinates, or CW_USEDEFAULT. If nWidth is CW_USEDEFAULT, the system selects a default width and height for the window; the default width extends from the initial x-coordinates to the right edge of the screen; the default height extends from the initial y-coordinate to the top of the icon area. CW_USEDEFAULT is valid only for overlapped windows; if CW_USEDEFAULT is specified for a pop-up or child window, the nWidth and nHeight parameter are set to zero.</param>
        /// <param name="nHeight">Specifies the height, in device units, of the window. For overlapped windows, nHeight is the window's height, in screen coordinates. If the nWidth parameter is set to CW_USEDEFAULT, the system ignores nHeight.</param> <param name="hWndParent">Handle to the parent or owner window of the window being created. To create a child window or an owned window, supply a valid window handle. This parameter is optional for pop-up windows.
        /// <para>Windows 2000/XP: To create a message-only window, supply HWND_MESSAGE or a handle to an existing message-only window.</para></param>
        /// <param name="hMenu">Handle to a menu, or specifies a child-window identifier, depending on the window style. For an overlapped or pop-up window, hMenu identifies the menu to be used with the window; it can be NULL if the class menu is to be used. For a child window, hMenu specifies the child-window identifier, an integer value used by a dialog box control to notify its parent about events. The application determines the child-window identifier; it must be unique for all child windows with the same parent window.</param>
        /// <param name="hInstance">Handle to the instance of the module to be associated with the window.</param> <param name="lpParam">Pointer to a value to be passed to the window through the CREATESTRUCT structure (lpCreateParams member) pointed to by the lParam param of the WM_CREATE message. This message is sent to the created window by this function before it returns.
        /// <para>If an application calls CreateWindow to create a MDI client window, lpParam should point to a CLIENTCREATESTRUCT structure. If an MDI client window calls CreateWindow to create an MDI child window, lpParam should point to a MDICREATESTRUCT structure. lpParam may be NULL if no additional data is needed.</para></param>
        /// <returns>If the function succeeds, the return value is a handle to the new window.
        /// <para>If the function fails, the return value is NULL. To get extended error information, call GetLastError.</para>
        /// <para>This function typically fails for one of the following reasons:</para>
        /// <list type="">
        /// <item>an invalid parameter value</item>
        /// <item>the system class was registered by a different module</item>
        /// <item>The WH_CBT hook is installed and returns a failure code</item>
        /// <item>if one of the controls in the dialog template is not registered, or its window window procedure fails WM_CREATE or WM_NCCREATE</item>
        /// </list></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern HWND CreateWindowExW(
            WindowStylesEx dwExStyle,
           [MarshalAs(UnmanagedType.LPTStr)]string lpClassName,
           [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName,
           WindowStyles dwStyle,
           int x,
           int y,
           int nWidth,
           int nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindowW(HWND windowHandle);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.U2)]
        internal static extern ATOM RegisterClassExW([In] ref WndClassEx lpwcx);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(HWND hWnd, ShowWindowCommands nCmdShow);

        [DllImport("user32.dll")]
        internal static extern bool UpdateWindow(HWND hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetMessageW(out MSG lpMsg, HWND hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        internal static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern LRESULT DispatchMessageW([In] ref MSG lpmsg);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern LRESULT DefWindowProcW(HWND hWnd, WindowMessage uMsg, WPARAM wParam, LPARAM lParam);

        [DllImport("gdi32.dll")]
        internal static extern IntPtr GetStockObject(StockObject fnObject);

        [Obsolete("Use LoadImage instead")]
        [DllImport("user32.dll")]
        internal static extern HICON LoadIcon(HINSTANCE hInstance, string lpIconName);

        [Obsolete("Use LoadImage instead")]
        [DllImport("user32.dll")]
        internal static extern HCURSOR LoadCursor(HINSTANCE hInstance, string lpCursorName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern HANDLE LoadImage(HINSTANCE hInstance, string lpszName, ImageType uType, int cxDesired, int cyDesired, FuLoad fuLoad);

        [DllImport("user32.dll")]
        internal static extern int GetSystemMetrics(SystemMetric smIndex);



        // SetWindowLongPtr does not exist on x86 platforms (it's a macro that resolves to SetWindowLong).
        // We need to detect if we are on x86 or x64 at runtime and call the correct function
        // (SetWindowLongPtr on x64 or SetWindowLong on x86). Fun!
        internal static IntPtr SetWindowLong(IntPtr handle, GetWindowLongOffsets item, IntPtr newValue)
        {
            // SetWindowPos defines its error condition as an IntPtr.Zero retval and a non-0 GetLastError.
            // We need to SetLastError(0) to ensure we are not detecting on older error condition (from another function).

            IntPtr retval = IntPtr.Zero;
            SetLastError(0);

            if (IntPtr.Size == 4)
                retval = new IntPtr(SetWindowLongWInternal(handle, item, newValue.ToInt32()));
            else
                retval = SetWindowLongPtrWInternal(handle, item, newValue);

            if (retval == IntPtr.Zero)
            {
                int error = Marshal.GetLastWin32Error();
                if (error != 0)
                    throw new Exception(String.Format("Failed to modify window border. Error: {0}", error));
            }

            return retval;
        }

        internal static IntPtr SetWindowLong(IntPtr handle, WindowProcedure newValue)
        {
            return SetWindowLong(handle, GetWindowLongOffsets.WNDPROC, Marshal.GetFunctionPointerForDelegate(newValue));
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongW", CharSet = CharSet.Unicode)]
        static extern LONG SetWindowLongWInternal(HWND hWnd, GetWindowLongOffsets nIndex, LONG dwNewLong);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongPtrW", CharSet = CharSet.Unicode)]
        static extern LONG_PTR SetWindowLongPtrWInternal(HWND hWnd, GetWindowLongOffsets nIndex, LONG_PTR dwNewLong);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongW", CharSet = CharSet.Unicode)]
        static extern LONG SetWindowLongWInternal(HWND hWnd, GetWindowLongOffsets nIndex, [MarshalAs(UnmanagedType.FunctionPtr)]WindowProcedure dwNewLong);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongPtrW", CharSet = CharSet.Unicode)]
        static extern LONG_PTR SetWindowLongPtrWInternal(HWND hWnd, GetWindowLongOffsets nIndex, [MarshalAs(UnmanagedType.FunctionPtr)]WindowProcedure dwNewLong);

        [DllImport("kernel32.dll")]
        internal static extern void SetLastError(DWORD dwErrCode);

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate LRESULT WindowProcedure(HWND handle, WindowMessage message, WPARAM wParam, LPARAM lParam);

        [DllImport("user32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        internal extern static BOOL GetClientRect(HWND windowHandle, out Win32Rectangle clientRectangle);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(
            HWND hWnd,
            HWND hWndInsertAfter,
            int x, int y, int cx, int cy,
            SetWindowPosFlags flags
        );


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MoveWindow(
            HWND hWnd,
            int x, int y, int nWidth, int nHeight,
            bool repaint
        );

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern int ReleaseDC([In] HWND hWnd, [In] IntPtr hDC);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern BOOL SetWindowTextW(HWND hWnd, [MarshalAs(UnmanagedType.LPTStr)] string lpString);

    }
}
