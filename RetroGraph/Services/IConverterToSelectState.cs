using IGraphics.LogicViewing;
using RetroGraph.Models;

namespace RetroGraph.Services
{
    public interface IConverterToSelectState
    {
        SelectState Convert(SelectStateDto selectStateDto);
    }
}
