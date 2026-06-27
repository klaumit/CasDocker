using System.IO.Ports;
using DevForge.Lib.API;

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyPort : ICommPort
    {
        private readonly SerialPort _port;

        public LegacyPort(SerialPort port)
        {
            _port = port;
        }

        public void Open()
        {
            throw new System.NotImplementedException();
        }
    }
}