using System;
using System.Linq;
using System.Text;
using DevForge.Lib.API;
using DevForge.Lib.Common;
using DevForge.Lib.Messages.Impl;

namespace DevForge.Lib.Messages
{
    public static class MsgTool
    {
        public const char Nu = '\0';

        public static byte[] AsBytes(this string text)
        {
            var input = text.Trim() + Nu;
            var res = Encoding.ASCII.GetBytes(input);
            return res;
        }

        public static string AsString(this byte[] buffer)
        {
            var text = Encoding.ASCII.GetString(buffer);
            var res = text.Trim(Nu).Trim();
            return res;
        }

        private static readonly byte[] Sync = { 0xAA, 0x55 };

        public static void WriteMessage(this ICommPort port, Message msg)
        {
            if (port == null)
                return;
            var body = msg.Payload;
            var length = (ushort)body.Length;
            var head = new[]
            {
                Sync[0], Sync[1], (byte)msg.Kind,
                (byte)(length & 0xFF), (byte)(length >> 8)
            };
            var all = head.Concat(body).ToArray();
            var check = ushort.MinValue;
            Checking.UpdateCrc(ref check, all);
            msg.Checksum = check;
            msg.Length = length;
            var end = new[] { (byte)(check & 0xFF), (byte)(check >> 8) };
            all = all.Concat(end).ToArray();
            port.WriteBytes(all);
        }

        public static Message ReadMessage(this ICommPort port)
        {
            if (port == null)
                return null;
            var head = port.ReadBytes(5);
            if (head.Length != 5)
                return null;
            var syncIdx1 = Array.IndexOf(head, Sync[0], 0);
            if (syncIdx1 < 0)
                return null;
            var syncIdx2 = Array.IndexOf(head, Sync[1], syncIdx1);
            if (syncIdx2 < 0)
                return null;
            if (syncIdx1 != 0 || syncIdx2 != 1)
            {
                head = head.Skip(syncIdx1).ToArray();
                var fail = port.ReadBytes(syncIdx1);
                if (fail != null && fail.Length == syncIdx1)
                    head = head.Concat(fail).ToArray();
            }
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
            switch (kind)
            {
                case MsgKind.Hello: return new Hello(msg);
                case MsgKind.Quit: return new Quit(msg);
                case MsgKind.Unknown:
                default: return msg;
            }
        }
    }
}