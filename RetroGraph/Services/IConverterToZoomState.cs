using IGraphics.LogicViewing;
using RetroGraph.Models;

namespace RetroGraph.Services
{
    public interface IConverterToZoomState
    {
        ZoomState Convert(ZoomStateDto zoomStateDto);
    }
}
