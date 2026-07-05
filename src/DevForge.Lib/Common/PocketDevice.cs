using System;
using System.Linq;
using System.Threading;
using DevForge.Lib.API;
using DevForge.Lib.Messages;

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

        public string Name
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

        public event EventHandler<GotMessageArgs> NewMessage;

        private void OnNewMessage(Message msg)
        {
            if (NewMessage == null) return;
            var a = new GotMessageArgs { Stamp = DateTime.Now, Message = msg };
            NewMessage.Invoke(this, a);
        }

        private void DoLoop()
        {
            while (_port.IsOpen())
            {
                var message = _port.ReadMessage();
                if (message == null)
                    continue;
                OnNewMessage(message);
            }
        }

        public Message Receive()
        {
            var message = _port.ReadMessage();
            return message;
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