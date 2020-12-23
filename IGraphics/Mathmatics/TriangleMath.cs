namespace IGraphics.Mathmatics
{
    public static class TriangleMath
    {
        public static bool IsCounterClockwise(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return GetTriangleSpin(x1, y1, x2, y2, x3, y3) != TriangleSpin.clockwise;
        }

        private static TriangleSpin GetTriangleSpin(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var area = 0.5 * (x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2));
            if (area < ConstantsMath.Epsilon && area > -ConstantsMath.Epsilon) return TriangleSpin.no_clockwise;
            if (area > 0.0) return TriangleSpin.counter_clockwise;
            return TriangleSpin.clockwise;
        }

        private enum TriangleSpin
        {
            no_clockwise = 0,
            clockwise = -1,
            counter_clockwise = 1,
        }
    }
}
