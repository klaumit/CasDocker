using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DevForge.Lib.Tools
{
    public static class JsonExt
    {
        private static JsonSerializerSettings GetConfig(bool fmt = true)
        {
            var opt = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = fmt ? Formatting.Indented : Formatting.None
            };
            return opt;
        }

        public static string ToJson<T>(T obj, bool fmt = true)
        {
            if (obj == null)
                return null;
            return JsonConvert.SerializeObject(obj, GetConfig(fmt));
        }

        public static T ToObj<T>(string raw)
        {
            string text;
            if ((text = raw.TrimOrNull()) == null)
                return default;
            return JsonConvert.DeserializeObject<T>(text, GetConfig());
        }
    }
}