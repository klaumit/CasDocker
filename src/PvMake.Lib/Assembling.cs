using System.IO;
using System.Collections.Generic;
using FileEx = PvMake.Lib.FileExt;

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
            const string lnkAnc = "\t$(LNK) -";
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
                    if (text.Contains(lnkAnc))
                    {
                        var ins = "\t$(ASM) -cpu=sh3 -nologo";
                        text = text.Replace(lnkAnc, ins + "\r\n" + lnkAnc);
                        isDirty = true;
                    }
                    if (!isDirty)
                        continue;
                    File.WriteAllText(file, text, TextExt.Win);
                }
        }
    }
}