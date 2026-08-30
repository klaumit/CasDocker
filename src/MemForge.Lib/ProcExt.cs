using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

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
            KillAll(Process.GetProcessesByName(name));
        }

        public static void KillAll(params Process[] procs)
        {
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

        public static void Start(string fileName)
        {
            var workDir = Path.GetDirectoryName(fileName);
            var info = new ProcessStartInfo { FileName = fileName };
            if (!string.IsNullOrWhiteSpace(workDir))
                info.WorkingDirectory = workDir;
            Process.Start(info);
        }

        public static Process GetByPid(uint? pid)
        {
            return pid == null
                ? null
                : Process.GetProcessById((int)pid.Value);
        }
    }
}