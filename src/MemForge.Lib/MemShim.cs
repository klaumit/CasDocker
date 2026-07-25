using System;
using System.IO;
using System.Runtime.InteropServices;
using K = Vanara.PInvoke.Kernel32;
using Vanara.PInvoke;
using DevForge.Lib.Tools;
using System.Text;

namespace MemForge.Lib
{
    public sealed class MemShim : IDisposable
    {
        private const int MARKER_SIZE = 24;
        private const int SHM_BUF_SIZE = 256;
        private const int DWORD_SIZE = 4;

        private const int OFF_MARKER_BEG = 0;
        private const int OFF_TX_READY = OFF_MARKER_BEG + MARKER_SIZE;
        private const int OFF_TX_LEN = OFF_TX_READY + DWORD_SIZE;
        private const int OFF_TX_BUF = OFF_TX_LEN + DWORD_SIZE;
        private const int OFF_MARKER_MID = OFF_TX_BUF + SHM_BUF_SIZE;
        private const int OFF_RX_READY = OFF_MARKER_MID + MARKER_SIZE;
        private const int OFF_RX_LEN = OFF_RX_READY + DWORD_SIZE;
        private const int OFF_RX_BUF = OFF_RX_LEN + DWORD_SIZE;
        private const int OFF_MARKER_END = OFF_RX_BUF + SHM_BUF_SIZE;

        private const int STRUCT_SIZE = OFF_MARKER_END + MARKER_SIZE;
        private const int TX_REGION_SIZE = DWORD_SIZE + DWORD_SIZE + SHM_BUF_SIZE;

        private readonly K.SafeHPROCESS _proc;
        private readonly IntPtr _baseAddr;

        public uint TxAddr { get; set; }
        public uint RxAddr { get; set; }
        public uint EnAddr { get; set; }

        public MemShim(K.SafeHPROCESS proc, IntPtr baseAddr)
        {
            _proc = proc;
            _baseAddr = baseAddr;

            PrintDump();
            IsValid();
        }

        public bool IsValid()
        {
            if (_proc.IsNull || _proc.IsInvalid || _proc.IsClosed)
                return false;
            const string mark = "###ML";
            var enc = Encoding.ASCII;
            var beg = ReadString(enc, OFF_MARKER_BEG, 24, true).Split('_');
            if (beg.Length != 3 || beg[0] != mark || beg[2] != "BEG15###")
                return false;
            TxAddr = (uint)TextExt.ParseHex(beg[1], -1);
            var mid = ReadString(enc, OFF_MARKER_MID, 24, true).Split('_');
            if (mid.Length != 3 || mid[0] != mark || mid[2] != "MID16###")
                return false;
            RxAddr = (uint)TextExt.ParseHex(mid[1], -1);
            var end = ReadString(enc, OFF_MARKER_END, 24, true).Split('_');
            if (end.Length != 3 || end[0] != mark || end[2] != "END17###")
                return false;
            EnAddr = (uint)TextExt.ParseHex(end[1], -1);
            return true;
        }

		private void PrintDump()
		{
            var buffer = ReadBytes(0, STRUCT_SIZE);
            File.WriteAllBytes("ms1b.bin", buffer);
            File.WriteAllText("ms1b.txt", TextExt.ToHexString(buffer));

            var array = buffer.SwapEndian();
            File.WriteAllBytes("ms1l.bin", array);
            File.WriteAllText("ms1l.txt", TextExt.ToHexString(array));
		}

		public int Read(byte[] buffer, int offset, int count)
        {
            var region = ReadBytes(OFF_TX_READY, TX_REGION_SIZE);
            var ready = Ends.ToUInt32(region, 0, true);
            if (ready == 0)
                return 0;

            var len = Ends.ToUInt32(region, DWORD_SIZE, true);
            var got = Math.Min(len, Math.Min(count, SHM_BUF_SIZE));
            Array.Copy(region, DWORD_SIZE * 2, buffer, offset, got);

            WriteDWord(OFF_TX_READY, 0);
            return (int)got;
        }

        public bool Write(byte[] buffer, int offset, int count, int? origSize)
        {
            if (ReadDWord(OFF_RX_READY) != 0)
                return false;

            var len = Math.Min(count, SHM_BUF_SIZE);
            var payload = new byte[SHM_BUF_SIZE];
            Array.Copy(buffer, offset, payload, 0, len);

            var official = origSize ?? count;
            WriteBytes(OFF_RX_BUF, payload);
            WriteDWord(OFF_RX_LEN, (uint)official);
            WriteDWord(OFF_RX_READY, (uint)official);
            return true;
        }

        private string ReadString(Encoding enc, int offset, int len, bool bigEndian)
        {
            var bytes = ReadBytes(offset, len);
            if (bigEndian)
                bytes = bytes.SwapEndian();
            var text = enc.GetString(bytes);
            return text.CleanTrim();
        }

        private uint ReadDWord(int offset)
        {
            var buf = ReadBytes(offset, DWORD_SIZE);
            return Ends.ToUInt32(buf, 0, true);
        }

        private byte[] ReadBytes(int offset, int size)
        {
            var buf = new byte[size];
            var addr = IntPtr.Add(_baseAddr, offset);
            var handle = GCHandle.Alloc(buf, GCHandleType.Pinned);
            try
            {
                var buff = handle.AddrOfPinnedObject();
                SizeT rs;
                K.ReadProcessMemory(_proc, addr, buff, size, out rs);
            }
            finally
            {
                handle.Free();
            }
            return buf;
        }

        private void WriteDWord(int offset, uint value)
        {
            var buf = Ends.FromUInt32(value, false); /* even if big? */
            WriteBytes(offset, buf);
        }

        private void WriteBytes(int offset, byte[] buf)
        {
            var addr = IntPtr.Add(_baseAddr, offset);
            var handle = GCHandle.Alloc(buf, GCHandleType.Pinned);
            try
            {
                var buff = handle.AddrOfPinnedObject();
                var size = buf.Length;
                SizeT ws;
                K.WriteProcessMemory(_proc, addr, buff, size, out ws);
            }
            finally
            {
                handle.Free();
            }
        }

        public void Dispose()
        {
            if (_proc != null)
                _proc.Dispose();
        }
    }
}