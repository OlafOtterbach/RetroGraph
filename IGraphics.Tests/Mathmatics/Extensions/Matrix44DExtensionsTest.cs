using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using System;
using Xunit;

namespace IGraphics.Tests.Mathmatics.Extensions
{
   
   public class DMatrix4x4ExtensionsTest
   {
        [Fact]
        public void ToCardanTest1()
        {
            var cardanFrame = new CardanFrame(new Position3D(), 0.0.DegToRad(), 45.0.DegToRad(), 90.0.DegToRad());

            var mat = cardanFrame.ToMatrix44D();
            var ex1 = mat * new Vector3D(1, 0, 0);

            var cardan = mat.ToCardanFrame();
            var rot = cardan.ToMatrix44D();

            Assert.Equal(mat, rot);
        }

        [Fact]
        public void ToCardanTest2()
        {
            var cardanFrame = new CardanFrame(new Position3D(), 45.0.DegToRad(), 45.0.DegToRad(), 45.0.DegToRad());

            var mat = cardanFrame.ToMatrix44D();
            var ex1 = mat * new Vector3D(1, 0, 0);

            var cardan = mat.ToCardanFrame();
            var rot = cardan.ToMatrix44D();

            Assert.Equal(mat, rot);
        }

        [Fact]
        public void ToCardanTest3()
        {
            var cardanFrame = new CardanFrame(new Position3D(), 269.96510228042916.DegToRad(), 0.00030437000552928036.DegToRad(), 89.0006093665338.DegToRad());

            var mat = cardanFrame.ToMatrix44D();
            var ex1 = mat * new Vector3D(1, 0, 0);

            var cardan = mat.ToCardanFrame();
            var alpha = cardan.AlphaAngleAxisX.RadToDeg();
            var beta = cardan.BetaAngleAxisY.RadToDeg();
            var Gamma = cardan.GammaAngleAxisZ.RadToDeg();

            var rot = cardan.ToMatrix44D();

            Assert.Equal(mat, rot);
        }

        [Fact]
        public void ToCardanTest5()
        {
            const double epsilon = 0.0001;
            var helper = new CreateRandomMatrixHelper();

            for (int i = 0; i < 1000000; i++)
            {
                var matrix = helper.CreateFrame();
                var cardan = matrix.ToCardanFrame();
                var cardanMatrix = cardan.ToMatrix44D();

                var ex1 = matrix.Ex;
                var ex2 = cardanMatrix.Ex;
                Assert.True(Math.Abs(ex1.X - ex2.X) < epsilon);
                Assert.True(Math.Abs(ex1.Y - ex2.Y) < epsilon);
                Assert.True(Math.Abs(ex1.Z - ex2.Z) < epsilon);

                var ey1 = matrix.Ey;
                var ey2 = cardanMatrix.Ey;
                Assert.True(Math.Abs(ey1.X - ey2.X) < epsilon);
                Assert.True(Math.Abs(ey1.Y - ey2.Y) < epsilon);
                Assert.True(Math.Abs(ey1.Z - ey2.Z) < epsilon);

                var ez1 = matrix.Ez;
                var ez2 = cardanMatrix.Ez;
                Assert.True(Math.Abs(ez1.X - ez2.X) < epsilon);
                Assert.True(Math.Abs(ez1.Y - ez2.Y) < epsilon);
                Assert.True(Math.Abs(ez1.Z - ez2.Z) < epsilon);

                var offset1 = matrix.Offset;
                var offset2 = cardanMatrix.Offset;
                Assert.True(Math.Abs(offset1.X - offset2.X) < epsilon);
                Assert.True(Math.Abs(offset1.Y - offset2.Y) < epsilon);
                Assert.True(Math.Abs(offset1.Z - offset2.Z) < epsilon);
            }
        }
    }
}