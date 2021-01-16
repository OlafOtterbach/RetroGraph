using IGraphics.Mathmatics.Extensions;
using System;

namespace IGraphics.Mathmatics
{
    public struct CardanFrame : IEquatable<CardanFrame>
    {
        public CardanFrame(Position3D offset, double alphaAngleAxisX, double betaAngleAxisY, double gammaAngleAxisZ) : this()
        {
            Offset = offset;
            AlphaAngleAxisX = alphaAngleAxisX.Modulo2Pi();
            BetaAngleAxisY = betaAngleAxisY.Modulo2Pi();
            GammaAngleAxisZ = gammaAngleAxisZ.Modulo2Pi();
        }

        public CardanFrame(Vector3D translation, double alphaAngleAxisX, double betaAngleAxisY, double gammaAngleAxisZ) : this(translation.ToPosition3D(), alphaAngleAxisX, betaAngleAxisY, gammaAngleAxisZ)
        { }

        public CardanFrame(double alphaAngleAxisX, double betaAngleAxisY, double gammaAngleAxisZ) : this(new Position3D(), alphaAngleAxisX, betaAngleAxisY, gammaAngleAxisZ)
        { }

        public CardanFrame(CardanFrame other) : this(other.Offset, other.AlphaAngleAxisX, other.BetaAngleAxisY, other.GammaAngleAxisZ)
        { }

        public Position3D Offset { get; }
        public Vector3D Translation => Offset.ToVector3D();
        public double AlphaAngleAxisX { get; }
        public double BetaAngleAxisY { get; }
        public double GammaAngleAxisZ { get; }

        public override int GetHashCode()
        {
            return Translation.GetHashCode() * (int)AlphaAngleAxisX * (int)BetaAngleAxisY * (int)GammaAngleAxisZ;
        }

        public override bool Equals(Object obj)
        {
            return (obj is CardanFrame) && (this.Equals((CardanFrame)obj));
        }

        public bool Equals(CardanFrame other)
        {
            return (Translation == other.Translation)
                    && AlphaAngleAxisX.EqualsTo(other.AlphaAngleAxisX)
                    && BetaAngleAxisY.EqualsTo(other.BetaAngleAxisY)
                    && GammaAngleAxisZ.EqualsTo(other.GammaAngleAxisZ);
        }

        public static bool operator ==(CardanFrame first, CardanFrame second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(CardanFrame first, CardanFrame second)
        {
            return !(first.Equals(second));
        }
    }
}
