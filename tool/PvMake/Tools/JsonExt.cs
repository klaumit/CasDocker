using System.IO;
using System.Text.Json;

namespace PvMake.Tools
{
    public static class JsonExt
    {
        public static T? ReadJson<T>(this StreamReader reader)
        {
            var raw = reader.ReadLine();
            if (raw.TrimOrNull() is not { } json)
                return default;
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}