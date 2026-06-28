using DevForge.Lib.Messages;
using DevForge.Lib.Messages.Impl;
using Newtonsoft.Json;
using Xunit;

namespace DevForge.Tests
{
    public class MsgTest
    {
        [Fact]
        public void TestHello() => TestSimple(new Hello("Test me with this!"));

        [Fact]
        public void TestQuit() => TestSimple(new Quit("No special reason."));

        private static string ToJson(Message obj) => JsonConvert.SerializeObject(obj);

        private static void TestSimple<T>(T im) where T : Message
        {
            using var cp = new FakePort();
            cp.Open();
            cp.WriteMessage(im);
            cp.Rewind();
            var om = cp.ReadMessage();
            var first = ToJson(im);
            var second = ToJson(om);
            Assert.Equal(first, second);
        }
    }
}