using System;
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

        public byte[] ReadBytes(int count)
        {
            if (_usbHandle == null)
                return null;
            var handle = _usbHandle.Value;
            var buffer = new byte[count];
            uint bytesRead;
            if (!E.PVReadUsb(handle, buffer, (uint)buffer.Length, out bytesRead))
                return null;
            if (bytesRead < 1)
                return null;
            if (buffer.Length != bytesRead)
                Array.Resize(ref buffer, (int)bytesRead);
            return buffer;
        }

        public bool WriteBytes(byte[] buffer)
        {
            if (_usbHandle == null)
                return false;
            var handle = _usbHandle.Value;
            uint bytesWritten;
            if (!E.PVWriteUsb(handle, buffer, (uint)buffer.Length, out bytesWritten))
                return false;
            if (bytesWritten < 1)
                return false;
            return true;
        }
    }
}