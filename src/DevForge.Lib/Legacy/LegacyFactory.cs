using System.IO.Ports;
using System.Linq;
using DevForge.Lib.API;
using DevForge.Lib.API;

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyFactory : ICommFactory
    {
        public ICommPort Create()
        {
            return CreatePort();
        }

        public static string[] GetPortNames()
        {
            var names = SerialPort.GetPortNames();
            return names;
        }

        public static LegacyPort CreatePort()
        {
            var names = SerialPort.GetPortNames();
            var name = names.Last();
            const int speed = 38400;
            const Parity parity = Parity.None;
            const int dataB = 8;
            const StopBits stopB = StopBits.One;
            var port = new SerialPort(name, speed, parity, dataB, stopB);
            port.Handshake = Handshake.RequestToSend;
            port.NewLine = "\r\n";
            var wrap = new LegacyPort(port);
            wrap.Open();
            return wrap;
        }
    }
}