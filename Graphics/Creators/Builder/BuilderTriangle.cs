using RetroGraph.Mathmatics;

namespace RetroGraph.Graphics.Creators.Builder
{
    public struct BuilderTriangle
    {
        public BuilderFace Parent { get; set; }
        public Vector3D Normal { get; set; }
        public BuilderVertex P1 { get; set; }
        public BuilderVertex P2 { get; set; }
        public BuilderVertex P3 { get; set; }
    }
}