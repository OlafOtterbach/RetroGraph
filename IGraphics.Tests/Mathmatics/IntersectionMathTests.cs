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

        [Fact]
        public void CalculatePerpendicularPointTest_WhenLineInPlaneXYAndPosition_ThenPlumpPointIsCalculated()
        {
            var position = new Position3D(100, 100, 200);

            var plump = position.CalculatePerpendicularPoint(new Position3D(), new Vector3D(1,1,0));

            var expected = new Position3D(100, 100, 0);
            Assert.Equal(expected, plump);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenLineInPlaneXZAndPosition_ThenPlumpPointIsCalculated()
        {
            var position = new Position3D(100, 200, 100);

            var plump = position.CalculatePerpendicularPoint(new Position3D(0, 0, 0), new Vector3D(1, 0, 1));

            var expected = new Position3D(100, 0, 100);
            Assert.Equal(expected, plump);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenLineInPlaneYZAndPosition_ThenPlumpPointIsCalculated()
        {
            var position = new Position3D(200, 100, 100);

            var plump = position.CalculatePerpendicularPoint(new Position3D(0, 0, 0), new Vector3D(0, 1, 1));

            var expected = new Position3D(0, 100, 100);
            Assert.Equal(expected, plump);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenTwoLinesInPararllelPlaneXY_ThenPlumpPointIsCalculated()
        {
            var baseOffset = new Position3D(10, 20, 0);
            var baseDirection = new Vector3D(1, 0, 0);
            var secondOffest = new Position3D(10, 20, 100);
            var secondDirection = new Vector3D(0, 1, 0);

            var (success, plumpOnBaseAxis) = IntersectionMath.CalculatePerpendicularPoint(baseOffset, baseDirection, secondOffest, secondDirection);

            Assert.True(success);
            Assert.Equal(baseOffset, plumpOnBaseAxis);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenTwoCrossingLines_ThenPlumpPointIsCrossPoint()
        {
            var baseOffset = new Position3D(0, 0, 100);
            var baseDirection = new Vector3D(1, 1, -1);
            var secondOffest = new Position3D(0, 0, -100);
            var secondDirection = new Vector3D(1, 1, 1);

            var (success, plumpOnBaseAxis) = IntersectionMath.CalculatePerpendicularPoint(baseOffset, baseDirection, secondOffest, secondDirection);

            Assert.True(success);
            Assert.Equal(new Position3D(100, 100, 0), plumpOnBaseAxis);
        }
    }
}