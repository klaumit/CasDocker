using System;
using System.IO;
using DevForge.Lib.Common;

namespace DevForge
{
    internal static class Utils
    {
        public static void X2()
        {
            // var quit = new Quit("Hello, C# !");
            // dev.Send(quit);
        }

        internal static void OnNewDevice(object s, DeviceFoundArgs e)
        {
            Console.WriteLine(" " + s + " = " + e);
        }
    }
}