using System;
using DevForge.Lib.Common;
using DevForge.Lib.Messages;

// ReSharper disable UnusedMemberInSuper.Global

namespace DevForge.Lib.API
{
	public interface ICommDevice : IDisposable
    {
        string Name { get; }

        void Start();

        event EventHandler<GotMessageArgs> NewMessage;

        void Stop();

        Message Receive();

        void Send(Message msg);
    }
}