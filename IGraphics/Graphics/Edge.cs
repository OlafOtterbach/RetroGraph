namespace IGraphics.Graphics
{
    public class Edge
    {
        public Body Parent { get; set; }

        public Point3D Start => First.Start;
        public Point3D End => First.End;

        public CoEdge First { get; set; }

        public CoEdge Second { get; set; }
    }
}