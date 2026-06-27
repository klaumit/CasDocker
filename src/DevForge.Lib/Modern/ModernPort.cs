using System;
using System.Text;
using DevForge.Lib.API;
using E = DevForge.Lib.Modern.Internals.EnumDevNative;
using K = DevForge.Lib.Modern.Internals.KernelNative;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
    public sealed class ModernPort : ICommPort
    {
        private readonly string _devicePath;
        private IntPtr? _usbHandle;

        public ModernPort(string devicePath)
        {
            _devicePath = devicePath;
        }

        public void Open()
        {
            var handle = K.CreateFile(_devicePath, K.GENERIC_READ_WRITE,
                0, IntPtr.Zero, K.OPEN_EXISTING,
                K.FILE_FLAG_OVERLAPPED, IntPtr.Zero);
            if (handle == IntPtr.Zero)
                return;
            _usbHandle = handle;
        }

        public void Close()
        {
            if (_usbHandle != null)
            {
                K.CloseHandle(_usbHandle.Value);
            }
            _usbHandle = null;
        }

        public void Dispose()
        {
            Close();
        }

        public string ReadString(int maxLen = 64)
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
    }
}