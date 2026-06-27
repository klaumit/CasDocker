namespace DevForge.Lib.API
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