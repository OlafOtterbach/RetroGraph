using IGraphics.Mathematics;

namespace IGraphics.Graphics
{
    public class CylinderSensor : ISensor
    {
        public CylinderSensor(Vector3D axis)
        {
            Axis = axis;
        }

        public Vector3D Axis { get; }
    }

}
