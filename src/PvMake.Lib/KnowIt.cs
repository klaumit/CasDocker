// ReSharper disable InconsistentNaming

namespace PvMake.Lib
{
    public static class KnowIt
    {
        public enum Known
        {
            None = 0,
            ModelX86, CompilerX86,
            ModelSH3, CompilerSH3,
            ModelCLP
        }

        public static Known CheckKind(string name)
        {
            var lbl = name.ToUpperInvariant();
            if (lbl == "LSIJ") return Known.CompilerX86;
            if (lbl == "SHC") return Known.CompilerSH3;
            if (lbl.StartsWith("PV2")) return Known.ModelX86;
            if (lbl.StartsWith("PV3")) return Known.ModelSH3;
            if (lbl.StartsWith("CLP")) return Known.ModelCLP;
            return Known.None;
        }

        public static bool IsHitachi(string sdk)
        {
            var kind = CheckKind(sdk);
            return (kind == Known.ModelSH3 || kind == Known.CompilerSH3);
        }

        public static bool IsIntel(string sdk)
        {
            var kind = CheckKind(sdk);
            return (kind == Known.ModelX86 || kind == Known.CompilerX86);
        }

        public static bool IsClassPad(string sdk)
        {
            var kind = CheckKind(sdk);
            return (kind == Known.ModelCLP);
        }
    }
}