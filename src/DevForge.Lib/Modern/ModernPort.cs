using System;
using System.Text;
using System.Threading;
using DevForge.Lib.API;
using E = DevForge.Lib.Modern.EnumDevNative;
using K = DevForge.Lib.Modern.KernelNative;

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
    }
}