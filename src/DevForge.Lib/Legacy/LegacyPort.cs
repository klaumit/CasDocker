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

        private void Close()
        {
            if (_port != null)
                _port.Close();
            _port = null;
        }

        public void Dispose()
        {
            Close();
        }
    }
}