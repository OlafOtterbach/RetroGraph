using IGraphics.LogicViewing;

namespace RetroGraph.Models.Extensions
{
    public static class SelectStateDtoExtension
    {
        public static SelectState ToSelectState(this SelectStateDto selectStateDto)
        {
            var selectState = new SelectState
            {
                selectPositionX = selectStateDto.selectPositionX,
                selectPositionY = selectStateDto.selectPositionY,
                Camera = selectStateDto.camera.ToCamera(),
                CanvasWidth = selectStateDto.canvasWidth,
                CanvasHeight = selectStateDto.canvasHeight
            };

            return selectState;
        }
    }
}
