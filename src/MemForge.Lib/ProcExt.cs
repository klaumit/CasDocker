using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MemForge.Lib
{
    public static class ProcExt
    {
        public static void KillAll(string name)
        {
            var procs = Process.GetProcessesByName(name);
            foreach (var proc in procs)
                proc.Kill();
        }
    }

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