using IGraphics.Mathmatics;
using System;
using Xunit;

namespace IGraphics.Tests
{
    public class IntersectionMathTests
    {
        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest1()
        {
            Assert.True(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 1, 10, 10, 1));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest2()
        {
            Assert.True(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 5-1, 5+1, 5+1, 5-1));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest3()
        {
            Assert.False(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 20, 10, 30, 1));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest4()
        {
            Assert.False(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 10, 10, 20, 1));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest5()
        {
            Assert.False(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 1, 30, 10, 20));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest6()
        {
            Assert.False(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 1, 20, 10, 10));
        }
    }
}