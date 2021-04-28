using IGraphics.Graphics.Creators.Creator;
using IGraphics.Mathematics;
using System.Collections.Generic;
using System.Linq;

namespace IGraphics.Graphics.Creators
{
    public static class Floor
    {
        public static Body Create(int count, double size)
        {
            var creator = new GraphicsCreator();

            var positions = CreatePositions(count, size);
            for (var y = 0; y < count; y++)
            {
                for (var x = 0; x < count; x++)
                {
                    var point1 = positions[x][y];
                    var point2 = positions[x][y + 1];
                    var point3 = positions[x + 1][y + 1];
                    var point4 = positions[x + 1][y];
                    creator.AddFace(true, false);
                    creator.AddTriangle(point1, point2, point4);
                    creator.AddTriangle(point3, point4, point2);
                }
            }

            size = size * count;
            size = size > 0 ? size / 2.0 : 0.5;
            var hight = 2;

            var p1 = new Position3D(-size, -size, -hight);
            var p2 = new Position3D(size, -size, -hight);
            var p3 = new Position3D(size, -size, 0);
            var p4 = new Position3D(-size, -size, 0);

            var p5 = new Position3D(-size, size, -hight);
            var p6 = new Position3D(size, size, -hight);
            var p7 = new Position3D(size, size, 0);
            var p8 = new Position3D(-size, size, 0);

            // South
            creator.AddFace(true, false);
            creator.AddTriangle(p1, p2, p3);
            creator.AddTriangle(p3, p4, p1);

            // East
            creator.AddFace(true, false);
            creator.AddTriangle(p2, p6, p7);
            creator.AddTriangle(p7, p3, p2);

            // North
            creator.AddFace(true, false);
            creator.AddTriangle(p6, p5, p8);
            creator.AddTriangle(p8, p7, p6);

            // West
            creator.AddFace(true, false);
            creator.AddTriangle(p5, p1, p4);
            creator.AddTriangle(p4, p8, p5);

            // Bottom
            creator.AddFace(true, false);
            creator.AddTriangle(p2, p1, p5);
            creator.AddTriangle(p5, p6, p2);


            var body = creator.CreateBody();

            return body;
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