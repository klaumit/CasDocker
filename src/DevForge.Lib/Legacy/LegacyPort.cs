using System.IO.Ports;
using System.Text;
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

        public string ReadString(int maxLen = 64)
        {
            if (_port == null)
                return null;
            var buffer = new byte[maxLen];
            var bytesRead = _port.Read(buffer, 0, buffer.Length);
            int rest;
            while ((rest = _port.BytesToRead) >= 1)
                bytesRead += _port.Read(buffer, bytesRead, rest);
            if (bytesRead < 1)
                return null;
            var text = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            var res = text.Trim();
            return res;
        }
    }
}