namespace IGraphics.Mathmatics.Extensions
{
    public static class Vector3DExtensions
    {
        public static Position3D ToPosition3D(this Vector3D vector)
        {
            return new Position3D(vector.X, vector.Y, vector.Z);
        }

        public static double SquaredLength(this Vector3D vector)
        {
            var squaredLength = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z;
            return squaredLength;
        }

        public static bool IsLinearTo(this Vector3D first, Vector3D second)
        {
            var normalFirst = first.Normalize();
            var normalSecond = second.Normalize();
            bool result = (normalFirst == normalSecond) || (normalFirst == (normalSecond * -1.0f));
            return result;
        }

        public static double CounterClockwiseAngleWith(this Vector3D first, Vector3D second)
        {
            double alpha = 0.0f;

            // Calculates coordinate system of t_vecA as X axis
            var ex = first.Normalize();
            var ez = ex & second.Normalize();
            var len = ez.Length;
            if (len > ConstantsMath.Epsilon)
            {
                ez = ez.Normalize();

                // Vectors are linear independent
                var ey = ez & ex;
                ey = ey.Normalize();

                // Transforms t_vecB to this coordinate system
                var origin = new Position3D(0.0f, 0.0f, 0.0f);
                var matrix = Matrix44D.CreateCoordinateSystem(origin, ex, ey, ez).Inverse();
                second = matrix * second;

                // Angle between t_vecA as X-Axis and t_vecB
                alpha = AngleMath.VectorToAngle(second.X, second.Y);
            }
            else
            {
                // Vectors lies on same line
                if (ex == second)
                {
                    // Vectors have same direction
                    alpha = 0.0f;
                }
                else
                {
                    // Vectors have opposite direction
                    alpha = ConstantsMath.Pi;
                }
            }

            return alpha;
        }
    }
}