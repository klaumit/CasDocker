using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.IO;

namespace MemForge.Lib
{
    public delegate void ProcStarted(object sender, uint pid, string name);

    public sealed class ProcWatcher : IDisposable
    {
        private ManagementEventWatcher _watcher;
        private List<string> _names;

        public ProcWatcher(params string[] names)
        {
            _names = new List<string>(names);
            string query = "SELECT * FROM Win32_ProcessStartTrace";
            _watcher = new ManagementEventWatcher(query);
            _watcher.EventArrived += ProcessStarted;
            _watcher.Start();
        }

        public ProcStarted Started { get; set; } 

        private void ProcessStarted(object sender, EventArrivedEventArgs e)
        {
            var procExe = (string)e.NewEvent["ProcessName"];
            var procName = Path.GetFileNameWithoutExtension(procExe);
            if (_names.Contains(procName))
            {
                uint procId = (uint)e.NewEvent["ProcessID"];
                if (Started != null)
                    Started(this, procId, procName);
            }             
        }

        public void Dispose()
        {
            Started = null;
            _watcher.Stop();
            _watcher.Dispose();
            _names.Clear();
        }
    }
}