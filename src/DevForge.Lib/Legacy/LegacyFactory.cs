using System.IO.Ports;
using System.Linq;
using System.Threading;
using DevForge.Lib.API;
using DevForge.Lib.Common;

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyFactory : BaseFactory 
    {
        public override ICommPort Create()
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
            string name = null;
            while (Thread.CurrentThread.IsAlive)
            {
                var names = GetPortNames().OrderBy(p => p);
                name = names.LastOrDefault();
                if (!string.IsNullOrWhiteSpace(name))
                    break;
                Thread.Sleep(1000);
            }
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