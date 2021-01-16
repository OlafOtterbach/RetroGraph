namespace IGraphics.Mathmatics.Extensions
{
    public static class CardanFrameExtensions
    {
        public static Matrix44D ToMatrix44D(this CardanFrame eulerFrame)
        {
            var alphaRotation = Matrix44D.CreateRotation(new Vector3D(1.0, 0.0, 0.0), eulerFrame.AlphaAngleAxisX);
            var betaRotation = Matrix44D.CreateRotation(new Vector3D(0.0, 1.0, 0.0), eulerFrame.BetaAngleAxisY);
            var gammaRotation = Matrix44D.CreateRotation(new Vector3D(0.0, 0.0, 1.0), eulerFrame.GammaAngleAxisZ);
            var translation = Matrix44D.CreateTranslation(eulerFrame.Translation);

            var matrix = translation * gammaRotation * betaRotation * alphaRotation;

            return matrix;
        }
    }
}
