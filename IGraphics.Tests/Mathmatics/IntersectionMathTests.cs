using IGraphics.Mathmatics;
using System;
using Xunit;

namespace IGraphics.Tests
{
    public class IntersectionMathTests
    {
        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest()
        {
            Assert.True(IntersectionMath.AreLinesBoundedBoxesOverlapped(1,1, 10,10, 1,10, 10,1));
        }
    }
}