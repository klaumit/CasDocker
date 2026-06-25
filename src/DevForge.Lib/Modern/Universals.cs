using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevForge.Lib.API;
using System.IO.Ports;
using System.Threading;

namespace DevForge.Lib.Modern
{
    internal static class Universals
    {
        public sealed class Uni
        {
        }

        internal static void Fuck(int wait = 250)
        {
            string devicePath;
            do
            {
                var path = new byte[260];
                var idx = EnumDevNative.PVEnumUsbA(0, path, path.Length);
                devicePath = Encoding.ASCII.GetString(path).TrimEnd('\0');

                Thread.Sleep(wait);
            } while (string.IsNullOrWhiteSpace(devicePath));
        }
    }
}