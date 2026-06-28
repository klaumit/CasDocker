using DevForge.Lib.Messages;
using DevForge.Lib.Messages.Impl;
using Newtonsoft.Json;
using Xunit;

namespace DevForge.Tests
{
    public class MsgTest
    {
        [Fact]
        public void TestSimple()
        {
            using var cp = new FakePort();
            cp.Open();

            var im = new Hello("Test me with this!");
            cp.WriteMessage(im);

            cp.Rewind();
            var om = cp.ReadMessage();

            Assert.Equal(ToJson(im), ToJson(om));
        }

        private static string ToJson(Message obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}