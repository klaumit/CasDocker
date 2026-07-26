using System.IO;
using System.Text;
using Newtonsoft.Json;
using F = System.IO.File;

// ReSharper disable ConvertIfStatementToNullCoalescingAssignment
// ReSharper disable MoveVariableDeclarationInsideLoopCondition
// ReSharper disable UseStringInterpolation

namespace DevForge.Lib.Common
{
    public sealed class XxdStat
    {
        public string File { get; }
        public XxdInfo Info { get; private set; }

        public XxdStat(string file)
        {
            File = file;
            Info = new XxdInfo();
        }

        public void Read()
        {
            if (!F.Exists(File)) { Info = new XxdInfo(); return; }
            var txt = F.ReadAllText(File, Encoding.UTF8);
            Info = JsonConvert.DeserializeObject<XxdInfo>(txt) ?? new XxdInfo();
        }

        public void Write()
        {
            var txt = JsonConvert.SerializeObject(Info, Formatting.Indented);
            F.WriteAllText(File, txt, Encoding.UTF8);
        }

        public override string ToString()
        {
            var name = Path.GetFileNameWithoutExtension(File);
            return string.Format("[Info] '{0}' at {1} => {2} range(s)",
                name, Info.Pos, Info.Ranges?.Count ?? 0);
        }
    }
}