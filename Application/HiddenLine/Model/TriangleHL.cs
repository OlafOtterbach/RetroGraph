using RetroGraph.Application.Mathmatics;
using RetroGraph.Application.Graphics;

namespace RetroGraph.Application.HiddenLine.Model
{
    public class TriangleHL
    {
        public Vector3D Normal { get; set; }
        public Position3D P1 { get; set; }
        public Position3D P2 { get; set; }
        public Position3D P3 { get; set; }
        public TriangleSpin Spin { get; set; }

        public bool HasParentFaceBorder { get; set; }

        public Triangle Triangle { get; set; }
    }
}
