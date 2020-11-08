using RetroGraph.Application.Mathmatics;
using RetroGraph.Application.Graphics.Creators.Builder;
using System.Collections.Generic;
using System.Linq;

namespace RetroGraph.Application.Graphics.Graphics.Creators
{
    public static class Floor
    {
        public static Body Create(int count, double size)
        {
            IBeginFace beginFace = GraphicsBuilder.Definition.BeginBody;
            IBeginFaceAndEndBody beginFaceAndEndBody = null;

            var positions = CreatePositions(count, size);
            for(var y = 0; y < count; y++)
            {
                for (var x = 0; x < count; x++)
                {
                    var p1 = positions[x][y];
                    var p2 = positions[x][y+1];
                    var p3 = positions[x+1][y+1];
                    var p4 = positions[x+1][y];
                    beginFaceAndEndBody = beginFaceAndEndBody == null ? CreateFace(beginFace, p1, p2, p3, p4) : CreateFace(beginFaceAndEndBody, p1, p2, p3, p4);
                }
            }

            var body = beginFaceAndEndBody.EndBody.CreateBody();

            return body;
        }

        private static IBeginFaceAndEndBody CreateFace(IBeginFace beginFace, Position3D p1, Position3D p2, Position3D p3, Position3D p4)
        {
            return beginFace.BeginFace
                                .HasBorder
                                .BeginTriangle
                                    .P1(p1)
                                    .P2(p2)
                                    .P3(p4)
                                .EndTriangle
                                .BeginTriangle
                                    .P1(p3)
                                    .P2(p4)
                                    .P3(p2)
                                .EndTriangle
                            .EndFace;
        }

        private static IBeginFaceAndEndBody CreateFace(IBeginFaceAndEndBody beginFace, Position3D p1, Position3D p2, Position3D p3, Position3D p4)
        {
            return beginFace.BeginFace
                                .HasBorder
                                .BeginTriangle
                                    .P1(p1)
                                    .P2(p2)
                                    .P3(p4)
                                .EndTriangle
                                .BeginTriangle
                                    .P1(p3)
                                    .P2(p4)
                                    .P3(p2)
                                .EndTriangle
                            .EndFace;
        }

        private static Position3D[][] CreatePositions(int count, double size)
        {
            return CreatePositionGrid(count, size).ToArray();
        }

        private static IEnumerable<Position3D[]> CreatePositionGrid(int count, double size)
        {
            var center = count * size / 2.0;
            for (var y = 0; y <= count; y++)
            {
                yield return CreatePositionLine(y * size - center, center, count, size).ToArray();
            }
        }

        private static IEnumerable<Position3D> CreatePositionLine(double ypos, double center, int count, double size)
        {
            for (var x = 0; x <= count; x++)
            {
                yield return new Position3D(x * size - center, ypos, 0.0);
            }
        }
    }
}
