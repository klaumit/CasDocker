using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vanara.PInvoke;

namespace MemForge.Lib
{
    public static class WindowExt
    {
        public static List<Tuple<HWND, string>> GetTopLevelWindows(uint procId)
        {
            var windows = new List<Tuple<HWND, string>>();
            User32.EnumWindows((hWnd, lParam) =>
            {
                uint windowPid;
                User32.GetWindowThreadProcessId(hWnd, out windowPid);
                if (windowPid == procId && User32.IsWindowVisible(hWnd))
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