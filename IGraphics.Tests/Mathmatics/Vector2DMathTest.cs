using IGraphics.Mathematics;
using System;
using Xunit;

namespace IGraphics.Tests.Mathematics
{
    public class Vector2DMathTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(10, Vector2DMath.Length(0, 0, 10, 0));
        }

        [Fact]
        public void Test2()
        {
            Assert.Equal(10, Vector2DMath.Length(0, 0, 0, 10));
        }

        [Fact]
        public void Test3()
        {
            Assert.Equal(2, Vector2DMath.Length(Math.Sqrt(2), Math.Sqrt(2), 2 * Math.Sqrt(2), 2 * Math.Sqrt(2)), 4);
        }
    }
}
