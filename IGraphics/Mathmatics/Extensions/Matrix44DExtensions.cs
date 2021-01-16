using System;

namespace IGraphics.Mathmatics.Extensions
{
    public static class Matrix44DExtensions
    {
        public static CardanFrame ToCardanFrame(this Matrix44D matrix)
        {
            var ex = matrix.Ex;
            var ey = matrix.Ey;

            var gammaAngleAxisZ = AngleMath.VectorToAngle(ex.X, ex.Y);

            var rotationXY = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), -gammaAngleAxisZ);
            var ex1 = rotationXY * ex;
            var betaAngleAxisY = -AngleMath.VectorToAngle(ex1.X, ex1.Z);

            var rotationXZ = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), -betaAngleAxisY);
            var ey1 = rotationXZ * rotationXY * ey;
            var alphaAngleAxisX = AngleMath.VectorToAngle(ey1.Y, ey1.Z);

            return new CardanFrame(matrix.Translation, alphaAngleAxisX, betaAngleAxisY, gammaAngleAxisZ);
        }
    }
}
