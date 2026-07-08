using System;
using System.IO;
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
        private MemoryStream _memory;

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

        private MemoryStream GetStream()
        {
            if (_memory != null)
                return _memory;

            if (_usbHandle == null)
                return null;

            uint got;
            var handle = _usbHandle.Value;
            const int maxLen = 512;
            var array = new byte[maxLen];
            if (E.PVReadUsb(handle, array, (uint)array.Length, out got) && got >= 1)
                _memory = new MemoryStream(array, 0, (int)got);

            return _memory;
        }

        public byte[] ReadBytes(int count)
        {
            var mem = GetStream();
            if (mem == null)
                return null;
            var rest = mem.Length - mem.Position;
            if (rest < count)
            {
                _memory = null;
                mem = GetStream();
            }
            if (mem == null)
                return null;
            var buffer = new byte[count];
            var bytesRead = mem.Read(buffer, 0, count);
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

        public bool IsOpen()
        {
            return _usbHandle != null;
        }
    }
}