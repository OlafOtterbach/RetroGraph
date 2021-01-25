using IGraphics.Graphics.Creators.Creator;
using IGraphics.Mathmatics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGraphics.Graphics.Creators
{
    public static class Oval
    {
        public static Body Create(
            int segments1,
            int segments2,
            double radius1,
            double radius2,
            bool hasBorder1,
            bool hasBorder2,
            bool facetted1,
            bool facetted2,
            double length,
            double depth,
            Matrix44D originFrame)
        {
            var creator = new GraphicsCreator();
            creator.SetOriginFrame(originFrame);
            var (topLoop1, topLoop2) = CreateTopBottomFace(segments1, segments2, radius1, radius2, length, depth, depth, creator, true);
            var (bottomLoop1, bottomLoop2) = CreateTopBottomFace(segments1, segments2, radius1, radius2, length, depth, 0.0, creator, false);
            CreateSideFace(topLoop1, topLoop2, bottomLoop1, bottomLoop2, hasBorder1, hasBorder2, facetted1, facetted2, creator);

            var body = creator.CreateBody();
            return body;
        }

        private static (List<Position3D>, List<Position3D>) CreateTopBottomFace(int segments1, int segments2, double radius1, double radius2, double length, double depth, double z, GraphicsCreator creator, bool isTop)
        {
            var startAngle = Math.Acos((radius1 - radius2) / length);
            var endAngle = (-startAngle).Modulo2Pi();
            var alpha1 = (endAngle - startAngle) / segments1;
            var sign = isTop ? 1.0 : -1.0;

            creator.AddFace(true, false);
            var pointsRadius1 = new List<Position3D>();
            for (int i = 0; i < segments1; i++)
            {
                double x0 = (Math.Cos(i * alpha1 + startAngle) * radius1);
                double y0 = sign * (Math.Sin(i * alpha1 + startAngle) * radius1);
                double x1 = (Math.Cos(((i + 1) * alpha1 + startAngle).Modulo2Pi()) * radius1);
                double y1 = sign * (Math.Sin(((i + 1) * alpha1 + startAngle).Modulo2Pi()) * radius1);
                double x2 = 0.0;
                double y2 = 0.0;
                var p1 = new Position3D(x0, y0, z);
                var p2 = new Position3D(x1, y1, z);
                var p3 = new Position3D(x2, y2, z);
                creator.AddTriangle(p1, p2, p3);
                pointsRadius1.Add(p1);
                if (i == segments1 - 1) pointsRadius1.Add(p2);
            }

            var alpha2 = (2.0 * Math.PI - (endAngle - startAngle)) / segments2;
            startAngle = endAngle;
            var pointsRadius2 = new List<Position3D>();
            for (int i = 0; i < segments2; i++)
            {
                double x0 = length + (Math.Cos(i * alpha2 + startAngle) * radius2);
                double y0 = sign * (Math.Sin(i * alpha2 + startAngle) * radius2);
                double x1 = length + (Math.Cos(((i + 1) * alpha2 + startAngle).Modulo2Pi()) * radius2);
                double y1 = sign * (Math.Sin(((i + 1) * alpha2 + startAngle).Modulo2Pi()) * radius2);
                double x2 = length;
                double y2 = 0.0;
                var p1 = new Position3D(x0, y0, z);
                var p2 = new Position3D(x1, y1, z);
                var p3 = new Position3D(x2, y2, z);
                creator.AddTriangle(p1, p2, p3);
                pointsRadius2.Add(p1);
                if (i == segments2 - 1) pointsRadius2.Add(p2);
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

            if (!isTop)
            {
                pointsRadius1.Reverse();
                pointsRadius2.Reverse();
            }

            return (pointsRadius1, pointsRadius2);
        }

        private static void CreateSideFace(
            List<Position3D> topLoop1,
            List<Position3D> topLoop2,
            List<Position3D> bottomLoop1,
            List<Position3D> bottomLoop2,
            bool hasBorder1,
            bool hasBorder2,
            bool facetted1,
            bool facetted2,
            GraphicsCreator creator)
        {
            creator.AddFace(hasBorder1, facetted1);
            var n1 = topLoop1.Count;
            for (var i = 0; i < n1 - 1; i++)
            {
                var p1 = topLoop1[i];
                var p2 = bottomLoop1[i];
                var p3 = bottomLoop1[(i + 1) % n1];
                var p4 = topLoop1[(i + 1) % n1];
                creator.AddTriangle(p1, p2, p3);
                creator.AddTriangle(p4, p1, p3);
            }

            creator.AddFace(hasBorder2, facetted2);
            var n2 = topLoop2.Count;
            for (var i = 0; i < n2 - 1; i++)
            {
                var p1 = topLoop2[i];
                var p2 = bottomLoop2[i];
                var p3 = bottomLoop2[(i + 1) % n2];
                var p4 = topLoop2[(i + 1) % n2];
                creator.AddTriangle(p1, p2, p3);
                creator.AddTriangle(p4, p1, p3);
            }

            creator.AddFace(false, false);
            var pkt1 = topLoop1.Last();
            var pkt2 = bottomLoop1.Last();
            var pkt3 = bottomLoop2.First();
            var pkt4 = topLoop2.First();
            creator.AddTriangle(pkt1, pkt2, pkt3);
            creator.AddTriangle(pkt4, pkt1, pkt3);

            creator.AddFace(false, false);
            pkt1 = topLoop2.Last();
            pkt2 = bottomLoop2.Last();
            pkt3 = bottomLoop1.First();
            pkt4 = topLoop1.First();
            creator.AddTriangle(pkt1, pkt2, pkt3);
            creator.AddTriangle(pkt4, pkt1, pkt3);
        }




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
            for (var i = 0; i < n; i++)
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
            if (isTop)
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

