using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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
}