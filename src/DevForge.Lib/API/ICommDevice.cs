using System;
using DevForge.Lib.Messages;

// ReSharper disable UnusedMemberInSuper.Global

namespace DevForge.Lib.API
{
    public interface ICommDevice : IDisposable
    {
        string Name { get; }

        void Start();

        void Stop();

        Message Receive();

        void Send(Message msg);
    }
}