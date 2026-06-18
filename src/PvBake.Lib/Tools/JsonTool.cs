using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PvBake.Lib.Tools
{
    public static class JsonTool
    {
        public static string ToJson(object obj, bool format = false)
        {
            var config = new JsonSerializerSettings
            {
                Formatting = format ? Formatting.Indented : Formatting.None,
                NullValueHandling = NullValueHandling.Include,
                Converters = { new StringEnumConverter() }
            };
            return JsonConvert.SerializeObject(obj, config);
        }
    }
}