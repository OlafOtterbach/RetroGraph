using IGraphics.Graphics;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;

namespace IGraphics.LogicViewing
{
    public static class SphereSensorExtension
    {
        public static void Process(this SphereSensor sphereSensor,
            Body body,
            double startX,
            double startY,
            double endX,
            double endY,
            double canvasWidth,
            double canvasHeight,
            double nearPlaneDist,
            Matrix44D cameraFrame)
        {
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;

            var canvasOrigin = ViewProjection.ProjectCanvasToSceneSystem(0.0, 0.0, canvasWidth, canvasHeight, nearPlaneDist, cameraFrame);
            var canvasExPos = ViewProjection.ProjectCanvasToSceneSystem(1.0, 0.0, canvasWidth, canvasHeight, nearPlaneDist, cameraFrame);
            var canvasEyPos = ViewProjection.ProjectCanvasToSceneSystem(0.0, 1.0, canvasWidth, canvasHeight, nearPlaneDist, cameraFrame);
            var exAxis = canvasExPos - canvasOrigin;
            var eyAxis = canvasEyPos - canvasOrigin;

            double exAngle = 1.0 * CalculateAngle(startY, endY, canvasHeight);
            double eyAngle = 1.0 * -CalculateAngle(startX, endX, canvasWidth);

            var rotationEx = Matrix44D.CreateRotation(body.Frame.Offset, exAxis, exAngle);
            var rotationEy = Matrix44D.CreateRotation(body.Frame.Offset, eyAxis, eyAngle);

            body.Frame = rotationEx * rotationEy * body.Frame;
        }

        private static double CalculateAngle(
            double start,
            double end,
            double size)
        {
            var delta = end - start;
            var angle = (360.0 * delta / size).DegToRad();
            return angle;
        }
    }
}