using IGraphics.Graphics;
using IGraphics.LogicViewing;
using IGraphics.Mathmatics;
using IGraphics.Graphics.Services;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;

namespace RetroGraph.Services

{
    public class ConverterToMoveState : IConverterToMoveState
    {
        private Scene _scene;

        public ConverterToMoveState(Scene scene)
        {
            _scene = scene;
        }

        public MoveState Convert(MoveStateDto moveStateDto)
        {
            var camera = moveStateDto.camera.ToCamera();
            var startOffset = ViewProjection.ProjectCanvasToSceneSystem(moveStateDto.startX, moveStateDto.startY, moveStateDto.canvasWidth, moveStateDto.canvasHeight, camera.NearPlane, camera.Frame);
            var endOffset = ViewProjection.ProjectCanvasToSceneSystem(moveStateDto.endX, moveStateDto.endY, moveStateDto.canvasWidth, moveStateDto.canvasHeight, camera.NearPlane, camera.Frame);
            var offset = camera.Frame.Offset;
            var startDirection = startOffset - offset;
            var endDirection = endOffset - offset;
            var body = _scene.GetBody(moveStateDto.bodyId);

            var moveState = new MoveState()
            {
                SelectedBody = body,
                BodyIntersection = new Position3D(moveStateDto.bodyIntersection.X, moveStateDto.bodyIntersection.Y, moveStateDto.bodyIntersection.Z),
                StartMoveX = moveStateDto.startX,
                StartMoveY = moveStateDto.startY,
                StartMoveOffset = startOffset,
                StartMoveDirection = startDirection,
                EndMoveX = moveStateDto.endX,
                EndMoveY = moveStateDto.endY,
                EndMoveOffset = endOffset,
                EndMoveDirection = endDirection,
                Camera = moveStateDto.camera.ToCamera(),
                CanvasWidth = moveStateDto.canvasWidth,
                CanvasHeight = moveStateDto.canvasHeight,
            };

            return moveState;
        }
    }
}
