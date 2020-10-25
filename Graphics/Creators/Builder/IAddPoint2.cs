using RetroGraph.Mathmatics;

namespace RetroGraph.Graphics.Creators.Builder
{
    public interface IAddPoint2
    {
        IAddPoint3 P2(Position3D p2);

        IAddPoint3 P2(double x, double y, double z);
    }
}