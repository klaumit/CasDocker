using System;
using DevForge.Lib.Common;
using System.Collections.Generic;
using System.Linq;
using DevForge.Lib.API;
using System.Windows.Forms;

// ReSharper disable UseCollectionExpression

namespace DevForge
{
    internal static class Utils
    {
        internal static MainForm Main;
        private static readonly List<ICommDevice> devices = new List<ICommDevice>();

        internal static void InvokeGui(Action action)
        {
            if (Main.InvokeRequired)
            {
                Main.Invoke(action);
                return;
            }
            action();
        }

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

            InvokeGui(() =>
            {
                var form = new DeviceForm(e);
                form.Show(Main);
            });
        }
    }
}