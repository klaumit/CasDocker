using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace MemForge.Lib
{
	public static class ProcExt
    {
        public static Process Find(string name)
        {
            var procs = Process.GetProcessesByName(name);
            var proc = procs.FirstOrDefault();
            return proc;
        }

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
                var procName = proc.ProcessName.ToLowerInvariant();
                if (names.Select(n => n.ToLowerInvariant()).Contains(procName))
                {
                    var procId = (uint)proc.Id;
                    started(null, procId, procName);
                }
            }
        }
    }
}