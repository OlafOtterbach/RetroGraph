using IGraphics.LogicViewing;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;

namespace RetroGraph.Services
{
    public class ConverterToSelectState : IConverterToSelectState
    {
        public SelectState Convert(SelectStateDto selectStateDto)
        {
            var selectState = new SelectState()
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
