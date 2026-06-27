using System.IO.Ports;
using DevForge.Lib.API;

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
    }
}