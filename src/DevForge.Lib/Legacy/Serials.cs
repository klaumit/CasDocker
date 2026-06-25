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
            port.NewLine = "\r\n";
            port.Open();
            return port;
        }

        internal static string ReadString(this SerialPort port, int maxLen = 64)
        {
            if (port == null)
                return null;
            var buffer = new byte[maxLen];
            var bytesRead = port.Read(buffer, 0, buffer.Length);
            int rest;
            while ((rest = port.BytesToRead) >= 1)
                bytesRead += port.Read(buffer, bytesRead, buffer.Length - bytesRead);
            if (bytesRead < 1)
                return null;
            var text = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            var res = text.Trim();
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