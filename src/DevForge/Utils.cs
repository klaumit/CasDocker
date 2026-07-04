using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Windows.Forms;
using DevForge.Resources;
using System.Drawing;
using DevForge.Lib.Legacy;
using DevForge.Lib.API;
using DevForge.Lib.Modern;
using DevForge.Lib.Common;
using DevForge.Lib.Messages.Impl;
using System.Threading;

namespace DevForge
{
    internal static class Utils
    {
        public static Lazy<ICommDevice> LegacyDev = new Lazy<ICommDevice>(DoLegacy);
        public static Lazy<ICommDevice> ModernDev = new Lazy<ICommDevice>(DoModern);

        private static ICommDevice DoLegacy()
        {
            ICommDevice dev = new PocketDevice(new LegacyFactory());
            dev.Start();
            return dev;
        }

        private static ICommDevice DoModern()
        {
            ICommDevice dev = new PocketDevice(new ModernFactory());
            dev.Start();
            return dev;
        }
           
        public static void X2()
        {           
            // var quit = new Quit("Hello, C# !");
            // dev.Send(quit);
        }

        public static void X1()
        {
            // var quit = new Quit("Hello, C# !");
            // dev.Send(quit);
        }
    }
}