using RetroGraph.Application.Mathmatics;
using RetroGraph.Application.Graphics.Graphics;

namespace RetroGraph.Application.Graphics.Creators.Builder
{
    public interface IGraphicsBuilder
    {
        IBeginFace BeginBody { get; }
        IBeginTriangleOrHasBorder BeginFace { get; }
        IBeginTriangleAndEndFace EndTriangle { get; }
        IAddPoint1 BeginTriangle { get; }

        IAddPoint2 P1(double x, double y, double z);

        IAddPoint2 P1(Position3D p1);

        IAddPoint3 P2(double x, double y, double z);

        IAddPoint3 P2(Position3D p2);

        IEndTriangle P3(double x, double y, double z);

        IEndTriangle P3(Position3D p3);

        IBeginFaceAndEndBody EndFace { get; }
        ICreateBody EndBody { get; }

        Body CreateBody();
    }
}