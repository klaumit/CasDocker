using System.Linq;
using DevForge.Lib.API;
using System.IO.Ports;

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyDevice : ICommDevice
    {
        private SerialPort _port;

        public LegacyDevice()
        {
            var names = SerialPort.GetPortNames();
            var name = names.Last();
            const int speed = 38400;
            const Parity parity = Parity.None;
            const int dataB = 8;
            const StopBits stopB = StopBits.One;
            var port = new SerialPort(name, speed, parity, dataB, stopB);
            port.Handshake = Handshake.RequestToSend;
            port.Open();
            _port = port;
        }
    }
}