using DevForge.Lib.API;

namespace DevForge.Lib.Modern
{
    public sealed class ModernPort : ICommPort
    {
        private readonly string _devicePath;

        public ModernPort(string devicePath)
        {
            _devicePath = devicePath;
        }

        public void Open()
        {
            throw new System.NotImplementedException();
        }
    }
}