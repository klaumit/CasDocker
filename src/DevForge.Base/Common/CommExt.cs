using DevForge.Lib.API;

namespace DevForge.Lib.Common
{
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