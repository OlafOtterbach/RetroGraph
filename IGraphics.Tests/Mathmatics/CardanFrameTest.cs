﻿using IGraphics.Mathmatics;
using Xunit;

namespace IGraphics.Tests.Mathmatics
{
    public class CardanFrameTest
    {
        [Fact]
        public void ConstructorTest1()
        {
            var cardan = new CardanFrame(new Position3D(10, 20, 30), 10.0.DegToRad(), 20.0.DegToRad(), 30.0.DegToRad());

            Assert.Equal(new Position3D(10, 20, 30), cardan.Offset);
            Assert.Equal(new Vector3D(10, 20, 30), cardan.Translation);
            Assert.Equal(10.0.DegToRad(), cardan.AlphaAngleAxisX, 4);
            Assert.Equal(20.0.DegToRad(), cardan.BetaAngleAxisY, 4);
            Assert.Equal(30.0.DegToRad(), cardan.GammaAngleAxisZ, 4);
        }

        [Fact]
        public void ConstructorTest2()
        {
            var cardan = new CardanFrame(10.0.DegToRad(), 20.0.DegToRad(), 30.0.DegToRad());

            Assert.Equal(new Position3D(0, 0, 0), cardan.Offset);
            Assert.Equal(new Vector3D(0, 0, 0), cardan.Translation);
            Assert.Equal(10.0.DegToRad(), cardan.AlphaAngleAxisX, 4);
            Assert.Equal(20.0.DegToRad(), cardan.BetaAngleAxisY, 4);
            Assert.Equal(30.0.DegToRad(), cardan.GammaAngleAxisZ, 4);
        }

        [Fact]
        public void ConstructorTest3()
        {
            var original = new CardanFrame(new Position3D(10, 20, 30), 10.0.DegToRad(), 20.0.DegToRad(), 30.0.DegToRad());
            var cardan = new CardanFrame(original);

            Assert.Equal(new Position3D(10, 20, 30), cardan.Offset);
            Assert.Equal(new Vector3D(10, 20, 30), cardan.Translation);
            Assert.Equal(10.0.DegToRad(), cardan.AlphaAngleAxisX, 4);
            Assert.Equal(20.0.DegToRad(), cardan.BetaAngleAxisY, 4);
            Assert.Equal(30.0.DegToRad(), cardan.GammaAngleAxisZ, 4);
        }

        [Fact]
        public void EqualsTest()
        {
            var original = new CardanFrame(new Position3D(10, 20, 30), 10.0.DegToRad(), 20.0.DegToRad(), 30.0.DegToRad());
            var cardan = new CardanFrame(new Position3D(10, 20, 30), 10.0.DegToRad(), 20.0.DegToRad(), 30.0.DegToRad());
            object originalObject = original;
            object cardanObject = cardan;

            Assert.True(original == cardan);
            Assert.True(cardan == original);
            Assert.True(original.Equals(cardan));
            Assert.True(originalObject.Equals(cardanObject));
        }

        [Fact]
        public void GetHashCodeTest1()
        {
            var original = new CardanFrame(new Position3D(10, 20, 30), 10.0.DegToRad(), 20.0.DegToRad(), 30.0.DegToRad());
            var cardan = new CardanFrame(new Position3D(10, 20, 30), 10.0.DegToRad(), 20.0.DegToRad(), 30.0.DegToRad());

            Assert.Equal(original.GetHashCode(), cardan.GetHashCode());
        }
    }
}
