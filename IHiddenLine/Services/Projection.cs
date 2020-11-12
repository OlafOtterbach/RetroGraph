using IGraphics.Mathmatics;
using IHiddenLineGraphics.Model;
using System;

namespace IHiddenLineGraphics.Services
{
    public static class Projection
    {
        public static Position3D ProjectCameraSystemToSceneSystem(this Position3D position, Matrix44D cameraFrame)
        {
            var scenePosition = cameraFrame * position;
            return scenePosition;
        }

        public static Position3D ProjectSceneSystemToCameraSystem(this Position3D position, Matrix44D cameraFrame)
        {
            var cameraPosition = cameraFrame.Inverse() * position;
            return cameraPosition;
        }

        public static PointHL ProjectCameraSystemToCameraPlane(this Position3D position, double nearPlaneDist)
        {
            double x = (nearPlaneDist / position.Y) * position.X;
            double y = (nearPlaneDist / position.Y) * position.Z;
            return new PointHL(x, y);
        }

        public static PointHL ProjectCameraSystemToCameraPlane(this Position3D position, double nearPlaneDist, Matrix44D cameraFrame)
        {
            var pos = cameraFrame * position;
            var fac = (nearPlaneDist / pos.Y);
            double x = fac * position.X;
            double y = fac * position.Z;
            return new PointHL(x, y);
        }

        public static Position3D ProjectCameraPlaneToCameraSystem(this PointHL cameraPlanePoint, double nearPlaneDist)
        {
            var positionInCameraCoordinates = new Position3D(cameraPlanePoint.X, nearPlaneDist, cameraPlanePoint.Y);
            return positionInCameraCoordinates;
        }

        public static (double, double) ProjectCanvasToCameraPlane(double canvasX, double canvasY, double canvasWidth, double canvasHeight)
        {
            var size = Math.Min(canvasWidth, canvasHeight);
            var x = (canvasX - canvasWidth / 2.0) / size;
            var y = (canvasHeight / 2.0 - canvasY) / size;
            return (x, y);
        }

        public static (int x, int y) ProjectCameraPlaneToCanvas(this PointHL point, double canvasWidth, double canvasHeight)
        {
            var size = Math.Min(canvasWidth, canvasHeight);
            var x = (int)(point.X * size + canvasWidth / 2.0);
            var y = (int)(canvasHeight - (point.Y * size + canvasHeight / 2.0));
            return (x, y);
        }
    }
}