using System;
using System.IO;
using DevForge.Lib.Common;
using System.Collections.Generic;
using DevForge.Lib.API;
using System.Windows.Forms;

namespace DevForge
{
    internal static class Utils
    {
        public static void X2()
        {
            // var quit = new Quit("Hello, C# !");
            // dev.Send(quit);
        }

        internal static MainForm Main;
        private static readonly List<ICommDevice> devices = new List<ICommDevice>();

        internal static void OnExiting(object sender, FormClosingEventArgs e)
        {
            foreach (var device in devices)
            {
                device.Dispose();
            }
        }

        internal static void OnNewDevice(object s, DeviceFoundArgs e)
        {
            var dev = e.Device;
            devices.Add(dev);

            var form = new DeviceForm(dev);
            form.Show(Main);
        }
    }
}