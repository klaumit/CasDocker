using System.Linq;
using DevForge.Lib.API;

namespace DevForge.Lib.Common
{
    public abstract class BaseFactory : ICommFactory
    {
        public string Prefix { get { return GetType().Name.Split('F').First(); } }

        public abstract ICommPort Create();
    }
    
    public static class CommExt
    {
        public static void ClosePort(ref ICommPort port)
        {
            if (port == null)
                return;
            using (port)
                port.Close();
            port = null;
        }
    }
}