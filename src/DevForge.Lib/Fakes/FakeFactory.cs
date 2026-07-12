using System;
using DevForge.Lib.API;
using DevForge.Lib.Common;

namespace DevForge.Lib.Fakes
{
    public sealed class FakeFactory : BaseFactory
    {
        public override ICommPort Create()
        {
            throw new NotImplementedException();
        }
    }
}