using IGraphics.Graphics;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using System;

namespace IGraphics.LogicViewing
{
    public static class SphereSensorExtension
    {
        /// <summary>
        /// Processes the event.
        /// </summary>
        /// <param name="inputEvent">Event, that is processed</param>
        /// <returns>bool  true=event is used, false=not used</returns>
        public static void Process(this SphereSensor sphereSensor,
            Body body,
            double startX,
            double startY,
            Position3D startOffset,
            Vector3D startDirection,
            double endX,
            double endY,
            Position3D endOffset,
            Vector3D endDirection,
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

            var offsetFrame = Matrix44D.CreateCoordinateSystem(body.Frame.Offset, new Vector3D(1, 0, 0), new Vector3D(0, 0, 1));
            var exAxisFrame = offsetFrame * Matrix44D.CreateRotation(exAxis, 0.0);
            var eyAxisFrame = offsetFrame * Matrix44D.CreateRotation(eyAxis, 0.0);

            double exSign = CalculateAngleSign(exAxisFrame, startOffset, endOffset);
            double exAngle = exSign * 2.0 * CalculateAngle(startY, endY, canvasWidth, canvasHeight);
            double eySign = CalculateAngleSign(eyAxisFrame, startOffset, endOffset);
            double eyAngle = eySign * 2.0 * CalculateAngle(startX, endX, canvasWidth, canvasHeight);

            var rotationEx = Matrix44D.CreateRotation(body.Frame.Offset, exAxis, exAngle);
            var rotationEy = Matrix44D.CreateRotation(body.Frame.Offset, eyAxis, eyAngle);

            body.Frame = rotationEx * rotationEy * body.Frame;
        }

        private static double CalculateAngleSign(Matrix44D axisFrame, Position3D start, Position3D end)
        {
            Matrix44D invframe = axisFrame.Inverse();

            var startInFrame = invframe * start;
            var endInFrame = invframe * end;

            var sx = startInFrame.X;
            var sy = startInFrame.Y;
            var ex = endInFrame.X;
            var ey = endInFrame.Y;

            var sign = TriangleMath.IsCounterClockwise(sx, sy, ex, ey, 0.0, 0.0) ? 1.0 : -1.0;

            return sign;
        }

        private static double CalculateAngle(
            double start,
            double end,
            double canvasWidth,
            double canvasHeight)
        {
            var min = Math.Min(canvasWidth, canvasHeight);
            var delta = Math.Abs(end - start);
            var angle = (360.0 * delta / min).DegToRad();
            return angle;
        }
    }
}