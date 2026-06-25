using System.Linq;
using DevForge.Lib.API;
using System.IO.Ports;

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyCommDevice : ICommDevice
    {
        private SerialPort _port;

        public LegacyCommDevice()
        {
            var names = SerialPort.GetPortNames();
            var name = names.Last();
            var speed = 38400;
            var parity = Parity.None;
            var dataB = 8;
            var stopB = StopBits.One;
            var port = new SerialPort(name, speed, parity, dataB, stopB);
            port.Handshake = Handshake.RequestToSend;
            port.Open();
            _port = port;
        }
    }
}