using DevForge.Lib.API;

namespace DevForge.Lib.Modern
{
    public sealed class ModernDevice : ICommDevice
    {
        private UsbPort _port;

        public void Start()
        {
            _port = Universals.CreatePort();
        }

        public void Stop()
        {
            Universals.ClosePort(ref _port);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}