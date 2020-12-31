using IGraphics.LogicViewing;
using RetroGraph.Models;

namespace RetroGraph.Services
{
    public interface IConverterToMoveState
    {
        MoveState Convert(MoveStateDto moveStateDto);
    }
}
