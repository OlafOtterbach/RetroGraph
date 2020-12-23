using System;

namespace IGraphics.Mathmatics
{
    public static class Vector2DMath
    {
        public static double Length(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }
    }
}
