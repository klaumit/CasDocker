using System.Linq;

namespace DevForge.Lib.Modern
{
    internal static class Universals
    {
        

        internal static void ClosePort(ref UsbPort port)
        {
            if (port == null)
                return;
            using (port)
                port.Close();
            port = null;
        }
    }
}