namespace PvMake.Lib
{
    public sealed class Model
    {
        public Model(string model, string sdk)
        {
            Mod = model;
            Sdk = sdk;
        }

        public string Mod { get; private set; }
        public string Sdk { get; private set; }
    }
}