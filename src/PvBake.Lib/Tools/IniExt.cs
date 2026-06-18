using System.IO;
using IniParser;
using IniParser.Model;
using PvBake.Lib.Models;

namespace PvBake.Lib.Tools
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

        public static Project ReadProject(string file)
        {
            var iniData = ReadFile(file);
            var iniDir = Path.GetDirectoryName(file);
            var group = iniData["CSGROUP5"];
            return new Project
            {
                biosFile = ValueTool.Search(iniDir, group["CHIPFILE0"]),
                biosOffs = group["CHIPOFFSET0"].ParseHex(),
                applFile = ValueTool.Search(iniDir, group["CHIPFILE1"]),
                applOffs = group["CHIPOFFSET1"].ParseHex()
            };
        }
    }
}