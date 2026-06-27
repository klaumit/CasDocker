using System;
using System.Linq;
using System.Text;
using DevForge.Lib.API;
using DevForge.Lib.Common;

namespace DevForge.Lib.Messages
{
    public static class MsgTool
    {
        public static string AsString(this byte[] buffer)
        {
            var text = Encoding.ASCII.GetString(buffer);
            var res = text.Trim('\0').Trim();
            return res;
        }

        private static readonly byte[] Sync = { 0xAA, 0x55 };

        public static Message ReadMessage(this ICommPort port)
        {
            var head = port.ReadBytes(5);
            if (head.Length != 5)
                return null;
            if (head[0] != Sync[0])
                return null;
            if (head[1] != Sync[1])
                return null;
            var kind = (MsgKind)head[2];
            if (!typeof(MsgKind).IsEnumDefined(kind))
                return null;
            var length = (ushort)((head[4] << 8) | head[3]);
            if (length < 1 || length > 4096)
                return null;
            var rest = length + 2;
            var body = port.ReadBytes(rest);
            if (body.Length != rest)
                return null;
            var yourCheck = (ushort)((body[rest - 1] << 8) | body[rest - 2]);
            Array.Resize(ref body, rest - 2);
            var all = head.Concat(body).ToArray();
            var myCheck = ushort.MinValue;
            Checking.UpdateCrc(ref myCheck, all);
            if (yourCheck != myCheck)
                return null;
            var msg = new Message
            {
                Kind = kind, Length = length, Payload = body, Checksum = myCheck
            };
            return msg;
        }
    }
}