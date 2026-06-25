using System.Linq;
using System.IO.Ports;
using System.Text;

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

        internal static string ReadString(this SerialPort port, int maxLen = 64)
        {
            if (port == null)
                return null;
            var buffer = new byte[maxLen];
            var bytesRead = port.Read(buffer, 0, buffer.Length);
            if (bytesRead < 1)
                return null;
            var text = Encoding.ASCII.GetString(buffer);
            var res = text.Substring(0, bytesRead).Trim();
            return res;
        }

        internal static void ClosePort(ref SerialPort port)
        {
            if (port == null)
                return;
            using (port)
                port.Close();
            port = null;
        }
    }
}