using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.LogicViewing.Services;
using IGraphics.Mathematics;
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

        public SelectedBodyState SelectBody(SelectEvent selectEvent)
        {
            var posScene = ViewProjection.ProjectCanvasToSceneSystem(selectEvent.selectPositionX, selectEvent.selectPositionY, selectEvent.CanvasWidth, selectEvent.CanvasHeight, selectEvent.Camera.NearPlane, selectEvent.Camera.Frame);
            var rayOffset = selectEvent.Camera.Frame.Offset;
            var rayDirection = posScene - rayOffset;
            var (isIntersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);

            return new SelectedBodyState { SelectedBodyId = isIntersected ? body.Id : Guid.Empty, IsBodySelected = isIntersected, BodyIntersection = intersection };
        }

        public Camera Touch(TouchEvent touchEvent)
        {
            if (touchEvent.IsBodyTouched)
            {
                touchEvent.Camera.MoveTargetTo(touchEvent.TouchPosition);
            }

            return touchEvent.Camera;
        }

        public Camera Select(SelectEvent selectEvent)
        {
            var posScene = ViewProjection.ProjectCanvasToSceneSystem(selectEvent.selectPositionX, selectEvent.selectPositionY, selectEvent.CanvasWidth, selectEvent.CanvasHeight, selectEvent.Camera.NearPlane, selectEvent.Camera.Frame);
            var rayOffset = selectEvent.Camera.Frame.Offset;
            var rayDirection = posScene - rayOffset;

            var (isintersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);
            if (isintersected)
            {
                selectEvent.Camera.MoveTargetTo(intersection);
            }

            return selectEvent.Camera;
        }

        public Camera Move(MoveEvent moveEvent)
        {
            if (!_moveSensorProcessors.Process(moveEvent))
            {
                var deltaX = moveEvent.EndMoveX - moveEvent.StartMoveX;
                var deltaY = moveEvent.EndMoveY - moveEvent.StartMoveY;
                return Orbit(deltaX, deltaY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera);
            }

            return moveEvent.Camera;
        }

        public Camera Zoom(ZoomEvent zoomEvent)
        {
            var dy = zoomEvent.Delta * 1.0;
            zoomEvent.Camera.Zoom(dy);
            return zoomEvent.Camera;
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