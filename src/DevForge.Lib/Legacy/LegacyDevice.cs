using DevForge.Lib.API;
using System.IO.Ports;

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyDevice : ICommDevice
    {
        private SerialPort _port;

        public void Start()
        {
            _port = Serials.CreatePort();
        }

        public void Stop()
        {
            Serials.ClosePort(ref _port);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}