using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MemForge.Lib
{
    public static class Defaults
    {
        public const string Sim86 = "Sim3022";

        public const string SimSh = "CASIO SimSH";
    }

    public static class ProcExt
    {
        public static void KillAll(string name)
        {
            var procs = Process.GetProcessesByName(name);
            foreach (var proc in procs)
                proc.Kill();
        }

        public static void Find(IEnumerable<string> names, ProcStarted started)
        {
            foreach (var proc in Process.GetProcesses())
            {
                var procName = proc.ProcessName;
                if (names.Contains(procName))
                {
                    var procId = (uint)proc.Id;
                    started(null, procId, procName);
                }
            }
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