using IGraphics.Graphics.Creators.Creator;
using IGraphics.Mathematics;

namespace IGraphics.Graphics.Creators
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

            var creator = new GraphicsCreator();

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

            // Top
            creator.AddFace(true, false);
            creator.AddTriangle(p4, p3, p7);
            creator.AddTriangle(p7, p8, p4);

            // Bottom
            creator.AddFace(true, false);
            creator.AddTriangle(p2, p1, p5);
            creator.AddTriangle(p5, p6, p2);

            var body = creator.CreateBody();

            return body;
        }
    }
}