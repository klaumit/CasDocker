using System;
using System.Threading.Tasks;
using DevForge.Lib.API;
using DevForge.Lib.Legacy;
using DevForge.Lib.Modern;

// ReSharper disable UseNullPropagation
// ReSharper disable ClassNeverInstantiated.Global

namespace DevForge.Lib.Common
{
    public sealed class DeviceFoundArgs : EventArgs
    {
        public DateTime Stamp { get; set; }
        public ICommDevice Device { get; set; }
    }

    public sealed class DeviceHub
    {
        public event EventHandler<DeviceFoundArgs> NewDevice;

        private void OnNewDevice(DeviceFoundArgs e)
        {
            if (NewDevice == null) return;
            NewDevice.Invoke(this, e);
        }

        private void OnNewDevice(ICommDevice dev)
        {
            OnNewDevice(new DeviceFoundArgs { Stamp = DateTime.Now, Device = dev });
        }

        public void StartLegacy()
        {
            Task.Factory.StartNew(DoLegacy);
        }

        public void StartModern()
        {
            Task.Factory.StartNew(DoModern);
        }

        private void DoLegacy()
        {
            var factory = new LegacyFactory();
            var prefix = factory.Prefix;
            var port = factory.Create();
            var dev = new PocketDevice(prefix, port);
            OnNewDevice(dev);
            dev.Start();
        }

        private void DoModern()
        {
            var factory = new ModernFactory();
            var prefix = factory.Prefix;
            var port = factory.Create();
            var dev = new PocketDevice(prefix, port);
            OnNewDevice(dev);
            dev.Start();
        }
    }
}