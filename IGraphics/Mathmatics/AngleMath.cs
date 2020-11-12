using IGraphics.Mathmatics.Extensions;
using System;

namespace IGraphics.Mathmatics
{
    public static class AngleMath
    {
        public static double DegToRad(this double radAlpha)
        {
            var result = radAlpha * Math.PI / 180.0f;
            return result;
        }

        public static double RadToDeg(this double radAlpha)
        {
            var result = radAlpha * 180.0f / Math.PI;
            return result;
        }

        public static double Modulo2Pi(this double angle)
        {
            double alpha;

            // Seperates absolute value and sign
            var absAlpha = Math.Abs(angle);
            var signAlpha = (angle >= 0.0f);

            // Calculates modulo2Pi
            if ((absAlpha > ConstantsMath.PI2) || (absAlpha.EqualsTo(ConstantsMath.PI2)))
            {
                long count = (long)(absAlpha / ConstantsMath.PI2);
                absAlpha = absAlpha - (count * ConstantsMath.PI2);
                if (absAlpha < 0.0f)
                {
                    absAlpha = 0.0f;
                }
            }

            // Calculates positive angle
            if (signAlpha)
            {
                alpha = absAlpha;
            }
            else
            {
                if (absAlpha.EqualsTo(0.0))
                {
                    alpha = absAlpha;
                }
                else
                {
                    alpha = ConstantsMath.PI2 - absAlpha;
                }
            }

            return alpha;
        }
    }
}