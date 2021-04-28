using IGraphics.LogicViewing;
using IGraphics.Mathematics;

namespace RetroGraph.Models.Extensions
{
    public static class MoveEventDtoExtension
    {
        public static MoveEvent ToMoveEvent(this MoveEventDto moveEventDto)
        {
            var moveEvent = new MoveEvent()
            {
                SelectedBodyId = moveEventDto.BodyId,
                BodyTouchPosition = new Position3D(moveEventDto.BodyIntersection.X, moveEventDto.BodyIntersection.Y, moveEventDto.BodyIntersection.Z),
                StartMoveX = moveEventDto.StartX,
                StartMoveY = moveEventDto.StartY,
                EndMoveX = moveEventDto.EndX,
                EndMoveY = moveEventDto.EndY,
                Camera = moveEventDto.Camera.ToCamera(),
                CanvasWidth = moveEventDto.CanvasWidth,
                CanvasHeight = moveEventDto.CanvasHeight,
            };

            return moveEvent;
        }
    }
}
