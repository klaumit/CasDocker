using DevForge.Lib.Hex;
using DevForge.Lib.Ponder;
using Xunit;

namespace DevForge.Tests
{
    public class AddrTest
    {
        [Fact]
        public void Test86Calc()
        {
            var range = Ranges.Create("102000", "102200");
            var count = 0;
            foreach (var addr in range.Iterate(64))
            {
                var first = addr.From86Address();
                var second = first.Get86Address();
                var third = second.From86Address();
                Assert.Equal(addr, second);
                Assert.Equal(first.ToString(), third.ToString());
                count++;
            }
            Assert.Equal(8, count);
        }
    }
}