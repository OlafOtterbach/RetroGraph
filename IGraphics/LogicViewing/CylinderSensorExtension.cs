using IGraphics.Graphics;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using System;

namespace IGraphics.LogicViewing
{
    public static class CylinderSensorExtension
    {
        public static void Process(this CylinderSensor cylinderSensor,
            Body body,
            double startX,
            double startY,
            Position3D startOffset,
            double endX,
            double endY,
            Position3D endOffset,
            double canvasWidth,
            double canvasHeight)
        {
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;

            var axisFrame = body.Frame * Matrix44D.CreateRotation(cylinderSensor.Axis, 0.0);
            double sign = CalculateAngleSign(axisFrame, startOffset, endOffset);
            double angle = sign * 2.0 * CalculateAngle(startX, startY, endX, endY, canvasWidth, canvasHeight);
            Rotate(body, cylinderSensor.Axis, angle);
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
            double startX,
            double startY,
            double endX,
            double endY,
            double canvasWidth,
            double canvasHeight)
        {
            var min = Math.Min(canvasWidth, canvasHeight);
            var delta = Vector2DMath.Length(startX, startY, endX, endY);
            var angle = (360.0 * delta / min).DegToRad();
            return angle;
        }

        private static void Rotate(Body body, Vector3D axis, double angle)
        {
            var rotation = Matrix44D.CreateRotation(body.Frame.Offset, axis, angle);
            body.Frame = rotation * body.Frame;
        }
    }
}