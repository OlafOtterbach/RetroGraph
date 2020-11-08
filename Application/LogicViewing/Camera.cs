using RetroGraph.Application.Mathmatics;

namespace RetroGraph.Application.LogicViewing
{
    public class Camera
    {
        public double Distance => (Target - Frame.Offset).Length;

        public double NearPlane { get; set; }

        public Position3D Target { get; set; }

        public Matrix44D Frame { get; set; }
    }
}
