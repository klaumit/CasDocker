using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PvBake.Lib.Tools
{
    public static class JsonTool
    {
        public static JsonSerializerSettings GetConfig(bool format = false)
        {
            var config = new JsonSerializerSettings
            {
                Formatting = format ? Formatting.Indented : Formatting.None,
                NullValueHandling = NullValueHandling.Include,
                Converters = { new StringEnumConverter() }
            };
            return config;
        }

        public static string ToJson(object obj, bool format = false)
        {
            var config = GetConfig(format);
            return JsonConvert.SerializeObject(obj, config);
        }

        public static T FromJson<T>(string txt)
        {
            var config = GetConfig();
            return JsonConvert.DeserializeObject<T>(txt, config);
        }
    }
}