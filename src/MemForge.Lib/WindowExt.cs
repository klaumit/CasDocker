using System;
using System.Collections.Generic;
using System.Text;
using Vanara.PInvoke;

namespace MemForge.Lib
{
    public static class WindowExt
    {
        public static List<Tuple<HWND, string>> GetTopLevelWindows(uint procId)
        {
            var windows = new List<Tuple<HWND, string>>();
            VanExt.EnumWindows((hWnd, lParam) =>
            {
                uint windowPid;
                VanExt.GetWindowThreadProcessId(hWnd, out windowPid);
                if (windowPid == procId && VanExt.IsWindowVisible(hWnd))
                {
                    var txt = GetWindowTitle(hWnd);
                    windows.Add(Tuple.Create(hWnd, txt));
                }
                return true;
            }, IntPtr.Zero);
            return windows;
        }

        public static string GetWindowTitle(HWND hWnd)
        {
            var sb = new StringBuilder(1024);
            VanExt.GetWindowText(hWnd, sb, sb.Capacity);
            var txt = sb.ToString().Trim();
            return txt;
        }
    }
}