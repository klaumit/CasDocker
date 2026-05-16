using IniParser;
using IniParser.Model;

namespace PvMake.Lib
{
    public static class IniExt
    {
        private static IniData ReadFile(string file)
        {
            var enc = TextExt.Utf;
            var parser = new FileIniDataParser();
            var ini = parser.ReadFile(file, enc);
            return ini;
        }

        public static Project ReadProj(string file)
        {
            var ini = ReadFile(file);
            return new Project(ini);
        }
    }
}