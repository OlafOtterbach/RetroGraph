using IGraphics.Mathematics.Extensions;
using System;

namespace IGraphics.Mathematics
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
            if ((absAlpha > ConstantsMath.Pi2) || (absAlpha.EqualsTo(ConstantsMath.Pi2)))
            {
                long count = (long)(absAlpha / ConstantsMath.Pi2);
                absAlpha = absAlpha - (count * ConstantsMath.Pi2);
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
                    alpha = ConstantsMath.Pi2 - absAlpha;
                }
            }

            return alpha;
        }

        public static double ToAngle(this (double X,double Y) vector)
        {
            var x = vector.X;
            var y = vector.Y;

            if (x.EqualsTo(0.0) && y.EqualsTo(0.0))
            {
                return 0.0;
            }
            {
                var alpha = Math.Atan2(y, x);
                alpha = alpha >= 0.0 ? alpha : ConstantsMath.Pi2 + alpha;
                return alpha;
            }
        }
    }
}