using System;

namespace DevForge.Lib.API
{
    public interface ICommDevice : IDisposable
    {
        void Start();

        void Stop();
    }
}