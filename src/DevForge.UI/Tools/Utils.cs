using System;
using DevForge.Lib.Common;
using System.Collections.Generic;
using DevForge.Lib.API;
using System.Windows.Forms;

// ReSharper disable UseCollectionExpression

namespace DevForge.UI
{
    public static class Utils
    {
        public static Form Main;
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

        public static void OnExiting(object sender, FormClosingEventArgs e)
        {
            foreach (var device in devices)
            {
                device.Dispose();
            }
        }

        public static void OnNewDevice(object s, DeviceFoundArgs e)
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