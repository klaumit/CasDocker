namespace DevForge.Lib.API
{
    public interface IPvBuff
    {
        ushort Src { get; }
        ushort Seg { get; }
        ushort Off { get; }
    }
}