using DevForge.Lib.API;

namespace DevForge.Lib.Modern
{
    public sealed class ModernDevice : ICommDevice
    {
        public void Start()
        {
            Universals.Fuck();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}