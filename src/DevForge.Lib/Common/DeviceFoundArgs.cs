using System;
using DevForge.Lib.API;
using DevForge.Lib.Messages.Impl;

namespace DevForge.Lib.Common
{
    public sealed class DeviceFoundArgs : EventArgs
    {
        public DateTime Stamp { get; set; }
        public ICommDevice Device { get; set; }
        public Hello Hello { get; set; }
    }
}