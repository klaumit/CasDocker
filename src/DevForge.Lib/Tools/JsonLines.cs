using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DevForge.Lib.Tools
{
	public sealed class JsonLines : IDisposable
	{
		private StreamWriter _writer;
		private JsonSerializerSettings _config;

		public JsonLines(string path) : this(File.AppendText(path))
		{
		}

		public JsonLines(StreamWriter writer)
		{
			_writer = writer;
			_config = new JsonSerializerSettings
			{
				Formatting = Formatting.None,
				Converters = { new StringEnumConverter() },
				NullValueHandling = NullValueHandling.Include
			};
		}

		public void Write(object obj)
		{
			var json = JsonConvert.SerializeObject(obj, _config);
			_writer.WriteLine(json);
			_writer.Flush();
		}

		public void Dispose()
		{
			_writer.Close();
			_writer.Dispose();
		}
	}
}