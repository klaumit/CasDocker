using System;
using System.Threading.Tasks;
using DevForge.Lib.API;
using DevForge.Lib.Legacy;
using DevForge.Lib.Modern;
using DevForge.Lib.Messages;
using DevForge.Lib.Messages.Impl;

// ReSharper disable UseNullPropagation
// ReSharper disable ClassNeverInstantiated.Global

namespace DevForge.Lib.Common
{
    public sealed class GotMessageArgs : EventArgs
    {
        public DateTime Stamp { get; set; }
        public Message Message { get; set; }
    }

    public sealed class DeviceFoundArgs : EventArgs
    {
        public DateTime Stamp { get; set; }
        public ICommDevice Device { get; set; }
        public Hello Hello { get; set; }
    }

    public sealed class DeviceHub
    {
        public event EventHandler<DeviceFoundArgs> NewDevice;

        private void OnNewDevice(ICommDevice dev, Hello hel)
        {
            if (NewDevice == null) return;
            var a = new DeviceFoundArgs { 
                Stamp = DateTime.Now, Device = dev, Hello = hel
            };
            NewDevice.Invoke(this, a);
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
            var h = ToHello(dev.Receive());
            OnNewDevice(dev, h);
            dev.Start();
        }

        private void DoModern()
        {
            var factory = new ModernFactory();
            var prefix = factory.Prefix;
            var port = factory.Create();
            var dev = new PocketDevice(prefix, port);
            var h = ToHello(dev.Receive());
            OnNewDevice(dev, h);
            dev.Start();
        }

        private Hello ToHello(Message message)
        {
            var hello = message as Hello;
            return hello;
        }
    }
}