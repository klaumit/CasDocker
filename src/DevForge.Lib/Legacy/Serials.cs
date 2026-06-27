using System.Linq;
using System.IO.Ports;
using System.Text;

namespace DevForge.Lib.Legacy
{
    internal static class Serials4
    {
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
    }
}