using IGraphics.Mathmatics;
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

        [Fact]
        public void Check2DLineWithLineTest1()
        {
            var (hasIntersection, ix, iy) = IntersectionMath.Check2DLineWithLine(0, 0, 10, 10, 0, 10, 10, 0);

            Assert.True(hasIntersection);
            Assert.Equal(5.0, ix, 2);
            Assert.Equal(5.0, iy, 2);
        }

        [Fact]
        public void Check2DLineWithLineTest2()
        {
            var (hasIntersection, ix, iy) = IntersectionMath.Check2DLineWithLine(0, 0, 10, 10, 0, 10, 4, 6);

            Assert.False(hasIntersection);
        }

        [Fact]
        public void Check2DLineWithLineTest3()
        {
            var (hasIntersection, ix, iy) = IntersectionMath.Check2DLineWithLine(0, 0, 10, 10, 0, 10, 5, 5);

            Assert.False(hasIntersection);
        }
    }
}