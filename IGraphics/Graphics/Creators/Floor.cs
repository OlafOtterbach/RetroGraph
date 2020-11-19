using IGraphics.Graphics.Creators.Creator;
using IGraphics.Mathmatics;
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
                    var p1 = positions[x][y];
                    var p2 = positions[x][y + 1];
                    var p3 = positions[x + 1][y + 1];
                    var p4 = positions[x + 1][y];
                    creator.AddFace(true);
                    creator.AddTriangle(p1, p2, p4);
                    creator.AddTriangle(p3, p4, p2);
                }
            }

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