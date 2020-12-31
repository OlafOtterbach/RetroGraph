using IGraphics.LogicViewing;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;

namespace RetroGraph.Services
{
    public class ConverterToZoomState : IConverterToZoomState
    {
        public ZoomState Convert(ZoomStateDto zoomStateDto)
        {
            var zoomState = new ZoomState()
            {
                Delta = zoomStateDto.delta,
                Camera = zoomStateDto.camera.ToCamera(),
                CanvasWidth = zoomStateDto.canvasWidth,
                CanvasHeight = zoomStateDto.canvasHeight
            };

            return zoomState;
        }
    }
}
