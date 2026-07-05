using System;
using DevForge.Lib.Messages;

// ReSharper disable UnusedMemberInSuper.Global

namespace DevForge.Lib.API
{
    public interface ICommDevice : IDisposable
    {
        void Start();

        void Stop();

        void Send(Message msg);

        string Name { get; }
    }
}