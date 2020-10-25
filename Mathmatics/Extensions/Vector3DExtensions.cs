namespace RetroGraph.Mathmatics.Extensions
{
    public static class Vector3DExtensions
    {
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
    }
}