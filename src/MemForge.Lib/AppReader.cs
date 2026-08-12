using System.Diagnostics;
using System.IO;
using System.Linq;
using Hexer.Core;
using EE = Hexer.Core.ElfExt;
using MR = MemForge.Lib.MemReader;

// ReSharper disable UseStringInterpolation

namespace MemForge.Lib
{
    public static class AppReader
    {
        public static void WriteAllFound(uint pid, string wName)
        {
            var bName = string.Format("proc_{0}_apps", pid);
            if (!Directory.Exists(bName))
                Directory.CreateDirectory(bName);
            foreach (var item in MR.ReadAll(pid))
            {
                var ii = item.Info;
                if (ii.AllocationProtect == 0x00000004 && ii.RegionSize == 0x00085000 &&
                    ii.State == 0x00001000 && ii.Protect == 0x00000004 && ii.Type == 0x00020000)
                {
                    var array = item.Buffer.SwapEndian(true);
                    var pvaIndex = array.IndicesOf(Consts.PvaMarkB).ToArray();
                    var rldIndex = array.IndicesOf(Consts.RldMarkB).ToArray();
                    if (!(pvaIndex.Length >= 1 && rldIndex.Length >= 1))
                        continue;
                    var anchors = EE.FindAnchors(pvaIndex, rldIndex).ToArray();
                    if (!(anchors.Length >= 1))
                        continue;
                    var baseAddr = ii.BaseAddress.ToInt64();
                    foreach (var anchor in anchors)
                    {
                        var name = string.Format("{0:x8}_{1:D2}.pva", baseAddr, anchor.I);
                        var full = Path.Combine(bName, name);


                        
                        
                        

                        Debugger.Break();
                    }
                }
            }
        }
    }
}