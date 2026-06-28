using System;

namespace DevForge.Lib.API
{
    public interface ICommPort : IDisposable
    {
        void Open();

        void Close();

        byte[] ReadBytes(int count);

        bool WriteBytes(byte[] buffer);
    }
}