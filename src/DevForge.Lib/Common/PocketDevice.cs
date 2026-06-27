using System;
using System.Threading;
using DevForge.Lib.API;

namespace DevForge.Lib.Modern
{
    public sealed class PocketDevice : ICommDevice
    {
        private readonly ICommFactory _factory;
        private ICommPort _port;
        private Thread _thread;

        public PocketDevice(ICommFactory factory)
        {
            _factory = factory;
        }

        public void Start()
        {
            _thread = new Thread(DoLoop) { IsBackground = true };
            _thread.Start();
        }

        private void DoLoop()
        {
            _port = _factory.Create();
            Console.WriteLine(" " + this.GetType().Name + " ...");
            var head = _port.ReadString();
            Console.WriteLine(" " + this.GetType().Name + " => '" + head + "'");
        }

        public void Stop()
        {
            CommExt.ClosePort(ref _port);
            _thread.Interrupt();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}