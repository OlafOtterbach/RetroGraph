using IGraphics.LogicViewing;

namespace RetroGraph.Models.Extensions
{
    public static class ZoomEventDtoExtension
    {
        public static ZoomEvent ToZoomEvent(this ZoomEventDto zoomEventDto)
        {
            var zoomEvent = new ZoomEvent
            {
                Delta = zoomEventDto.delta,
                Camera = zoomEventDto.camera.ToCamera(),
                CanvasWidth = zoomEventDto.canvasWidth,
                CanvasHeight = zoomEventDto.canvasHeight
            };

            return zoomEvent;
        }
    }
}