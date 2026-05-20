namespace PvMake.Lib
{
    public static class KnowIt
    {
        public enum Known
        {
            None = 0,
            ModelX86, CompilerX86,
            ModelSH3, CompilerSH3
        }

        public static Known CheckKind(string name)
        {
            var lbl = name.ToUpperInvariant();
            if (lbl == "LSIJ") return Known.CompilerX86;
            if (lbl == "SHC") return Known.CompilerSH3;
            if (lbl.StartsWith("PV2")) return Known.ModelX86;
            if (lbl.StartsWith("PV3")) return Known.ModelSH3;
            return Known.None;
        }

        public static bool IsHitachi(string sdk)
        {
            var kind = CheckKind(sdk);
            return ( kind == Known.ModelSH3 || kind == Known.CompilerSH3 );
        }
    }
}