using IGraphics.LogicViewing;

namespace RetroGraph.Models.Extensions
{
    public static class SelectEventDtoExtension
    {
        public static SelectEvent ToSelectEvent(this SelectEventDto selectEventDto)
        {
            var selectEvent = new SelectEvent
            {
                selectPositionX = selectEventDto.selectPositionX,
                selectPositionY = selectEventDto.selectPositionY,
                Camera = selectEventDto.camera.ToCamera(),
                CanvasWidth = selectEventDto.canvasWidth,
                CanvasHeight = selectEventDto.canvasHeight
            };

            return selectEvent;
        }
    }
}
