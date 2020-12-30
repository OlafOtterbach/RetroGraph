using IGraphics.Graphics;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using System;

namespace IGraphics.LogicViewing.Services
{
    public class CylinderSensorService : IMoveSensorService
    {
        public bool CanProcess(ISensor sensor) => sensor is CylinderSensor;

        public void Process(ISensor sensor, MoveState moveState)
        {
            Process(sensor as CylinderSensor,
                    moveState.SelectedBody,
                    moveState.StartMoveX,
                    moveState.StartMoveY,
                    moveState.StartMoveOffset,
                    moveState.StartMoveDirection,
                    moveState.EndMoveX,
                    moveState.EndMoveY,
                    moveState.EndMoveOffset,
                    moveState.EndMoveDirection,
                    moveState.CanvasWidth,
                    moveState.CanvasHeight,
                    moveState.Camera);
        }

        private static void Process(CylinderSensor cylinderSensor,
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
            Camera camera)
        {
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;

            var axisFrame = body.Frame * Matrix44D.CreateRotation(cylinderSensor.Axis, 0.0);

            double angle;
            if (IsAxisIsInCameraPlane(axisFrame, camera))
            {
                angle = CalculateAngleForAxisLieingInCanvas(startX, startY, endOffset, endDirection, axisFrame, canvasWidth, canvasHeight, camera);
            }
            else
            {
                angle = CalculateAngleForAxisNotLieingInCanvas(startX, startY, startOffset, startDirection, endX, endY, endOffset, endDirection, axisFrame, canvasWidth, canvasHeight);
            }
            Rotate(body, cylinderSensor.Axis, angle);
        }

        private static bool IsAxisIsInCameraPlane(Matrix44D axisFrame, Camera camera)
        {
            var axis = axisFrame.Ez;
            var cameraDirection = camera.Frame.Ey;

            double limitAngle = 25.0;
            double alpha = axis.CounterClockwiseAngleWith(cameraDirection);
            alpha = alpha.Modulo2Pi();
            alpha = alpha.RadToDeg();
            var result = !((Math.Abs(alpha) < limitAngle) || (Math.Abs(alpha) > (180.0 - limitAngle)));

            return result;
        }

        private static double CalculateAngleForAxisLieingInCanvas(
            double startX,
            double startY,
            Position3D endOffset,
            Vector3D endDirection,
            Matrix44D axisFrame,
            double canvasWidth,
            double canvasHeight,
            Camera camera
        )
        {
            var angle = 0.0;
            var axis = axisFrame.Ez;
            var cameraDirection = camera.Frame.Ey;

            var direction = (cameraDirection & axis).Normalize() * 10000.0;
            var offset = ViewProjection.ProjectCanvasToSceneSystem(startX, startY, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var p1 = offset - direction;
            var p2 = offset + direction;
            var (success, plump) = IntersectionMath.CalculatePerpendicularPoint(p1, p2 - p1, endOffset, endDirection);
            if (success)
            {
                var sign = (plump - p1).Length < (plump - p2).Length ? -1.0 : 1.0;

                var (endX, endY) = ViewProjection.ProjectSceneSystemToCanvas(plump, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
                double delta = Math.Sqrt((endX - startX) * (endX - startX) + (endY - startY) * (endY - startY));
                angle = sign * delta.DegToRad();
            }

            return angle;
        }

        private static double CalculateAngleForAxisNotLieingInCanvas(
            double startX,
            double startY,
            Position3D startOffset,
            Vector3D startDirection,
            double endX,
            double endY,
            Position3D endOffset,
            Vector3D endDirection,
            Matrix44D axisFrame,
            double canvasWidth,
            double canvasHeight)
        {
            startOffset = GetPosition(startOffset, startDirection, axisFrame);
            endOffset = GetPosition(endOffset, endDirection, axisFrame);

            double sign = CalculateAngleSign(axisFrame, startOffset, endOffset);
            var angle = sign * 1.0 * CalculateAngle(startX, startY, endX, endY, canvasWidth, canvasHeight);

            return angle;
        }

        private static Position3D GetPosition(Position3D offset, Vector3D direction, Matrix44D axisFrame)
        {
            var (success, position) = IntersectionMath.Intersect(axisFrame.Offset, axisFrame.Ez, offset, direction);
            var result = success ? position : offset;
            return result;
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
