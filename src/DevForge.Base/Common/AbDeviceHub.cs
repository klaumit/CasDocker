using System;
using DevForge.Lib.API;
using DevForge.Lib.Messages;
using DevForge.Lib.Messages.Impl;

// ReSharper disable UseNullPropagation
// ReSharper disable ClassNeverInstantiated.Global

namespace DevForge.Lib.Common
{
    public abstract class AbDeviceHub
    {
        public event EventHandler<DeviceFoundArgs> NewDevice;

        private void OnNewDevice(ICommDevice dev, Hello hel)
        {
            if (NewDevice == null) return;
            var a = new DeviceFoundArgs
            {
                Stamp = DateTime.Now, Device = dev, Hello = hel
            };
            NewDevice.Invoke(this, a);
        }

        protected void DoOnePort(BaseFactory factory)
        {
            var prefix = factory.Prefix;
            var port = factory.Create();
            var dev = new PocketDevice(prefix, port);
            var raw = dev.Receive();
            if (raw == null)
                return;
            var h = ToHello(raw);
            OnNewDevice(dev, h);
            dev.Start();
        }

        private static Hello ToHello(Message message)
        {
            var hello = message as Hello;
            return hello;
        }
    }
}