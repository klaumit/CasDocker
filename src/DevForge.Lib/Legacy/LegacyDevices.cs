using System.IO.Ports;

namespace DevForge.Lib.Legacy
{
    public static class LegacyDevices
    {
        public static string[] GetPortNames()
        {
            var names = SerialPort.GetPortNames();
            return names;
        }
    }
}