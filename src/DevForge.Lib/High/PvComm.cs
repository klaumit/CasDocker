using System.Runtime.Serialization;

namespace sss
{
    public enum PvComm
    {
        Unknown = 0,

        [EnumMember(Value = "9pin")] _9pin,

        [EnumMember(Value = "USB")] _USB
    }
}