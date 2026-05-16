using PvMake.Lib.Resources;
using System.Collections.Generic;

// ReSharper disable InlineOutVariableDeclaration

namespace PvMake.Lib
{
    public static class Archives
    {
        public static Dictionary<string, Archive> ReadList()
        {
            var json = ResTool.GetText("parts.json");
            var list = JsonExt.ToObj<IList<Archive>>(json);
            var back = new Dictionary<string, Archive>();
            foreach (var entry in list)
                back.Add(entry.Name, entry);
            return back;
        }

        public static IEnumerable<Archive> Iter(this IDictionary<string, Archive> archives, string key)
        {
            Archive archive;
            if (archives.TryGetValue(key, out archive))
            {
                yield return archive;
                if (archive.Uses != null)
                    foreach (var subU in archive.Uses)
                    foreach (var subA in archives.Iter(subU))
                        yield return subA;
            }
        }
    }
}