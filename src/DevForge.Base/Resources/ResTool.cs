using System.IO;
using System.Text;

namespace DevForge.Lib.Resources
{
	public static class ResTool
	{
		public static string GetPath(string name)
		{
			var type = typeof(ResTool);
			var ass = type.Assembly;
			var dll = Path.GetFullPath(ass.Location);
			var dir = Path.GetDirectoryName(dll);
			var full = Path.Combine(dir, "Resources", name);
			return full;
		}

		public static string ReadUtf(string path)
		{
			var text = File.ReadAllText(path, Encoding.UTF8);
			return text;
		}
	}
}