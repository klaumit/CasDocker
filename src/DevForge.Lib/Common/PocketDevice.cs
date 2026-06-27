using System;
using System.Linq;
using System.Threading;
using DevForge.Lib.API;

// ReSharper disable UseNameOfInsteadOfTypeOf

namespace DevForge.Lib.Common
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

        private string Name
        {
            get
            {
                var prefix = _factory.GetType().Name.Split('F').First();
                var last = GetType().Name.Split('t').Last();
                var name = prefix + last;
                return name;
            }
        }

        public void Start()
        {
            _thread = new Thread(DoLoop) { IsBackground = true, Name = Name };
            _thread.Start();
        }

        private void DoLoop()
        {
            _port = _factory.Create();
            Console.WriteLine(" [{0}] created...", Name);
            var head = _port.ReadString();
            Console.WriteLine(" [{0}] => '{1}'", Name, head);
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