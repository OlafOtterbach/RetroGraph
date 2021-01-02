using IGraphics.LogicViewing;

namespace RetroGraph.Models.Extensions
{
    public static class TouchStateDtoExtension
    {
        public static TouchState ToTouchState(this TouchStateDto touchStateDto)
        {
            var touchState = new TouchState
            {
                IsBodyTouched = touchStateDto.IsBodyTouched,
                BodyId = touchStateDto.BodyId,
                TouchPosition = touchStateDto.TouchPosition.ToPosition3D(),
                Camera = touchStateDto.Camera.ToCamera(),
                CanvasWidth = touchStateDto.CanvasWidth,
                CanvasHeight = touchStateDto.CanvasHeight
            };
            return touchState;
        }
    }
}
