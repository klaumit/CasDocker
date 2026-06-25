using System;

// ReSharper disable UnusedMemberInSuper.Global

namespace DevForge.Lib.API
{
    public interface ICommDevice : IDisposable
    {
        void Start();

        void Stop();
    }
}