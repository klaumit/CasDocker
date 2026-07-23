using System;

// ReSharper disable UnusedMemberInSuper.Global

namespace DevForge.Lib.API
{
	public interface ICloseable : IDisposable
    {
        void Close();
    }
}