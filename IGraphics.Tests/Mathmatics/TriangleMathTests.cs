
using IGraphics.Mathematics;
using Xunit;

namespace IGraphics.Tests
{
    public class TriangleMathTests
    {
        [Fact]
        public void IsCounterClockwiseTest1()
        {
            var res = TriangleMath.IsCounterClockwise(10, 10, 10, 10, 0, 0);

        }
    }
}