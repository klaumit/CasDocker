using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using DevForge.Lib.API;
using DevForge.Lib.Ponder;
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
        private IEnumerator<byte> _sink;

        public ModernPort(string devicePath)
        {
            _devicePath = devicePath;
            WaitMs = 25;
            MaxLen = 256;
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
            if (_sink != null)
            {
                _sink.Dispose();
            }
            _sink = null;
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

        public int WaitMs { get; set; }
        public int MaxLen { get; set; }

        private IEnumerable<byte> ReadOneByte()
        {
            while (IsOpen())
            {
                var handle = _usbHandle.Value;
                var array = new byte[MaxLen];
                uint got;
                if (E.PVReadUsb(handle, array, (uint)array.Length, out got))
                    for (var i = 0; i < got; i++)
                        yield return array[i];
                Thread.Sleep(WaitMs);
            }
        }

        private bool MoveNext()
        {
            if (_sink != null)
            {
                if (_sink.MoveNext())
                    return true;
                _sink.Dispose();
                _sink = null;
            }
            if (_sink == null)
            {
                var iter = ReadOneByte();
                _sink = iter.GetEnumerator();
            }
            return _sink.MoveNext();
        }

        public byte[] ReadBytes(int count)
        {
            var array = new byte[count];
            for (var i = 0; i < count; i++)
            {
                if (MoveNext())
                    array[i] = _sink.Current;
            }
            return array;
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