using System;
using System.Text;
using System.Threading;
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
    }
}