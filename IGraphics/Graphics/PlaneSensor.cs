using IGraphics.Mathematics;

namespace IGraphics.Graphics
{
    public class PlaneSensor : ISensor
    {
        public PlaneSensor(Vector3D planeNormal)
        {
            PlaneNormal = planeNormal;
        }

        public Vector3D PlaneNormal { get; }
    }
}
