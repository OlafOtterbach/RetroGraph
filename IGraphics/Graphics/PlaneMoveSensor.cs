using IGraphics.Mathmatics;

namespace IGraphics.Graphics
{
    public class PlaneMoveSensor : ISensor
    {
        public PlaneMoveSensor(Vector3D planeNormal)
        {
            PlaneNormal = planeNormal;
        }

        public Vector3D PlaneNormal { get; }
    }
}
