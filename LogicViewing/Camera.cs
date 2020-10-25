using RetroGraph.Mathmatics;

namespace RetroGraph.LogicViewing
{
    public class Camera
    {
        public double Distance => (Target - Frame.Offset).Length;

        public double NearPlane { get; set; }

        public Position3D Target { get; set; }

        public Matrix44D Frame { get; set; }
    }
}
