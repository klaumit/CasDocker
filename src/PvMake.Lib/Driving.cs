using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Vanara.PInvoke;

namespace PvMake.Lib
{
    public static class Driving
    {
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

        public static void FindAll()
        {
            var windowH = WaitForWindow("SIM3022");
            Console.WriteLine("Window = " + windowH);

            var iss = new WindowsInput.InputSimulator();
            iss.Keyboard.Sleep(TimeSpan.FromSeconds(5));
            iss.Keyboard.TextEntry("Hello you witch!");

            foreach (var w in ManagedWinapi.Windows.SystemWindow.AllToplevelWindows)
            {
                Console.WriteLine(" " + w.ClassName);
            }

            Console.ReadLine();
        }
    }
}