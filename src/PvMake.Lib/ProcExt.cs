using System.Diagnostics;

namespace PvMake.Lib
{
    public static class ProcExt
    {
        public static bool Start(string exe, string pwd, string args)
        {
            var info = new ProcessStartInfo
            {
                FileName = exe
            };
            if (!string.IsNullOrWhiteSpace(pwd))
                info.WorkingDirectory = pwd;
            if (!string.IsNullOrWhiteSpace(args))
                info.Arguments = args;
            var proc = Process.Start(info);
            return proc.WaitForInputIdle(5 * 1000);            
        }
    }
}