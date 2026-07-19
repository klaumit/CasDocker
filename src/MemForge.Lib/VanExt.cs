using System;
using System.Collections.Generic;
using System.Drawing;
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
    }
}