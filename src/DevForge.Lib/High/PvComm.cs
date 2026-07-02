using System.Runtime.Serialization;

// ReSharper disable InconsistentNaming

namespace DevForge.Lib.High
{
    public enum PvComm
    {
        Unknown = 0,

        [EnumMember(Value = "9pin")] _9pin,

        [EnumMember(Value = "USB")] _USB
    }
}