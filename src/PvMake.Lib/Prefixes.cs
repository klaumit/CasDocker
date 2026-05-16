namespace PvMake.Lib
{
    public static class Prefixes
    {
        public static string GetPvPrefix()
        {
            var pvPre = FileExt.GetEnvDir("PV_PREFIX", true);
            if (string.IsNullOrWhiteSpace(pvPre))
                pvPre = FileExt.GetAssDir("_pv", true);
            return pvPre;
        }
    }
}