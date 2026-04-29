using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PvMake.Tools;
using SimpleTextPreprocessor;
using SimpleTextPreprocessor.ExpressionSolver;
using SimpleTextPreprocessor.IncludeResolver;

// ReSharper disable TooWideLocalVariableScope
// ReSharper disable UseObjectOrCollectionInitializer

namespace PvMake.Core
{
    internal static class Coding
    {
        public static void ReCopy(IEnumerable<string>? files, string dest)
        {
            if (files == null)
                return;
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                var bytes = File.ReadAllBytes(file);
                File.WriteAllBytes(tgt, bytes);
                Console.WriteLine($"    + {name} ({bytes.Length} B) => {tgt}");
            }
        }

        public static void ReWrite(IEnumerable<string>? files, string dest, bool patchHit = false)
        {
            if (files == null)
                return;
            var includer = new EmptyIncludeResolver();
            var solver = new DefaultExpressionSolver();
            var opt = PreprocessorOptions.Default;
            var preProc = new Preprocessor(includer, solver, opt);
            preProc.AddToIgnored("include");
            preProc.AddToIgnored("define");
            var symbols = new Dictionary<string, string>();
            if (patchHit)
            {
                preProc.AddSymbol("__HITACHI__");
                symbols["FAR"] = " ";
                symbols["PADE"] = ",\t0x00";
            }
            else
            {
                symbols["FAR"] = " far ";
                symbols["PADE"] = "";
            }
            foreach (var file in files ?? [])
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                var lines = new List<string>();
                using (var input = new StreamReader(file, Encoding.ASCII))
                {
                    var bld = new StringBuilder();
                    using var writer = new StringWriter(bld);
                    var report = new ReportList();
                    if (!preProc.Process(input, writer, report))
                        throw new InvalidOperationException(ToText(file, report));
                    string tmp;
                    foreach (var iLine in writer.ToString().Split('\n'))
                    {
                        var line = iLine;
                        foreach (var (key, val) in symbols)
                        {
                            tmp = $" {key} ";
                            if (line.Contains(tmp))
                                line = line.Replace(tmp, val);
                        }
                        lines.Add(line);
                    }
                }
                FileExt.WriteWin(tgt, lines);
                Console.WriteLine($"    + {name} ({lines.Count} L) => {tgt}");
            }
        }

        private static string ToText(string file, ReportList report)
        {
            var bld = new StringBuilder();
            bld.AppendLine($"[{file}]");
            foreach (var e in report.Entries)
                bld.AppendLine($"{e.FileId}({e.Line},{e.Column}): {e.Message}");
            return bld.ToString();
        }
    }
}