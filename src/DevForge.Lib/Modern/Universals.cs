using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevForge.Lib.API;
using System.IO.Ports;
using System.Threading;

namespace DevForge.Lib.Modern
{
    internal static class Universals
    {
        internal static UsbPort CreatePort()
        {
            var names = UsbPort.GetPortNames();
            var name = names.Last();
            var port = new UsbPort(name);
            port.Open();
            return port;
        }

        internal static void ClosePort(ref UsbPort port)
        {
            using (port)
                port.Close();
            port = null;
        }
    }
}