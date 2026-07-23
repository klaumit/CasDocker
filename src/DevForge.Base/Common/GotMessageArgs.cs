using System;
using DevForge.Lib.Messages;

namespace DevForge.Lib.Common
{
	public sealed class GotMessageArgs : EventArgs
    {
        public DateTime Stamp { get; set; }
        public Message Message { get; set; }
    }
}