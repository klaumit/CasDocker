using System.Diagnostics;
using System;
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

        public static ProcessStartInfo New(string exe, string pwd = null, string args = null)
        {
            var info = new ProcessStartInfo
            {
                FileName = exe
            };
            if (!string.IsNullOrWhiteSpace(pwd))
                info.WorkingDirectory = pwd;
            if (!string.IsNullOrWhiteSpace(args))
                info.Arguments = args;
            info.UseShellExecute = false;
            return info;
        }

        public static bool Listen(this ProcessStartInfo info, int sec = 5,
            OutputFilter filter = null)
        {
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            using (var proc = new Process { StartInfo = info })
            {
                proc.EnableRaisingEvents = true;
                proc.ErrorDataReceived += (o, e) =>
                {
                    Console.WriteLine(" {0}", e.Data);
                    if (filter != null) filter.Invoke(e.Data);
                };
                proc.OutputDataReceived += (o, e) =>
                {
                    Console.WriteLine(" {0}", e.Data);
                    if (filter != null) filter.Invoke(e.Data);
                };
                if (!proc.Start())
                {
                    var name = Path.GetFileNameWithoutExtension(info.FileName);
                    throw new InvalidOperationException(
                        string.Format("Could not start process '{0}'!", name)
                    );
                }
                proc.BeginErrorReadLine();
                proc.BeginOutputReadLine();
                return proc.WaitForExit(sec * 1000);
            }
        }

        public static bool Start(this ProcessStartInfo info, int sec = 5)
        {
            using (var proc = Process.Start(info))
            {
                return proc.WaitForInputIdle(sec * 1000);
            }
        }
    }
}