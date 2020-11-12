using IGraphics.Graphics;
using IGraphics.Mathmatics;

namespace IHiddenLineGraphics.Model
{
    public class EdgeHL
    {
        public Position3D Start { get; set; }
        public Position3D End { get; set; }
        public TriangleHL First { get; set; }
        public TriangleHL Second { get; set; }

        public Edge Edge { get; set; }
    }
}