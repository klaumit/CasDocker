using System;
using System.Linq;
using System.Text;
using System.Threading;
using DevForge.Lib.API;
using E = DevForge.Lib.Modern.EnumDevNative;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
    public static class ModernDevices
    {
        public static string[] GetPortNames(int wait = 250)
        {
            string devicePath;
            while (true)
            {
                var path = new byte[260];
                E.PVEnumUsbA(0, path, path.Length);
                devicePath = Encoding.ASCII.GetString(path).TrimEnd('\0');
                if (!string.IsNullOrWhiteSpace(devicePath))
                    break;
                Thread.Sleep(wait);
            }
            return new[] { devicePath };
        }

        public static ModernPort CreatePort()
        {
            var names = GetPortNames();
            var name = names.Last();
            var port = new ModernPort(name);
            port.Open();
            return port;
        }
    }
}