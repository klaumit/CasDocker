using System;
using System.Runtime.InteropServices;
using System.Text;
using Vanara.PInvoke;
using L = Vanara.PInvoke.Lib;

namespace MemForge.Lib
{
    public static class VanExt
    {
        [DllImport(L.User32, SetLastError = true, CharSet = CharSet.Auto)]
        [PInvokeData("winuser.h", MSDNShortId = "getwindowtext")]
        public static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport(L.User32, SetLastError = false, ExactSpelling = true)]
        [PInvokeData("winuser.h", MSDNShortId = "getwindowthreadprocessid")]
        public static extern uint GetWindowThreadProcessId(HWND hWnd, out uint lpdwProcessId);

        [DllImport(L.User32, SetLastError = true, ExactSpelling = true)]
        [PInvokeData("winuser.h", MSDNShortId = "enumwindows")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        
        [PInvokeData("Winuser.h", MSDNShortId = "ms633493")]
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public delegate bool EnumWindowsProc([In] HWND hwnd, [In] IntPtr lParam);

        [DllImport(L.User32, SetLastError = false, ExactSpelling = true)]
        [PInvokeData("winuser.h", MSDNShortId = "iswindowvisible")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(HWND hWnd);
    }
}