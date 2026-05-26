using System.Diagnostics;
using System;
using System.Threading;
using System.IO;

namespace PvMake.Lib
{
    public static class ProcExt
    {
        private static void PrintLines(StreamReader reader, string prefix)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
                Console.WriteLine(prefix + line);
        }

        public static bool Start(string exe, string pwd, string args, int sec = 2)
        {
            var info = new ProcessStartInfo
            {
                FileName = exe
            };
            if (!string.IsNullOrWhiteSpace(pwd))
                info.WorkingDirectory = pwd;
            if (!string.IsNullOrWhiteSpace(args))
                info.Arguments = args;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            var proc = Process.Start(info);

            var errThread = new Thread(() => 
                PrintLines(proc.StandardError, " ")) { IsBackground = true };
            errThread.Start();

            var outThread = new Thread(() =>
                PrintLines(proc.StandardOutput, " ")) { IsBackground = true };
            outThread.Start();

            try
            {
                return proc.WaitForInputIdle(sec * 1000);
            }
            catch (InvalidOperationException)
            {
                return proc.WaitForExit(sec * 1000);
            }            
        }
    }
}