using IGraphics.Graphics;

namespace IGraphics.LogicViewing.Services
{
    public interface IMoveSensorProcessor
    {
        bool Process(ISensor sensor, MoveState moveState);
    }
}
