using System;
using System.Threading;
using DevForge.Lib.API;
using System.IO.Ports;

namespace DevForge.Lib.Legacy
{
    public sealed class LegacyDevice : ICommDevice
    {
        private Thread _thread;
        private SerialPort _port;

        public void Start()
        {
            _thread = new Thread(DoLoop) { IsBackground = true, Name = "Loop" };
            _thread.Start();
        }

        private void DoLoop()
        {
            _port = Serials.CreatePort();
            Console.WriteLine(" " + this.GetType().Name + " ...");
            var head = _port.ReadString();
            Console.WriteLine(" " + this.GetType().Name + " => '" + head + "'");
        }

        public void Stop()
        {
            Serials.ClosePort(ref _port);
            _thread.Interrupt();
            _thread.Abort();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}