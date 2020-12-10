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

        public static double VectorToAngle(double xpos, double ypos)
        {
            double alpha = 0.0;
            if ((Math.Abs(xpos) > ConstantsMath.Epsilon)
                || (Math.Abs(ypos) > ConstantsMath.Epsilon)
               )
            {
                double a = 0.0;
                if (xpos < 0.0)
                {
                    a = -xpos;
                }
                else
                {
                    a = xpos;
                }
                var cosAlpha = a / Math.Sqrt(xpos * xpos + ypos * ypos);
                alpha = Math.Acos(cosAlpha);
                if (xpos >= 0.0)
                {
                    if (ypos >= 0.0)
                    {
                        //alpha = alpha;
                    }
                    else
                    {
                        alpha = ConstantsMath.Pi2 - alpha; // 360° - m_alpha
                    }
                }
                else
                {
                    if (ypos >= 0.0)
                    {
                        alpha = ConstantsMath.Pi - alpha;  // 180° - m_alpha
                    }
                    else
                    {
                        alpha = ConstantsMath.Pi + alpha; // 180° + m_alpha
                    }
                }
            }
            else
            {
                alpha = 0.0;
            }
            return alpha;
        }
    }
}