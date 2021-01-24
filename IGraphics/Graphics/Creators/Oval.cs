using IGraphics.Graphics.Creators.Creator;
using IGraphics.Mathmatics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGraphics.Graphics.Creators
{
    public static class Oval
    {
        public static Body Create(int segments, double radius1, double radius2, double length, double depth)
        {
            var creator = new GraphicsCreator();
            var topLoop = CreateTopBottomFace(segments, radius1, radius2, length, depth, depth, creator, true);
            var bottomLoop = CreateTopBottomFace(segments, radius1, radius2, length, depth, 0.0, creator, false);
            CreateSideFace(topLoop, bottomLoop, creator);

            var body = creator.CreateBody();
            return body;
        }

        private static void CreateSideFace(List<Position3D> topLoop, List<Position3D> bottomLoop, GraphicsCreator creator)
        {
            creator.AddFace(false, false);

            var n = topLoop.Count;
            for(var i = 0; i<n;i++)
            {
                var p1 = topLoop[i];
                var p2 = bottomLoop[i];
                var p3 = bottomLoop[(i + 1) % n];
                var p4 = topLoop[(i + 1) % n];
                creator.AddTriangle(p1, p2, p3);
                creator.AddTriangle(p4, p1, p3);
            }
        }

        private static List<Position3D> CreateTopBottomFace(int segments, double radius1, double radius2, double length, double depth, double z, GraphicsCreator creator, bool isTop)
        {
            var startAngle = Math.Acos((radius1 - radius2) / length);
            var endAngle = (-startAngle).Modulo2Pi();
            var alpha = (endAngle - startAngle) / segments;
            var sign = isTop ? 1.0 : -1.0;

            creator.AddFace(true, false);
            var pointsRadius1 = new List<Position3D>();
            for (int i = 0; i < segments; i++)
            {
                double x0 = (Math.Cos(i * alpha + startAngle) * radius1);
                double y0 = sign * (Math.Sin(i * alpha + startAngle) * radius1);
                double x1 = (Math.Cos(((i + 1) * alpha + startAngle).Modulo2Pi()) * radius1);
                double y1 = sign * (Math.Sin(((i + 1) * alpha + startAngle).Modulo2Pi()) * radius1);
                double x2 = 0.0;
                double y2 = 0.0;
                var p1 = new Position3D(x0, y0, z);
                var p2 = new Position3D(x1, y1, z);
                var p3 = new Position3D(x2, y2, z);
                creator.AddTriangle(p1, p2, p3);
                pointsRadius1.Add(p1);
                if (i == segments - 1) pointsRadius1.Add(p2);
            }

            alpha = (2.0 * Math.PI - (endAngle - startAngle)) / segments;
            startAngle = endAngle;
            var pointsRadius2 = new List<Position3D>();
            for (int i = 0; i < segments; i++)
            {
                double x0 = length + (Math.Cos(i * alpha + startAngle) * radius2);
                double y0 = sign * (Math.Sin(i * alpha + startAngle) * radius2);
                double x1 = length + (Math.Cos(((i + 1) * alpha + startAngle).Modulo2Pi()) * radius2);
                double y1 = sign * (Math.Sin(((i + 1) * alpha + startAngle).Modulo2Pi()) * radius2);
                double x2 = length;
                double y2 = 0.0;
                var p1 = new Position3D(x0, y0, z);
                var p2 = new Position3D(x1, y1, z);
                var p3 = new Position3D(x2, y2, z);
                creator.AddTriangle(p1, p2, p3);
                pointsRadius2.Add(p1);
                if (i == segments - 1) pointsRadius2.Add(p2);
            }

            var s1 = pointsRadius1.First();
            var e1 = pointsRadius1.Last();
            var m1 = new Position3D(0.0, 0.0, z);
            var s2 = pointsRadius2.First();
            var e2 = pointsRadius2.Last();
            var m2 = new Position3D(length, 0.0, z);

            creator.AddTriangle(s1, m1, e2);
            creator.AddTriangle(e2, m1, m2);
            creator.AddTriangle(m1, e1, m2);
            creator.AddTriangle(m2, e1, s2);

            List<Position3D> loop;
            if(isTop)
            {
                 loop = pointsRadius1.Concat(pointsRadius2).ToList();
            }
            else
            {
                pointsRadius1.Reverse();
                pointsRadius2.Reverse();
                loop = pointsRadius1.Concat(pointsRadius2).ToList();
            }

            return loop;
        }
    }
}
