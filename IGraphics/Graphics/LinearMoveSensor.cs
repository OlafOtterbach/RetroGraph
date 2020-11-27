using IGraphics.Mathmatics;

namespace IGraphics.Graphics
{
    public class LinearMoveSensor : ISensor
    {
        public LinearMoveSensor(Vector3D axis)
        {
            Axis = axis;
        }

        public Vector3D Axis { get; }
    }
}
