using System;

namespace DevForge.Lib.API
{
    public interface ICommPort : IDisposable, ICloseable
    {
        void Open();

        void Close();

        byte[] ReadBytes(int count);

        bool WriteBytes(byte[] buffer);
    }
}