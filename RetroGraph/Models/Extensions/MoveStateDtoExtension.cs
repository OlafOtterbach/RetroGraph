using IGraphics.LogicViewing;
using IGraphics.Mathmatics;

namespace RetroGraph.Models.Extensions
{
    public static class MoveStateDtoExtension
    {
        public static MoveState ToMoveState(this MoveStateDto moveStateDto)
        {
            var moveState = new MoveState()
            {
                SelectedBodyId = moveStateDto.BodyId,
                BodyTouchPosition = new Position3D(moveStateDto.BodyIntersection.X, moveStateDto.BodyIntersection.Y, moveStateDto.BodyIntersection.Z),
                StartMoveX = moveStateDto.StartX,
                StartMoveY = moveStateDto.StartY,
                EndMoveX = moveStateDto.EndX,
                EndMoveY = moveStateDto.EndY,
                Camera = moveStateDto.Camera.ToCamera(),
                CanvasWidth = moveStateDto.CanvasWidth,
                CanvasHeight = moveStateDto.CanvasHeight,
            };

            return moveState;
        }
    }
}
