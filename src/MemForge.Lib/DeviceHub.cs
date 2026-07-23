using System.Threading.Tasks;
using DevForge.Lib.Common;
using DevForge.Lib.Modern;

// ReSharper disable UseNullPropagation
// ReSharper disable ClassNeverInstantiated.Global

namespace DevForge.Lib.Setup
{
    public sealed class DeviceHub : AbDeviceHub
    {
        public void StartMemory()
        {
            Task.Factory.StartNew(DoMemory);
        }

        private void DoMemory()
        {
            var factory = new MemoryFactory();
            DoOnePort(factory);
        }
    }
}