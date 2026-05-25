using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Vanara.PInvoke;
using System.Diagnostics;
using System.IO;
using W = Vanara.PInvoke.User32.WindowMessage;
using WindowsInput;

namespace PvMake.Lib
{
    public static class Driving
    {
        public static void KillAll(string name)
        {
            var pName = Path.GetFileNameWithoutExtension(name);
            var processes = Process.GetProcessesByName(pName);
            foreach (var proc in processes)
            {
                proc.Kill();
                proc.WaitForExit(5 * 1000);
            }
        }

        public static HWND? WaitForWindow(string name, int delay = 100, int count = 30)
        {
            int nr = 0;
            HWND handle;
            while ((handle = User32.FindWindow(null, name)).IsNull && nr <= count)
            {
                Thread.Sleep(delay);
                nr++;
            }
            return handle.IsNull ? default(HWND?) : handle;
        }

        public struct MenuItemRef
        {
            public uint ItemPos;
            public string Name;
            public uint? ItemId;
            public HMENU? SubMenu;
        }

        public static MenuItemRef? FindMenuItem(HMENU menu, string name)
        {
            var count = User32.GetMenuItemCount(menu);
            for (uint i = 0; i < count; i++)
            {
                var sb = new StringBuilder(256);
                User32.GetMenuString(menu, i, sb, sb.Capacity, User32.MenuFlags.MF_BYPOSITION);
                var text = sb.ToString();
                if (text.Equals(name))
                {
                    var mir = new MenuItemRef { ItemPos = i, Name = name };
                    var itemId = User32.GetMenuItemID(menu, (int)i);
                    if ((int)itemId == -1)
                        mir.SubMenu = User32.GetSubMenu(menu, (int)i);
                    else
                        mir.ItemId = itemId;
                    return mir;
                }
            }
            return null;
        }

        public static readonly Lazy<InputSimulator> Inputer = new Lazy<InputSimulator>();

        public static void OpenInIntel(string cpjFile)
        {
            var windowH = WaitForWindow("SIM3022");

            var menuBar = User32.GetMenu(windowH.Value);
            var fileMenu = Driving.FindMenuItem(menuBar, "&File");
            var openProj = Driving.FindMenuItem(fileMenu.Value.SubMenu.Value, "&Open Project");
            User32.PostMessage(windowH.Value, (uint)W.WM_COMMAND, (IntPtr)openProj.Value.ItemId.Value);

            var loadDlg = WaitForWindow("Select Loading Project File");
            var editFld = User32.FindWindowEx(loadDlg.Value, default(HWND), "Edit", "");
            User32.SendMessage(editFld, W.WM_SETTEXT, 0, cpjFile);

            var openBtn = User32.FindWindowEx(loadDlg.Value, default(HWND), "Button", "&Open");
            User32.SendMessage(openBtn, W.WM_BM_CLICK);
        }
    }
}