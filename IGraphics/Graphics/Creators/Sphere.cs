using System;
using System.Collections.Generic;

namespace IGraphics.Graphics.Creators
{
    public static class Sphere
    {
        public static Body Create(int circleSegments, double radius)
        {
            var segment = new List<double>();

            double alpha = Math.PI / circleSegments;
            for (int i = 0; i <= circleSegments; i++)
            {
                double x0 = (Math.Sin(i * alpha) * radius);
                double y0 = -(Math.Cos(i * alpha) * radius);
                segment.Add(x0);
                segment.Add(y0);
            }

            var segments = new double[][]
            {
                segment.ToArray()
            };

            var body = RotationBody.Create(circleSegments, segments);

            return body;
        }
    }
}
