using System.Collections.Generic;
using PvMake.Lib.Resources;

namespace PvMake.Lib
{
    public static class Models
    {
        public static Dir2Model ReadDirToModel()
        {
            var json = ResTool.GetText("models.json");
            var dict = JsonExt.ToObj<IDictionary<string, string[]>>(json);
            var back = new Dictionary<string, string>();
            foreach (var entry in dict)
            foreach (var val in entry.Value)
                back.Add(val, entry.Key);
            return new Dir2Model(dict, back);
        }
    }
}