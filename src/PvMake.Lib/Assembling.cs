using System;
using System.IO;
using PvMake.Lib;
using W = PvMake.Lib.Writing;
using M = PvMake.Lib.Making;
using A = PvMake.Lib.Assembling;
using S = PvMake.Lib.Siming;
using System.Collections.Generic;
using K = PvMake.Lib.KnowIt;
using FileExt = Pva2cpa.Lib.Files;
using FileEx = PvMake.Lib.FileExt;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Pva2cpa.Lib;

// ReSharper disable UseStringInterpolation
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable InlineOutVariableDeclaration

namespace PvMake.Lib
{
    public static class Assembling
    {
        public static void FixHitachiAsm(string pDir)
        {
            var myFiles = FileEx.FindAllFiles(pDir);
            SortedSet<string> mkFiles;
            myFiles.TryGetValue(".mak", out mkFiles);

            const string ccInfo = "CCINF{0}=";
            if (mkFiles != null)
                foreach (var file in mkFiles)
                {
                    var isDirty = false;
                    var text = File.ReadAllText(file, TextExt.Win);
                    for (var i = 0; i < 1; i++)
                    {
                        var tm = string.Format(ccInfo, i);
                        if (text.Contains(tm))
                        {
                            text = text.Replace(tm, tm + "-code=asmcode ");
                            isDirty = true;
                        }
                    }
                    if (!isDirty)
                        continue;
                    Console.WriteLine(" * '" + file + "'");
                    File.WriteAllText(file, text, TextExt.Win);
                }
        }
    }
}