using IGraphics.Mathematics;

namespace IGraphics.Graphics
{
    public class LinearSensor : ISensor
    {
        public LinearSensor(Vector3D axis)
        {
            Axis = axis;
        }

        public Vector3D Axis { get; }
    }
}
