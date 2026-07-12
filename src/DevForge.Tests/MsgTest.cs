using DevForge.Lib.Messages;
using DevForge.Lib.Messages.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit;

namespace DevForge.Tests
{
    public class MsgTest
    {
        [Fact]
        public void TestHello()
        {
            const string name = "TestMeWithThis!";
            TestSimple(new Hello($"app={name}"), out var m);
            Assert.Equal(name, m.AsInfo().App);
        }

        [Fact]
        public void TestQuit()
        {
            TestSimple(new Quit("No reason."), out var m);
            Assert.Equal(10, m.Text.Length);
        }

        [Fact]
        public void TestAlive()
        {
            TestSimple(new Alive("1E"), out var m);
            Assert.Equal(30, m.AsNumber());
        }

        [Fact]
        public void TestRead()
        {
            TestSimple(new Read("0330|03|6000|3000|0023|12345644"), out var m);
            Assert.Equal(0x330, m.AsBuff().Src);
            Assert.Equal(3, m.AsBuff().Bank);
            Assert.Equal(0x6000, m.AsBuff().Seg);
            Assert.Equal(0x3000, m.AsBuff().Off);
            Assert.Equal(0x23, m.AsBuff().Size);
            Assert.Equal(4, m.AsBuff().Bytes.Length);
        }

        private static string ToJson(Message obj)
            => JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
            });

        private static void TestSimple<T>(T im, out T om) where T : Message
        {
            using var cp = new FakePort();
            cp.Open();
            cp.WriteMessage(im);
            cp.Rewind();
            om = (T)cp.ReadMessage();
            var first = ToJson(im);
            var second = ToJson(om);
            Assert.Equal(first, second);
        }
    }
}