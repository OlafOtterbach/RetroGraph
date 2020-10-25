using RetroGraph.HiddenLine.Model;
using RetroGraph.Mathmatics;
using System;

namespace RetroGraph.LogicViewing
{
    public static class ViewProjection
    {
        public static Position3D ProjectCameraSystemToSceneSystem(this Position3D position, Matrix44D cameraFrame)
        {
            var scenePosition = cameraFrame * position;
            return scenePosition;
        }

        public static Position3D ProjectCameraPlaneToCameraSystem(double cameraPlaneX, double cameraPlaneY, double nearPlaneDist)
        {
            var positionInCameraCoordinates = new Position3D(cameraPlaneX, nearPlaneDist, cameraPlaneY);
            return positionInCameraCoordinates;
        }

        public static (double, double) ProjectCanvasToCameraPlane(double canvasX, double canvasY, double canvasWidth, double canvasHeight)
        {
            var size = Math.Min(canvasWidth, canvasHeight);
            var x = (canvasX - canvasWidth / 2.0) / size;
            var y = (canvasHeight / 2.0 - canvasY) / size;
            return (x, y);
        }
    }
}
