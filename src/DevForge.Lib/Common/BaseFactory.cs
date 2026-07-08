using System.Linq;
using DevForge.Lib.API;

namespace DevForge.Lib.Common
{
    public abstract class BaseFactory : ICommFactory
    {
        public string Prefix { get { return GetType().Name.Split('F').First(); } }

        public abstract ICommPort Create();
    }
}