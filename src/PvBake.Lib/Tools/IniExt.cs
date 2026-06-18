using IniParser;
using IniParser.Model;

namespace PvRanger
{
    public static class IniExt
    {
        public static IniData ReadFile(string file)
        {
            var enc = TextExt.Win;
            var parser = new FileIniDataParser();
            var ini = parser.ReadFile(file, enc);
            return ini;
        }
    }
}