using System;
using System.Text;
using System.Threading;
using E = DevForge.Lib.Modern.EnumDevNative;

// ReSharper disable UseCollectionExpression
// ReSharper disable InlineOutVariableDeclaration

namespace DevForge.Lib.Modern
{
    public sealed class UsbPort4
    {
        internal string ReadString(int maxLen = 64)
        {
            if (_usbHandle == null)
                return null;
            var handle = _usbHandle.Value;
            var buffer = new byte[maxLen];
            uint bytesRead;
            if (!E.PVReadUsb(handle, buffer, (uint)buffer.Length, out bytesRead))
                return null;
            var text = Encoding.ASCII.GetString(buffer, 0, (int)bytesRead);
            var res = text.Trim();
            return res;
        }
    }
}