using DevForge.Lib.API;
using DevForge.Lib.Messages;
using Newtonsoft.Json;
using Xunit;

namespace DevForge.Tests
{
    public class MsgTest
    {
        [Fact]
        public void TestSimple()
        {
            ICommPort cp = new FakePort();

            var im = new Hello();
            cp.WriteMessage(im);

            var om = cp.ReadMessage();
            Assert.Equal(ToJson(im), ToJson(om));
        }

        private static string ToJson(Message obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}