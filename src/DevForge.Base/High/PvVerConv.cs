using System;
using System.Globalization;
using DevForge.Lib.Tools;
using Newtonsoft.Json;

namespace DevForge.Lib.High
{
    public sealed class PvVerConv : JsonConverter<PvVer>
    {
        private const int ExpectedLength = 16;

        public override void WriteJson(JsonWriter writer, PvVer value, JsonSerializer s)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            var d = value.OsDate;
            var v = value.OsVer;
            var txt = string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}{6:D2}",
                d.Year, d.Month, d.Day, d.Hour, d.Minute, v.Major, v.Minor);
            writer.WriteValue(txt);
        }

        public override PvVer ReadJson(JsonReader reader, Type objectType,
            PvVer existingValue, bool hasExistingValue, JsonSerializer s)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            if (reader.TokenType == JsonToken.String)
            {
                var raw = ((string)reader.Value).TrimOrNull();
                if (raw != null && raw.Length == ExpectedLength)
                {
                    var cult = CultureInfo.InvariantCulture;
                    return new PvVer
                    {
                        OsDate = new DateTime(
                            ushort.Parse(raw.Substring(0, 4), cult),
                            byte.Parse(raw.Substring(4, 2), cult),
                            byte.Parse(raw.Substring(6, 2), cult),
                            byte.Parse(raw.Substring(8, 2), cult),
                            byte.Parse(raw.Substring(10, 2), cult),
                            0, DateTimeKind.Utc),
                        OsVer = new Version(
                            byte.Parse(raw.Substring(12, 2), cult),
                            byte.Parse(raw.Substring(14, 2), cult)
                        )
                    };
                }
            }
            return null;
        }
    }
}