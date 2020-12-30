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

        public Guid SelectBody(double canvasX, double canvasY, int canvasWidth, int canvasHeight, Camera camera)
        {
            var (x, y) = ViewProjection.ProjectCanvasToCameraPlane(canvasX, canvasY, canvasWidth, canvasHeight);
            var posCameraSystem = ViewProjection.ProjectCameraPlaneToCameraSystem(x, y, camera.NearPlane);
            var posScene = ViewProjection.ProjectCameraSystemToSceneSystem(posCameraSystem, camera.Frame);

            var rayOffset = camera.Frame.Offset;
            var rayDirection = posScene - rayOffset;
            var (isintersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);

            return body != null ? body.Id : Guid.Empty;
        }

        public Camera Select(double canvasX, double canvasY, double canvasWidth, double canvasHeight, Camera camera)
        {
            var (x, y) = ViewProjection.ProjectCanvasToCameraPlane(canvasX, canvasY, canvasWidth, canvasHeight);
            var posCameraSystem = ViewProjection.ProjectCameraPlaneToCameraSystem(x, y, camera.NearPlane);
            var posScene = ViewProjection.ProjectCameraSystemToSceneSystem(posCameraSystem, camera.Frame);

            var rayOffset = camera.Frame.Offset;
            var rayDirection = posScene - rayOffset;

            var (isintersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);
            if (isintersected)
            {
                camera.MoveTargetTo(intersection);
            }

            return camera;
        }

        public Camera Move(Guid bodyId, double startX, double startY, double endX, double endY, int canvasWidth, int canvasHeight, Camera camera)
        {
            var startOffset = ViewProjection.ProjectCanvasToSceneSystem(startX, startY, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var endOffset = ViewProjection.ProjectCanvasToSceneSystem(endX, endY, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var offset = camera.Frame.Offset;
            var startDirection = startOffset - offset;
            var endDirection = endOffset - offset;
            var body = Scene.GetBody(bodyId);

            var moveState = new MoveState()
            {
                SelectedBody = body,
                StartMoveX = startX,
                StartMoveY = startY,
                StartMoveOffset = startOffset,
                StartMoveDirection = startDirection,
                EndMoveX = endX,
                EndMoveY = endY,
                EndMoveOffset = endOffset,
                EndMoveDirection = endDirection,
                CanvasWidth = canvasWidth,
                CanvasHeight = canvasHeight,
                Camera = camera
            };

            if (!_moveSensorProcessors.Process(body?.Sensor, moveState))
            {
                var deltaX = endX - startX;
                var deltaY = endY - startY;
                return Orbit(deltaX, deltaY, canvasWidth, canvasHeight, camera);
            }

            return camera;
        }

        public Camera Zoom(double pixelDeltaY, Camera camera)
        {
            var dy = pixelDeltaY * 1.0;
            camera.Zoom(dy);
            return camera;
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