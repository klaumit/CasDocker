using MBI = Vanara.PInvoke.Kernel32.MEMORY_BASIC_INFORMATION;

namespace MemForge.Lib
{
    public struct MemGot
    {
        public MemGot(string name, MBI info, byte[] buffer)
        {
            Name = name;
            Info = info;
            Buffer = buffer;
        }

        public string Name;
        public MBI Info;
        public byte[] Buffer;
    }
}