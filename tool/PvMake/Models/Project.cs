using SoftCircuits.IniFileParser;

namespace PvMake.Models
{
    public sealed class Project
    {
        internal const string Fn = "project.ini";

        private const string App = "Application";
        private const string Nom = "Name";
        private const string Tit = "Title";
        private const string Ver = "Version";

        public Project(IniFile ini)
        {
            AppName = ini.GetSetting(App, Nom);
            AppTitle = ini.GetSetting(App, Tit);
            AppVer = ini.GetSetting(App, Ver);
        }

        public string? AppName { get; }
        public string? AppTitle { get; }
        public string? AppVer { get; }
    }
}