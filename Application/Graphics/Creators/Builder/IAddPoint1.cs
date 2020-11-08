using RetroGraph.Application.Mathmatics;

namespace RetroGraph.Application.Graphics.Creators.Builder
{
    public interface IAddPoint1
    {
        IAddPoint2 P1(Position3D p1);

        IAddPoint2 P1(double x, double y, double z);
    }
}