using IGraphics.LogicViewing;

namespace RetroGraph.Models.Extensions
{
    public static class ZoomStateDtoExtension
    {
        public static ZoomState ToZoomState(this ZoomStateDto zoomStateDto)
        {
            var zoomState = new ZoomState
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