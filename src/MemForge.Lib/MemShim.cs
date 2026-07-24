using System;
using System.IO;
using System.Runtime.InteropServices;
using K = Vanara.PInvoke.Kernel32;
using Vanara.PInvoke;
using DevForge.Lib.Tools;

namespace MemForge.Lib
{
    public sealed class MemShim : IDisposable
    {
        private const int MARKER_SIZE = 24;
        private const int SHM_BUF_SIZE = 256;
        private const int WORD_SIZE = 2;
        private const int STRUCT_SIZE = OFF_MARKER_END + MARKER_SIZE;

        private const int OFF_MARKER_BEG = 0;
        private const int OFF_TX_READY = OFF_MARKER_BEG + MARKER_SIZE;
        private const int OFF_TX_LEN = OFF_TX_READY + WORD_SIZE;
        private const int OFF_TX_BUF = OFF_TX_LEN + WORD_SIZE;
        private const int TX_REGION_SIZE = WORD_SIZE + WORD_SIZE + SHM_BUF_SIZE;

        private const int OFF_RX_READY = OFF_TX_BUF + SHM_BUF_SIZE;
        private const int OFF_RX_LEN = OFF_RX_READY + WORD_SIZE;
        private const int OFF_RX_BUF = OFF_RX_LEN + WORD_SIZE;
        private const int OFF_MARKER_END = OFF_RX_BUF + SHM_BUF_SIZE;

        private readonly K.SafeHPROCESS _proc;
        private readonly IntPtr _baseAddr;

        public const int TOTAL_SIZE = 592;

        public MemShim(K.SafeHPROCESS proc, IntPtr baseAddr)
        {
            _proc = proc;
            _baseAddr = baseAddr;

            PrintDump();
        }

		private void PrintDump()
		{
            var buffer = ReadBytes(0, TOTAL_SIZE);
            File.WriteAllBytes("ms1b.bin", buffer);
            File.WriteAllText("ms1b.txt", TextExt.ToHexString(buffer));

            var array = buffer.SwapEndian();
            File.WriteAllBytes("ms1l.bin", array);
            File.WriteAllText("ms1l.txt", TextExt.ToHexString(array));
		}

		public int Read(byte[] buffer, int offset, int count)
        {
            var region = ReadBytes(OFF_TX_READY, TX_REGION_SIZE);
            var ready = Ends.ToUInt16(region, 0, true);
            if (ready == 0)
                return 0;

            var len = Ends.ToUInt16(region, WORD_SIZE, true);
            var got = Math.Min(len, Math.Min(count, SHM_BUF_SIZE));
            Array.Copy(region, WORD_SIZE * 2, buffer, offset, got);

            WriteWord(OFF_TX_READY, 0);
            return got;
        }

        public bool Write(byte[] buffer, int offset, int count)
        {
            if (ReadWord(OFF_RX_READY) != 0)
                return false;

            var len = Math.Min(count, SHM_BUF_SIZE);
            var payload = new byte[SHM_BUF_SIZE];
            Array.Copy(buffer, offset, payload, 0, len);

            WriteBytes(OFF_RX_BUF, payload);
            WriteWord(OFF_RX_LEN, (ushort)len);
            WriteWord(OFF_RX_READY, 1);
            return true;
        }

        private ushort ReadWord(int offset)
        {
            var buf = ReadBytes(offset, WORD_SIZE);
            return Ends.ToUInt16(buf, 0, true);
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

        private void WriteWord(int offset, ushort value)
        {
            var buf = Ends.GetBytes(value, true);
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