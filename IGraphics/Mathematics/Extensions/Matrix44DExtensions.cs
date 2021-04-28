using System;

namespace IGraphics.Mathematics.Extensions
{
    public static class Matrix44DExtensions
    {
        public static CardanFrame ToCardanFrame(this Matrix44D matrix)
        {
            var ex = matrix.Ex;
            var ey = matrix.Ey;

            var gammaAngleAxisZ = (ex.X, ex.Y).ToAngle();

            var rotationXY = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), -gammaAngleAxisZ);
            var ex1 = rotationXY * ex;
            var betaAngleAxisY = -(ex1.X, ex1.Z).ToAngle();

            var rotationXZ = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), -betaAngleAxisY);
            var ey1 = rotationXZ * rotationXY * ey;
            var alphaAngleAxisX = (ey1.Y, ey1.Z).ToAngle();

            return new CardanFrame(matrix.Translation, alphaAngleAxisX, betaAngleAxisY, gammaAngleAxisZ);
        }
    }
}
