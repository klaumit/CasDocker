using System.IO;
using DevForge.Lib.API;

namespace DevForge.Tests
{
    internal sealed class FakePort : ICommPort
    {
        private MemoryStream? _mem;

        public void Open()
        {
            _mem = new MemoryStream();
        }

        public void Close()
        {
            _mem?.Dispose();
            _mem = null;
        }

        public void Dispose()
        {
            Close();
        }

        public bool WriteBytes(byte[] buffer)
        {
            _mem!.Write(buffer);
            _mem.Flush();
            return true;
        }

        public byte[] ReadBytes(int count)
        {
            var buffer = new byte[count];
            _ = _mem!.Read(buffer);
            return buffer;
        }

        public void Rewind(int pos = 0)
        {
            _mem!.Position = pos;
        }
    }
}