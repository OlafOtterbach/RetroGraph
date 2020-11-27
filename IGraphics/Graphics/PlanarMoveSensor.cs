using IGraphics.Mathmatics;

namespace IGraphics.Graphics
{
    public class PlanarMoveSensor : ISensor
    {
        public PlanarMoveSensor(Vector3D axisX, Vector3D axisY)
        {
            AxisX = axisX;
            AxisY = axisY;
        }

        public Vector3D AxisX { get; }
        public Vector3D AxisY { get; }
    }
}
