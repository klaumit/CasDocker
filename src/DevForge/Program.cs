using DevForge.Lib.API;
using DevForge.Lib.Legacy;
using DevForge.Lib.Modern;

namespace DevForge
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ICommDevice dev1 = new LegacyCommDevice();
            ICommDevice dev2 = new ModernCommDevice();
        }
    }
}