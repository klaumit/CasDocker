using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Vanara.PInvoke;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using W = Vanara.PInvoke.User32.WindowMessage;
using V = Vanara.PInvoke.User32.VK;
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
            var nr = 0;
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
        
        public static MenuItemRef GetMenuItemRef(uint i, string name, HMENU menu)
        {
            var mir = new MenuItemRef { ItemPos = i, Name = name };
            var itemId = User32.GetMenuItemID(menu, (int)i);
            if ((int)itemId == -1)
                mir.SubMenu = User32.GetSubMenu(menu, (int)i);
            else
                mir.ItemId = itemId;
            return mir;            
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
                    return GetMenuItemRef(i, name, menu);
                }
            }
            return null;
        }

        public static readonly Lazy<InputSimulator> Inputer = new Lazy<InputSimulator>();

        private static IntPtr MakeLParam(V vk, bool keyUp, bool altDown)
        {
            var scan = User32.MapVirtualKey((uint)vk, User32.MAPVK.MAPVK_VK_TO_VSC);
            const int repeatCount = 1;
            var lParam = repeatCount
                         | (scan << 16)
                         | ((altDown ? 1u : 0u) << 29)
                         | ((keyUp ? 1u : 0u) << 31);
            return (IntPtr)(int)lParam;
        }

        private static void PressOneKey(HWND hWnd, V key, int delay = 900)
        {
            User32.PostMessage(hWnd, (uint)W.WM_KEYDOWN, (IntPtr)key,
                MakeLParam(key, keyUp: false, altDown: false));
            Thread.Sleep(delay);
                
            User32.PostMessage(hWnd, (uint)W.WM_KEYUP, (IntPtr)key,
                MakeLParam(key, keyUp: true, altDown: false));
            Thread.Sleep(delay);
        }
        
        private static void PressSysKey(HWND hWnd, V sys, V key, int delay = 900)
        {
            User32.PostMessage(hWnd, (uint)W.WM_SYSKEYDOWN, (IntPtr)sys,
                MakeLParam(sys, keyUp: false, altDown: false));
            Thread.Sleep(delay);

            User32.PostMessage(hWnd, (uint)W.WM_SYSKEYDOWN, (IntPtr)key,
                MakeLParam(key, keyUp: false, altDown: true));
            Thread.Sleep(delay);

            User32.PostMessage(hWnd, (uint)W.WM_SYSKEYUP, (IntPtr)key,
                MakeLParam(key, keyUp: true, altDown: true));
            Thread.Sleep(delay);

            User32.PostMessage(hWnd, (uint)W.WM_SYSKEYUP, (IntPtr)sys,
                MakeLParam(sys, keyUp: true, altDown: false));
            Thread.Sleep(delay);
        }

        public static void OpenInIntel(string cpjFile)
        {
            var windowH = WaitForWindow("SIM3022");

            var menuBar = User32.GetMenu(windowH.Value);
            var fileMenu = Driving.FindMenuItem(menuBar, "&File");
            if (fileMenu == null)
            {
                // On Wine
                Driving.PressSysKey(windowH.Value, V.VK_MENU, V.VK_F);
                Driving.PressOneKey(windowH.Value, V.VK_O);
            }
            else
            {
                // On WinXP
                var openProj = Driving.FindMenuItem(fileMenu.Value.SubMenu.Value, "&Open Project");
                User32.PostMessage(windowH.Value, (uint)W.WM_COMMAND, (IntPtr)openProj.Value.ItemId.Value);
            }

            var loadDlg = WaitForWindow("Select Loading Project File");
            var editFld = User32.FindWindowEx(loadDlg.Value, default(HWND), "Edit", "");
            User32.SendMessage(editFld, W.WM_SETTEXT, 0, cpjFile);

            var openBtn = User32.FindWindowEx(loadDlg.Value, default(HWND), "Button", "&Open");
            User32.SendMessage(openBtn, W.WM_BM_CLICK);
        }

        public static void OpenInHitachi(string dlpFile)
        {
            var windowH = WaitForWindow("New project (Default) - CASIO SimSH Simulator");

            var menuBar = User32.GetMenu(windowH.Value);
            var fileMenu = Driving.FindMenuItem(menuBar, "&Project");
            if (fileMenu == null)
            {
            	// On Wine, not found by text somehow?!
            	return;
            }
            var openProj = Driving.FindMenuItem(fileMenu.Value.SubMenu.Value, "&Open...");
            User32.PostMessage(windowH.Value, (uint)W.WM_COMMAND, (IntPtr)openProj.Value.ItemId.Value);

            var loadDlg = WaitForWindow("Open project");
            var combExFld = User32.FindWindowEx(loadDlg.Value, default(HWND), "ComboBoxEx32", "");
            var combFld = User32.FindWindowEx(combExFld, default(HWND), "ComboBox", "");
            var editFld = User32.FindWindowEx(combFld, default(HWND), "Edit", "");
            User32.SendMessage(editFld, W.WM_SETTEXT, 0, dlpFile);

            var openBtn = User32.FindWindowEx(loadDlg.Value, default(HWND), "Button", "&Open");
            User32.SendMessage(openBtn, W.WM_BM_CLICK);
        }
    }
}