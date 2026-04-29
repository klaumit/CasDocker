using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            if (patchHit)
                preProc.AddSymbol("__HITACHI__");
            foreach (var file in files ?? [])
            {
                var name = Path.GetFileName(file);
                var tgt = Path.Combine(dest, name);
                string content;
                using (var input = new StreamReader(file, Encoding.ASCII))
                {
                    var bld = new StringBuilder();
                    using var writer = new StringWriter(bld);
                    var report = new ReportList();
                    if (!preProc.Process(input, writer, report))
                        throw new InvalidOperationException(ToText(file, report));
                    content = writer.ToString();
                    if (!content.Contains("\r\n"))
                        content = content.Replace("\n", "\r\n");
                }
                File.WriteAllText(tgt, content);
                var lineCount = content.Count('\n');
                Console.WriteLine($"    + {name} ({lineCount} L) => {tgt}");
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