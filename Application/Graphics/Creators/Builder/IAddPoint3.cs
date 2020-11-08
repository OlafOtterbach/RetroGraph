using RetroGraph.Application.Mathmatics;

namespace RetroGraph.Application.Graphics.Creators.Builder
{
    public interface IAddPoint3
    {
        IEndTriangle P3(Position3D p3);

        IEndTriangle P3(double x, double y, double z);
    }
}