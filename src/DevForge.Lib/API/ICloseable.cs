using System;

namespace DevForge.Lib.API
{
    public interface ICloseable : IDisposable
    {
        void Close();
    }
}