using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathmatics;
using System;

namespace IGraphics.LogicViewing
{
    public class LogicView : ILogicView
    {
        public LogicView(Scene scene)
        {
            Scene = scene;
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
            if (body?.Sensor is CylinderSensor cylinderSensor)
            {
                cylinderSensor.Process(
                    body,
                    startX,
                    startY,
                    startOffset,
                    startDirection,
                    endX,
                    endY,
                    endOffset,
                    endDirection,
                    canvasWidth,
                    canvasHeight, 
                    camera.NearPlane,
                    camera.Frame);
            }
            else
            {
                var deltaX = endX - startX;
                var deltaY = endY - startY;
                return Orbit(deltaX, deltaY, camera);
            }

            return camera;
        }

        public Camera Move2(Guid bodyId, double startX, double startY, double endX, double endY, int canvasWidth, int canvasHeight, Camera camera)
        {
            var deltaX = endX - startX;
            var deltaY = endY - startY;
            return Orbit(deltaX, deltaY, camera);
        }

        public Camera Zoom(double pixelDeltaY, Camera camera)
        {
            var dy = pixelDeltaY * 1.0;
            camera.Zoom(dy);
            return camera;
        }

        private Camera Orbit(double pixelDeltaX, double pixelDeltaY, Camera camera)
        {
            var horicontalPixelFor360Degree = 600;
            var verticalPixelFor360Degree = 400;
            var alpha = -(360.0 * pixelDeltaX / horicontalPixelFor360Degree).DegToRad();
            var beta = -(360.0 * pixelDeltaY / verticalPixelFor360Degree).DegToRad();
            camera.OrbitXY(alpha);
            camera.OrbitYZ(beta);
            return camera;
        }
    }
}