using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathmatics;

namespace IGraphics.LogicViewing
{
    public class LogicView : ILogicView
    {
        public LogicView(Scene scene)
        {
            Scene = scene;
        }

        public Scene Scene { get; }

        public Camera Select(double canvasX, double canvasY, double canvasWidth, double canvasHeight, Camera camera)
        {
            var (x, y) = ViewProjection.ProjectCanvasToCameraPlane(canvasX, canvasY, canvasWidth, canvasHeight);
            var posCameraSystem = ViewProjection.ProjectCameraPlaneToCameraSystem(x, y, camera.NearPlane);
            var posScene = ViewProjection.ProjectCameraSystemToSceneSystem(posCameraSystem, camera.Frame);

            var rayOffset = camera.Frame.Offset;
            var rayDirection = posScene - rayOffset;

            var (isintersected, intersection) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);
            if (isintersected)
            {
                camera.MoveTargetTo(intersection);
            }

            return camera;
        }

        public Camera Orbit(double pixelDeltaX, double pixelDeltaY, Camera camera)
        {
            var horicontalPixelFor360Degree = 600;
            var verticalPixelFor360Degree = 400;
            var alpha = -(360.0 * pixelDeltaX / horicontalPixelFor360Degree).DegToRad();
            var beta = -(360.0 * pixelDeltaY / verticalPixelFor360Degree).DegToRad();
            camera.OrbitXY(alpha);
            camera.OrbitYZ(beta);
            return camera;
        }

        public Camera Zoom(double pixelDeltaY, Camera camera)
        {
            var dy = pixelDeltaY * 1.0;
            camera.Zoom(dy);
            return camera;
        }
    }
}