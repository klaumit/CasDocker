namespace DevForge.Lib.API
{
    public interface ICommPort : ICloseable
    {
        void Open();

        byte[] ReadBytes(int count);

        bool WriteBytes(byte[] buffer);
    }
}