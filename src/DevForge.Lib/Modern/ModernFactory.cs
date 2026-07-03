using System;
using System.Linq;
using DevForge.Lib.API;
using System.Text;
using System.Threading;
using E = DevForge.Lib.Modern.Internals.EnumDevNative;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
    public sealed class ModernFactory : ICommFactory
    {
        public ICommPort Create()
        {
            try
            {
                return CreatePort();
            }
            catch (Exception)
            {
                return new ModernPort("<none>");
            }
        }

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