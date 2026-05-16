using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PvMake.Lib
{
    public static class JsonExt
    {
        private static JsonSerializerSettings GetConfig()
        {
            var opt = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            return opt;
        }

        public static T ToObj<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text, GetConfig());
        }
    }
}