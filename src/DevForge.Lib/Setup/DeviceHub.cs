using System;
using System.Threading.Tasks;
using DevForge.Lib.API;
using DevForge.Lib.Fakes;
using DevForge.Lib.Legacy;
using DevForge.Lib.Modern;
using DevForge.Lib.Messages;
using DevForge.Lib.Messages.Impl;

// ReSharper disable UseNullPropagation
// ReSharper disable ClassNeverInstantiated.Global

namespace DevForge.Lib.Common
{
    public sealed class DeviceHub : AbDeviceHub
    {
        public void StartLegacy()
        {
            Task.Factory.StartNew(DoLegacy);
        }

        public void StartModern()
        {
            Task.Factory.StartNew(DoModern);
        }

        private void DoLegacy()
        {
            var factory = new LegacyFactory();
            DoOnePort(factory);
        }

        private void DoModern()
        {
            var factory = new ModernFactory();
            DoOnePort(factory);
        }

        public void StartFake()
        {
            var factory = new FakePort();
            factory.Open();
            factory.WriteMessage(
                new Hello("app=Fake;cpu=X86;comm=Unknown;area=Unknown;ver=1972030723590103;chip=Unknown;mem=42000")
            );
            factory.Rewind();
            DoOnePort(factory);
        }
    }
}