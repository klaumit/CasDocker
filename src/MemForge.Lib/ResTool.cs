using System;
using System.IO;

namespace MemForge.Lib
{
	public static class ResTool
    {
        public static Stream GetStream(Type type, params string[] parts)
        {
            var ass = type.Assembly;
            var nsp = type.Namespace;
            var fup = nsp + "." + string.Join(".", parts);
            var stream = ass.GetManifestResourceStream(fup);
            return stream;
        }
    }
}