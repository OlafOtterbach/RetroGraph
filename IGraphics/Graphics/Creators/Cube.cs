using IGraphics.Graphics.Creators.Builder;
using IGraphics.Mathmatics;

namespace IGraphics.Graphics.Graphics.Creators
{
    public static class Cube
    {
        public static Body Create(double size)
        {
            size = size > 0 ? size / 2.0 : 0.5;

            var p1 = new Position3D(-size, -size, -size);
            var p2 = new Position3D(size, -size, -size);
            var p3 = new Position3D(size, -size, size);
            var p4 = new Position3D(-size, -size, size);

            var p5 = new Position3D(-size, size, -size);
            var p6 = new Position3D(size, size, -size);
            var p7 = new Position3D(size, size, size);
            var p8 = new Position3D(-size, size, size);

            var body = GraphicsBuilder
                .Definition
                    .BeginBody
                    .BeginFace
                        .HasBorder
                        // South
                        .BeginTriangle
                            .P1(p1)
                            .P2(p2)
                            .P3(p3)
                        .EndTriangle
                        .BeginTriangle
                            .P1(p3)
                            .P2(p4)
                            .P3(p1)
                        .EndTriangle
                    .EndFace
                    .BeginFace
                        .HasBorder
                        // East
                        .BeginTriangle
                            .P1(p2)
                            .P2(p6)
                            .P3(p7)
                        .EndTriangle
                        .BeginTriangle
                            .P1(p7)
                            .P2(p3)
                            .P3(p2)
                        .EndTriangle
                    .EndFace
                    .BeginFace
                        .HasBorder
                        // North
                        .BeginTriangle
                            .P1(p6)
                            .P2(p5)
                            .P3(p8)
                        .EndTriangle
                        .BeginTriangle
                            .P1(p8)
                            .P2(p7)
                            .P3(p6)
                        .EndTriangle
                    .EndFace
                    .BeginFace
                        .HasBorder
                        // West
                        .BeginTriangle
                            .P1(p5)
                            .P2(p1)
                            .P3(p4)
                        .EndTriangle
                        .BeginTriangle
                            .P1(p4)
                            .P2(p8)
                            .P3(p5)
                        .EndTriangle
                    .EndFace
                    .BeginFace
                        .HasBorder
                        // Top
                        .BeginTriangle
                           .P1(p4)
                           .P2(p3)
                           .P3(p7)
                        .EndTriangle
                        .BeginTriangle
                           .P1(p7)
                           .P2(p8)
                           .P3(p4)
                        .EndTriangle
                    .EndFace
                    .BeginFace
                        .HasBorder
                        // Bottom
                        .BeginTriangle
                           .P1(p2)
                           .P2(p1)
                           .P3(p5)
                        .EndTriangle
                        .BeginTriangle
                           .P1(p5)
                           .P2(p6)
                           .P3(p2)
                        .EndTriangle
                    .EndFace
                .EndBody
                .CreateBody();

            return body;
        }
    }
}