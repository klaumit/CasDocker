using System;
using System.Collections.Generic;
using System.Management;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace MemForge.Lib
{
    public sealed class ProcWatcher : IDisposable
    {
        private ManagementEventWatcher _watcher;
        private List<string> _names;

        public ProcWatcher(ProcStarted started, params string[] names)
        {
            Started = started;
            _names = new List<string>(names);
            var query = "SELECT * FROM Win32_ProcessStartTrace";
            _watcher = new ManagementEventWatcher(query);
            _watcher.EventArrived += ProcessStarted;
            try
            {
                _watcher.Start();
            }
            catch (ManagementException)
            {
                // Ignore!
            }

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                ProcExt.Find(_names, (o, i, n) => FireStarted(i, n));
            });
        }

        public ProcStarted Started { get; private set; }

        private void ProcessStarted(object sender, EventArrivedEventArgs e)
        {
            var procExe = (string)e.NewEvent["ProcessName"];
            var procName = Path.GetFileNameWithoutExtension(procExe);
            if (_names.Contains(procName))
            {
                var procId = (uint)e.NewEvent["ProcessID"];
                FireStarted(procId, procName);
            }             
        }

        private void FireStarted(uint procId, string procName)
        {
            if (Started != null)
                Started(this, procId, procName);
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