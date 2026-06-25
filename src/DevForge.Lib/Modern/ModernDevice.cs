using System;
using System.Threading;
using DevForge.Lib.API;

namespace DevForge.Lib.Modern
{
    public sealed class ModernDevice : ICommDevice
    {
        private Thread _thread;
        private UsbPort _port;

        public void Start()
        {
            _thread = new Thread(DoLoop) { IsBackground = true, Name = "Loop" };
            _thread.Start();
        }

        private void DoLoop()
        {
            _port = Universals.CreatePort();
            var head = _port.ReadString();
            Console.WriteLine(" " + this.GetType().Name + " => '" + head + "'");
        }

        public void Stop()
        {
            Universals.ClosePort(ref _port);
            _thread.Interrupt();
            _thread.Abort();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}