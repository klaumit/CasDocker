using System;
using System.Linq;
using System.Threading;
using DevForge.Lib.API;
using DevForge.Lib.Messages;
using DevForge.Lib.Messages.Impl;

// ReSharper disable ReplaceWithFieldKeyword
// ReSharper disable UseNameOfInsteadOfTypeOf

namespace DevForge.Lib.Common
{
    public sealed class PocketDevice : ICommDevice
    {
        private readonly string _prefix;
        private ICommPort _port;
        private Thread _thread;

        public PocketDevice(string prefix, ICommPort port)
        {
            _prefix = prefix;
            _port = port;
        }

        private string Name
        {
            get
            {
                var last = GetType().Name.Split('t').Last();
                var name = _prefix + last;
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
            Console.WriteLine(" [{0}] created...", Name);
            var head = _port.ReadMessage();
            var hello = head as Hello;
            var helloT = hello != null ? hello.Text : head.ToString();
            Console.WriteLine(" [{0}] => '{1}'", Name, helloT);
        }

        public void Send(Message msg)
        {
            _port.WriteMessage(msg);
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