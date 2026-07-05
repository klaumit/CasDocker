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

        public static string GetDateStr(this DateTime date)
        {
            var dts = date.ToString("u");
            return dts.Split(new[] { ' ' }, 2).First();
        }

        public static string GetTimeStr(this DateTime date)
        {
            var dts = date.ToString("u").TrimEnd('Z');
            return dts.Split(new[] { ' ' }, 2).Last();
        }

        public static string GetEnumStr<T>(this T val)
        {
            var txt = (val + "").TrimStart('_');
            return txt;
        }

        public static string GetVerStr(this Version val)
        {
            var txt = (val + "");
            return txt;
        }
    }
}