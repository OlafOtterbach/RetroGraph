using IGraphics.Graphics;

namespace IGraphics.LogicViewing.Services
{
    public interface IMoveSensorService
    {
        bool CanProcess(ISensor sensor);

        void Process(ISensor sensor, MoveEvent moveEvent);

    }
}
