namespace RetroGraph.Application.Graphics
{
    public class CoEdge
    {
        public Triangle ParentTriangle { get; set; }
        public Point3D Start { get; set; }
        public Point3D End { get; set; }

        public int GetHashCode(Edge edge)
        {
            if (ReferenceEquals(edge, null)) return 0;

            int start = edge.Start.GetHashCode();
            int end = edge.End.GetHashCode();

            if (start > end)
            {
                (start, end) = (end, start);
            }

            return start ^ end;
        }
    }
}
