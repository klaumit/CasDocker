using System;
using System.Text;
using System.Threading;
using E = DevForge.Lib.Modern.EnumDevNative;

// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
    public sealed class UsbPort : IDisposable
    {
        private readonly string _path;
        private IntPtr? _usbHandle;

        public UsbPort(string path)
        {
            _path = path;
        }

        public static string[] GetPortNames(int wait = 250)
        {
            string devicePath;
            do
            {
                var path = new byte[260];
                var idx = E.PVEnumUsbA(0, path, path.Length);
                devicePath = Encoding.ASCII.GetString(path).TrimEnd('\0');

                Thread.Sleep(wait);
            } while (string.IsNullOrWhiteSpace(devicePath));

            return new[] { devicePath };
        }

        public void Open()
        {
            var handle = E.CreateFile(_path, E.GENERIC_READ_WRITE,
                0, IntPtr.Zero, E.OPEN_EXISTING,
                E.FILE_FLAG_OVERLAPPED, IntPtr.Zero);

            if (handle == IntPtr.Zero)
                return;

            _usbHandle = handle;
        }

        private string ReadString(int maxLen = 64)
        {
            if (_usbHandle == null)
                return null;
            var handle = _usbHandle.Value;
            byte[] buffer = new byte[maxLen];
            uint bytesRead;
            if (!E.PVReadUsb(handle, buffer, (uint)buffer.Length, out bytesRead))
                return null;
            var text = Encoding.ASCII.GetString(buffer);
            var res = text.Substring(0, (int)bytesRead);
            return res;
        }

        public void Close()
        {
            if (_usbHandle != null)
                E.CloseHandle(_usbHandle.Value);
            _usbHandle = null;
        }

        public void Dispose()
        {
            Close();
        }
    }
}