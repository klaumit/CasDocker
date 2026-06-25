using System.Linq;
using System.IO.Ports;

namespace DevForge.Lib.Legacy
{
    internal static class Serials
    {
        internal static SerialPort CreatePort()
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
            return port;
        }

        internal static void ClosePort(ref SerialPort port)
        {
            using (port)
                port.Close();
            port = null;
        }
    }
}