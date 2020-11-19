using System;
using IGraphics.Graphics.Creators.Creator;
using IGraphics.Mathmatics;

namespace IGraphics.Graphics.Creators
{
    public static class RotationBody
    {
        public static Body Create(int circleSegments, double[][] shapeSections)
        {
            var creator = CreateGraphics(circleSegments, shapeSections);
            var body = creator.CreateBody();
            return body;
        }

        public static GraphicsCreator CreateGraphics(int circleSegments, double[][] shapeSections)
        {
            var creator = new GraphicsCreator();

            foreach (var section in shapeSections)
            {
                CreateSection(creator, circleSegments, section);
            }

            return creator;
        }

        private static void CreateSection(GraphicsCreator creator, int circleSegments, double[] section)
        {
            creator.AddFace(true);

            double alpha = 2.0 * Math.PI / circleSegments;
            var n = section.Length - 2;
            for (int j = 0; j < n; j += 2)
            {
                double radius0 = section[j];
                double radius1 = section[j + 2];
                double z0 = section[j + 1];
                double z1 = section[j + 3];
                for (int i = 0; i < circleSegments; i++)
                {
                    double x0 = (Math.Cos(i * alpha) * radius0);
                    double y0 = (Math.Sin(i * alpha) * radius0);
                    double x1 = (Math.Cos((i + 1) * alpha) * radius0);
                    double y1 = (Math.Sin((i + 1) * alpha) * radius0);
                    double x2 = (Math.Cos(i * alpha) * radius1);
                    double y2 = (Math.Sin(i * alpha) * radius1);
                    double x3 = (Math.Cos((i + 1) * alpha) * radius1);
                    double y3 = (Math.Sin((i + 1) * alpha) * radius1);

                    var p1 = new Position3D(x0, y0, z0);
                    var p2 = new Position3D(x1, y1, z0);
                    var p3 = new Position3D(x2, y2, z1);
                    var p4 = new Position3D(x3, y3, z1);

                    if (p1 != p2 && p2 != p4) creator.AddTriangle(p1, p2, p4);
                    if (p1 != p3 && p3 != p4) creator.AddTriangle(p4, p3, p1);
                }
            }
        }
    }
}
