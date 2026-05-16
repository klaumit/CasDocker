using IniParser.Model;

namespace PvMake.Lib
{
    public sealed class Project
    {
        public const string Fn = "project.ini";

        private const string App = "Application";
        private const string Nom = "Name";
        private const string Tit = "Title";
        private const string Ver = "Version";

        private const string Tgt = "Target";
        private const string Mod = "Models";

        public Project(IniData ini)
        {
            var app = ini.Sections[App];
            AppName = app[Nom];
            AppTitle = app[Tit];
            AppVer = app[Ver];
            var tgt = ini.Sections[Tgt];
            ForModels = TextExt.Split(',', tgt[Mod]);
        }

        public string AppName { get; private set; }
        public string AppTitle { get; private set; }
        public string AppVer { get; private set; }
        public string[] ForModels { get; private set; }
    }
}