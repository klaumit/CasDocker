using System.Collections.Generic;
using System.IO;
using PvMake.Models;
using PvMake.Tools;

namespace PvMake.Resources
{
    public static class ResTool
    {
        public static string GetDir(string model)
        {
            var dir = Path.Combine(nameof(Resources), model);
            return dir;
        }

        public static Dir2Model ReadDirToModel()
        {
            var file = Path.Combine(nameof(Resources), "dirToModel.json");
            var dict = JsonExt.ReadJson<IDictionary<string, string[]>>(file)!;
            var back = new Dictionary<string, string>();
            foreach (var entry in dict)
            foreach (var val in entry.Value)
                back.Add(val, entry.Key);
            return new Dir2Model(dict, back);
        }
    }
}