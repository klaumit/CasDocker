using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PvMake.Lib
{
    public static class Driving
    {
        public static void FindAll()
        {
            var windowH = Vanara.PInvoke.User32.FindWindow(null, "SIM3022");
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