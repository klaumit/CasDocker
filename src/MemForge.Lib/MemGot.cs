using static Vanara.PInvoke.Kernel32;

namespace MemForge.Lib
{
    public struct MemGot
    {
        public MemGot(string name, MEMORY_BASIC_INFORMATION info, byte[] buffer)
        {
            Name = name;
            Info = info;
            Buffer = buffer;
        }

        public string Name { get; set; }
        public MEMORY_BASIC_INFORMATION Info { get; set; }
        public byte[] Buffer { get; set; }
    }
}