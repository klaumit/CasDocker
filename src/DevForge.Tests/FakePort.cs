using DevForge.Lib.API;

namespace DevForge.Tests
{
    internal sealed class FakePort : ICommPort
    {
        public void Open()
        {
        }

        public void Close()
        {
        }

        public void Dispose()
        {
            Close();
        }

        public byte[] ReadBytes(int count)
        {
            throw new System.NotImplementedException();
        }

        public bool WriteBytes(byte[] buffer)
        {
            throw new System.NotImplementedException();
        }
    }
}