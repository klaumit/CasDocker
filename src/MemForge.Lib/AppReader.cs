using System.IO;
using System.Linq;
using Hexer.Core;
using System;
using System.Diagnostics;
using BW = Hexer.Wraps.BinWrap;
using AI = Hexer.Core.AppInfo;
using EE = Hexer.Core.ElfExt;
using MR = MemForge.Lib.MemReader;

// ReSharper disable UseStringInterpolation

namespace MemForge.Lib
{
    public static class AppReader
    {
        public static int WriteAllFound(uint pid, string wName)
        {
            var count = 0;
            var localDir = string.Format("proc_{0}_apps", pid);
            if (!Directory.Exists(localDir))
                Directory.CreateDirectory(localDir);
            var pvaBytes = Consts.PvaMarkB;
            var rldBytes = Consts.RldMarkB;
            foreach (var item in MR.ReadAll(pid))
            {
                var ii = item.Info;
                if (ii.AllocationProtect == 0x00000004 && ii.RegionSize == 0x00085000 &&
                    ii.State == 0x00001000 && ii.Protect == 0x00000004 && ii.Type == 0x00020000)
                {
                    var array = item.Buffer.SwapEndian(true);
                    var pvaIdx = array.IndicesOf(pvaBytes).ToArray();
                    var rldIdx = array.IndicesOf(rldBytes).ToArray();
                    if (!(pvaIdx.Length >= 1 && rldIdx.Length >= 1))
                        continue;
                    var anchors = EE.FindAnchors(pvaIdx, rldIdx).ToArray();
                    if (anchors.Length < 1)
                        continue;
                    var baseAddr = ii.BaseAddress.ToInt64();
                    foreach (var anchor in anchors)
                    {
                        var pvaName = string.Format("{0:x8}_{1:D2}.pva", baseAddr, anchor.I);
                        var e = anchor.GetSizes(array);
                        var ai = new AI { Name = pvaName };
                        try
                        {
                            BW.ExtractFiles(ai, e.pvaSize, e.elfSize, array, anchor, localDir);
                            count++;
                        }
                        catch (ArgumentException ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }
                }
            }
            return count;
        }
    }
}