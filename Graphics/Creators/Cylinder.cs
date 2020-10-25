using RetroGraph.Mathmatics;
using RetroGraph.Graphics.Creators.Builder;
using System;

namespace RetroGraph.Graphics.Graphics.Creators
{
    public static class Cylinder
    {
        public static Body Create(int segments, double radius, double height)
        {
            double alpha = 2.0 * Math.PI / segments;
            double half = height / 2.0;
            double z0 = -half;
            double z1 = half;

            var builder = GraphicsBuilder.Definition.BeginBody.BeginFace as GraphicsBuilder;
            for (int i = 0; i < segments; i++)
            {
                    double x0 = (Math.Sin(i * alpha) * radius);
                double y0 = (Math.Cos(i * alpha) * radius);
                double x1 = (Math.Sin((i + 1) * alpha) * radius);
                double y1 = (Math.Cos((i + 1) * alpha) * radius);
                var p1 = new Position3D(x0, y0, z0);
                var p2 = new Position3D(x0, y0, z1);
                var p3 = new Position3D(x1, y1, z0);
                var p4 = new Position3D(x1, y1, z1);
                builder = builder
                    .BeginTriangle
                            .P1(p1)
                            .P2(p2)
                            .P3(p3)
                    .EndTriangle
                    .BeginTriangle
                            .P1(p3)
                            .P2(p2)
                            .P3(p4)
                    .EndTriangle as GraphicsBuilder;
            }
            builder = builder.EndFace.BeginFace.HasBorder as GraphicsBuilder;

            for (int i = 0; i < segments; i++)
            {
                double x0 = (Math.Sin(i * alpha) * radius);
                double y0 = (Math.Cos(i * alpha) * radius);
                double x1 = (Math.Sin(((i + 1) * alpha).Modulo2Pi()) * radius);
                double y1 = (Math.Cos(((i + 1) * alpha).Modulo2Pi()) * radius);
                double x2 = 0.0;
                double y2 = 0.0;
                var p1 = new Position3D(x0, y0, z1);
                var p2 = new Position3D(x1, y1, z1);
                var p3 = new Position3D(x2, y2, z1);
                builder = builder
                    .BeginTriangle
                            .P1(p1)
                            .P2(p3)
                            .P3(p2)
                    .EndTriangle as GraphicsBuilder;
            }
            builder = builder.EndFace.BeginFace.HasBorder as GraphicsBuilder;

            for (int i = 0; i < segments; i++)
            {
                double x0 = (Math.Sin(i * alpha) * radius);
                double y0 = (Math.Cos(i * alpha) * radius);
                double x1 = (Math.Sin(((i + 1) * alpha).Modulo2Pi()) * radius);
                double y1 = (Math.Cos(((i + 1) * alpha).Modulo2Pi()) * radius);
                double x2 = 0.0;
                double y2 = 0.0;
                var p1 = new Position3D(x0, y0, z0);
                var p2 = new Position3D(x1, y1, z0);
                var p3 = new Position3D(x2, y2, z0);
                builder = builder
                    .BeginTriangle
                            .P1(p1)
                            .P2(p2)
                            .P3(p3)
                    .EndTriangle as GraphicsBuilder;
            }

            var body = builder.EndFace.EndBody.CreateBody();
            return body;
        }
    }
}