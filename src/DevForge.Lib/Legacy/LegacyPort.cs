using System;
using System.IO.Ports;
using DevForge.Lib.API;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyPort : ICommPort
    {
        private SerialPort _port;

        public LegacyPort(SerialPort port)
        {
            _port = port;
        }

        public void Open()
        {
            _port.Open();
        }

        public void Close()
        {
            if (_port != null)
            {
                _port.Close();
                _port.Dispose();
            }
            _port = null;
        }

        public void Dispose()
        {
            Close();
        }

        public byte[] ReadBytes(int count)
        {
            if (_port == null)
                return null;
            var buffer = new byte[count];
            var bytesRead = _port.Read(buffer, 0, buffer.Length);
            int rest;
            while ((bytesRead < buffer.Length) && (rest = _port.BytesToRead) >= 1)
                bytesRead += _port.Read(buffer, bytesRead, rest);
            if (bytesRead < 1)
                return null;
            if (buffer.Length != bytesRead)
                Array.Resize(ref buffer, bytesRead);
            return buffer;
        }
    }
}