using System;

namespace RetroGraph.Application.Mathmatics.Extensions
{
    public static class MathExtensions
    {
        public static bool EqualsTo(this double left, double right, double epsilon = ConstantsMath.Epsilon)
        {
            return (Math.Abs(left - right) < epsilon);
        }

        public static bool NotEqualsTo(this double left, double right, double epsilon = ConstantsMath.Epsilon)
        {
            return !left.EqualsTo(right, epsilon);
        }

        public static bool LessOrEqualsTo(this double first, double second, double epsilon = ConstantsMath.Epsilon)
        {
            bool isLessOrEqual = first < (second + epsilon);
            return isLessOrEqual;
        }

        public static bool LessTo(this double first, double second, double epsilon = ConstantsMath.Epsilon)
        {
            bool isLess = first <= (second - epsilon);
            return isLess;
        }

        public static bool GreaterOrEqualsTo(this double first, double second, double epsilon = ConstantsMath.Epsilon)
        {
            bool isGreaterOrEqual = LessOrEqualsTo(second, first, epsilon);
            return isGreaterOrEqual;
        }

        public static bool GreaterTo(this double first, double second, double epsilon = ConstantsMath.Epsilon)
        {
            bool isGreater = LessTo(second, first, epsilon);
            return isGreater;
        }
    }
}
