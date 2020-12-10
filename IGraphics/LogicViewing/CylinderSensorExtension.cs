using IGraphics.Graphics;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using System;

namespace IGraphics.LogicViewing
{
    public static class CylinderSensorExtension
    {
        /// <summary>
        /// Processes the event.
        /// </summary>
        /// <param name="inputEvent">Event, that is processed</param>
        /// <returns>bool  true=event is used, false=not used</returns>
        public static void Process(this CylinderSensor cylinderSensor,
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
            var axis = body.Frame * cylinderSensor.Axis;
            var axisFrame = Matrix44D.CreateRotation(axis, 0.0);
            double sign = CalculateAngleSign(axisFrame, startOffset, endOffset);
            double angle = sign * 1.0 * CalculateAngle(
            axisFrame,
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
            nearPlaneDist,
            cameraFrame);

            Rotate(body, axis, angle);
        }

        private static double CalculateAngleSign(Matrix44D axisFrame, Position3D start, Position3D end)
        {
            Matrix44D frame = axisFrame;
            Matrix44D invframe = axisFrame.Inverse();
            start = invframe * start;
            end = invframe * end;
            double xstart = start.X;
            double ystart = start.Y;
            double alpha = AngleMath.VectorToAngle(xstart, ystart);
            double xend = end.X;
            double yend = end.Y;
            double beta = AngleMath.VectorToAngle(xend, yend);
            alpha = alpha.RadToDeg();
            beta = beta.RadToDeg();
            alpha = alpha.RadToDeg();
            beta = beta.RadToDeg();
            double sign = 1.0;
            if (alpha > beta)
            {
                sign = -1.0;
            }
            return sign;
        }

        private static double CalculateAngle(
            Matrix44D axisFrame,
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
            double angle = 0.0;
            Vector3D axis = axisFrame * new Vector3D(0.0, 0.0, 1.0);
            double limitAngle = 25.0;
            double alpha = axis.CounterClockwiseAngleWith(cameraFrame.Ey);
            alpha = alpha.Modulo2Pi();
            alpha = alpha.RadToDeg();
            if ((Math.Abs(alpha) < limitAngle) || (Math.Abs(alpha) > (180.0 - limitAngle)))
            {
                double delta = Math.Sqrt((endX - startX) * (endX - startX) + (endY - startY) * (endY - startY));
                angle = delta.DegToRad();
            }
            else
            {
                var direction = (cameraFrame.Ey & axis).Normalize() * 10000.0;
                var p1 = startOffset - direction;
                var p2 = startOffset + direction;
                var (hasIntersection, plump) = IntersectionMath.CalculatePerpendicularPoint(p1, direction, endOffset, endDirection);
                if (hasIntersection)
                {
                    var (canvasPlumpX, canvasPlumpY) = ViewProjection.ProjectSceneSystemToCanvas(plump, canvasWidth, canvasHeight, nearPlaneDist, cameraFrame);
                    double delta = Math.Sqrt((canvasPlumpX - startX) * (canvasPlumpX - startX) + (canvasPlumpY - startY) * (canvasPlumpY - startY));
                    angle = delta.DegToRad();
                }
            }
            return angle;
        }

        private static void Rotate(Body body, Vector3D axis, double angle)
        {
            var rotation = Matrix44D.CreateRotation(axis, angle);
            body.Frame = rotation * body.Frame;
        }
    }
}