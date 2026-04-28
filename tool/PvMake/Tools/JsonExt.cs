using System.IO;
using System.Text.Json;

namespace PvMake.Tools
{
    public static class JsonExt
    {
        public static T? ReadJson<T>(this StreamReader reader)
        {
            var raw = reader.ReadToEnd();
            if (raw.TrimOrNull() is not { } json)
                return default;
            return JsonSerializer.Deserialize<T>(json);
        }

        public static T? ReadJson<T>(string file)
        {
            using var stream = File.OpenText(file);
            return stream.ReadJson<T>();
        }
    }
}