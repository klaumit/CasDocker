using System.Threading.Tasks;
using SoftCircuits.IniFileParser;

namespace PvMake.Tools
{
    public static class IniExt
    {
        public static async Task<IniFile> ReadFile(string file)
        {
            var ini = new IniFile();
            await ini.LoadAsync(file, TextExt.Utf);
            return ini;
        }
    }
}