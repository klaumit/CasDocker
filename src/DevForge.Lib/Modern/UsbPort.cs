using System;
using System.Text;
using System.Threading;
using E = DevForge.Lib.Modern.EnumDevNative;

// ReSharper disable UseCollectionExpression
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

        public void Open()
        {
            var handle = E.CreateFile(_path, E.GENERIC_READ_WRITE,
                0, IntPtr.Zero, E.OPEN_EXISTING,
                E.FILE_FLAG_OVERLAPPED, IntPtr.Zero);

            if (handle == IntPtr.Zero)
                return;

            _usbHandle = handle;
        }

        internal string ReadString(int maxLen = 64)
        {
            if (_usbHandle == null)
                return null;
            var handle = _usbHandle.Value;
            var buffer = new byte[maxLen];
            uint bytesRead;
            if (!E.PVReadUsb(handle, buffer, (uint)buffer.Length, out bytesRead))
                return null;
            var text = Encoding.ASCII.GetString(buffer, 0, (int)bytesRead);
            var res = text.Trim();
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