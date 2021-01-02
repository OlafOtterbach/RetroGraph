using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.LogicViewing.Services;
using IGraphics.Mathmatics;
using System;

namespace IGraphics.LogicViewing
{
    public class LogicView : ILogicView
    {
        private IMoveSensorProcessor _moveSensorProcessors;

        public LogicView(Scene scene, IMoveSensorProcessor moveSensorProcessor)
        {
            Scene = scene;
            _moveSensorProcessors = moveSensorProcessor;
        }

        public Scene Scene { get; }

        public SelectedBodyState SelectBody(SelectState selectState)
        {
            var posScene = ViewProjection.ProjectCanvasToSceneSystem(selectState.selectPositionX, selectState.selectPositionY, selectState.CanvasWidth, selectState.CanvasHeight, selectState.Camera.NearPlane, selectState.Camera.Frame);
            var rayOffset = selectState.Camera.Frame.Offset;
            var rayDirection = posScene - rayOffset;
            var (isIntersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);

            return new SelectedBodyState { SelectedBodyId = isIntersected ? body.Id : Guid.Empty, IsBodySelected = isIntersected, BodyIntersection = intersection };
        }

        public Camera Select(SelectState selectState)
        {
            var posScene = ViewProjection.ProjectCanvasToSceneSystem(selectState.selectPositionX, selectState.selectPositionY, selectState.CanvasWidth, selectState.CanvasHeight, selectState.Camera.NearPlane, selectState.Camera.Frame);
            var rayOffset = selectState.Camera.Frame.Offset;
            var rayDirection = posScene - rayOffset;

            var (isintersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);
            if (isintersected)
            {
                selectState.Camera.MoveTargetTo(intersection);
            }

            return selectState.Camera;
        }

        public Camera Move(MoveState moveState)
        {
            if (!_moveSensorProcessors.Process(moveState))
            {
                var deltaX = moveState.EndMoveX - moveState.StartMoveX;
                var deltaY = moveState.EndMoveY - moveState.StartMoveY;
                return Orbit(deltaX, deltaY, moveState.CanvasWidth, moveState.CanvasHeight, moveState.Camera);
            }

            return moveState.Camera;
        }

        public Camera Zoom(ZoomState zoomState)
        {
            var dy = zoomState.Delta * 1.0;
            zoomState.Camera.Zoom(dy);
            return zoomState.Camera;
        }

        private Camera Orbit(double pixelDeltaX, double pixelDeltaY, int canvasWidth, int canvasHeight, Camera camera)
        {
            var horicontalPixelFor360Degree = canvasWidth;
            var verticalPixelFor360Degree = canvasHeight;
            var alpha = -(360.0 * pixelDeltaX / horicontalPixelFor360Degree).DegToRad();
            var beta = -(360.0 * pixelDeltaY / verticalPixelFor360Degree).DegToRad();
            camera.OrbitXY(alpha);
            camera.OrbitYZ(beta);
            return camera;
        }
    }
}