namespace RetroGraph.HiddenLine.Model
{
    public class SceneHL
    {
        public double NearPlaneDistance { get; set; }
        public EdgeHL[] Edges { get; set; }
        public TriangleHL[] Triangles { get; set; }
    }
}
