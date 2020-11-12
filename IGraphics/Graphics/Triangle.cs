using IGraphics.Mathmatics;

namespace IGraphics.Graphics
{
    public class Triangle
    {
        public Face ParentFace { get; set; }

        public Vertex P1 { get; set; }
        public Vertex P2 { get; set; }
        public Vertex P3 { get; set; }
        public Vector3D Normal { get; set; }

        public CoEdge[] CoEdges { get; set; }
    }
}