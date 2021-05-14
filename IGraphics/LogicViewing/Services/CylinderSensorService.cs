using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathematics;
using IGraphics.Mathematics.Extensions;
using System;

namespace IGraphics.LogicViewing.Services
{
    public class CylinderSensorService : IMoveSensorService
    {
        private Scene _scene;

        public CylinderSensorService(Scene scene)
        {
            _scene = scene;
        }

        public bool CanProcess(ISensor sensor) => sensor is CylinderSensor;

        public void Process(ISensor sensor, MoveEvent moveEvent)
        {
            var offset = moveEvent.Camera.Frame.Offset;

            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.StartMoveX, moveEvent.StartMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var startMoveDirection = startMoveOffset - offset;
            var startMoveRay = new Axis3D(startMoveOffset, startMoveDirection);

            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.EndMoveX, moveEvent.EndMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var endMoveDirection = endMoveOffset - offset;
            var endMoveRay = new Axis3D(endMoveOffset, endMoveDirection);

            var body = _scene.GetBody(moveEvent.SelectedBodyId);

            Process(sensor as CylinderSensor,
                    body,
                    moveEvent.BodyTouchPosition,
                    moveEvent.StartMoveX,
                    moveEvent.StartMoveY,
                    startMoveRay,
                    moveEvent.EndMoveX,
                    moveEvent.EndMoveY,
                    endMoveRay,
                    moveEvent.CanvasWidth,
                    moveEvent.CanvasHeight,
                    moveEvent.Camera);
        }

        private static void Process(CylinderSensor cylinderSensor,
            Body body,
            Position3D bodyTouchPosition,
            double startX,
            double startY,
            Axis3D startMoveRay,
            double endX,
            double endY,
            Axis3D endMoveRay,
            double canvasWidth,
            double canvasHeight,
            Camera camera)
        {
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;

            var cylinderAxis = new Axis3D(body.Frame.Offset, body.Frame * cylinderSensor.Axis);

            double angle;
            if (IsAxisIsInCameraPlane(cylinderAxis, camera))
            {
                angle = CalculateAngleForAxisLieingInCanvas(bodyTouchPosition ,startX, startY, endMoveRay, cylinderAxis, canvasWidth, canvasHeight, camera);
            }
            else
            {
                angle = CalculateAngleForAxisNotLieingInCanvas(startX, startY, startMoveRay, endX, endY, endMoveRay, cylinderAxis, canvasWidth, canvasHeight);
            }

            Rotate(body, cylinderAxis, angle);
        }

        private static bool IsAxisIsInCameraPlane(Axis3D cylinderAxis, Camera camera)
        {
            var cameraDirection = camera.Frame.Ey;
            double limitAngle = 25.0;
            double alpha = cylinderAxis.Direction.CounterClockwiseAngleWith(cameraDirection);
            alpha = alpha.Modulo2Pi();
            alpha = alpha.RadToDeg();
            var result = !((Math.Abs(alpha) < limitAngle) || (Math.Abs(alpha) > (180.0 - limitAngle)));

            return result;
        }

        private static double CalculateAngleForAxisLieingInCanvas(
            Position3D bodyTouchPosition,
            double startX,
            double startY,
            Axis3D endMoveRay,
            Axis3D cylinderAxis,
            double canvasWidth,
            double canvasHeight,
            Camera camera
        )
        {
            var angle = 0.0;
            var cameraDirection = camera.Frame.Ey;

            var direction = (cameraDirection & cylinderAxis.Direction).Normalize() * 10000.0;
            var offset = ViewProjection.ProjectCanvasToSceneSystem(startX, startY, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var p1 = offset - direction;
            var p2 = offset + direction;
            var projectedAxis = new Axis3D(p1, p2 - p1);
            var (success, plump) = projectedAxis.CalculatePerpendicularPoint(endMoveRay);
            if (success)
            {
                // Ermitteln des Vorzeichens für Drehung
                var sign = (plump - p1).Length < (plump - p2).Length ? -1.0 : 1.0;

                // Ermitteln der Länge der Mausbewegung in Richtung senkrecht zur Rotationsachse
                var (endX, endY) = ViewProjection.ProjectSceneSystemToCanvas(plump, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
                double delta = Vector2DMath.Length(startX, startY, endX, endY);

                // Projektion Berührungspunkt auf Rotationsachse.
                // Ermittel Scheitelpunkte der Rotation auf Achse senkrecht zur Kamera und Rotationsachse.
                // Projektion der Scheitelpunkte auf Canvas.
                // Abstand Scheitelpunkte auf Achse ist die Mausbewegung für 180°.
                // Winkel ist Verhältnis Länge Mausbewegung zur Länge für 180°.
                var plumpPoint = cylinderAxis.CalculatePerpendicularPoint(bodyTouchPosition);
                var distance = (bodyTouchPosition - plumpPoint).Length;
                direction = direction.Normalize() * distance;
                var startPosition = cylinderAxis.Offset - direction;
                var endPosition = cylinderAxis.Offset + direction;
                (startX, startY) = ViewProjection.ProjectSceneSystemToCanvas(startPosition, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
                (endX, endY) = ViewProjection.ProjectSceneSystemToCanvas(endPosition, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
                var lengthOfHalfRotation = Vector2DMath.Length(startX, startY, endX, endY);

                var angleInDegree = 180.0 * delta / lengthOfHalfRotation;

                angle = sign * angleInDegree.DegToRad();
            }

            return angle;
        }

        private static double CalculateAngleForAxisNotLieingInCanvas(
            double startX,
            double startY,
            Axis3D startMoveRay,
            double endX,
            double endY,
            Axis3D endMoveRay,
            Axis3D cylinderAxis,
            double canvasWidth,
            double canvasHeight)
        {
            var axisPlane = new Plane3D(cylinderAxis.Offset, cylinderAxis.Direction);
            var startOffset = GetPosition(startMoveRay, axisPlane);
            var endOffset = GetPosition(endMoveRay, axisPlane);

            double sign = CalculateAngleSign(cylinderAxis, startOffset, endOffset);
            var angle = sign * 1.0 * CalculateAngle(startX, startY, endX, endY, canvasWidth, canvasHeight);

            return angle;
        }

        private static Position3D GetPosition(Axis3D moveRay, Plane3D axisPlane)
        {
            var (success, position) = axisPlane.Intersect(moveRay);
            var result = success ? position : moveRay.Offset;
            return result;
        }

        private static double CalculateAngleSign(Axis3D cylinderAxis, Position3D start, Position3D end)
        {
            var axisFrame = Matrix44D.CreateRotation(cylinderAxis.Offset, cylinderAxis.Direction);
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

        private static void Rotate(Body body, Axis3D axis, double angle)
        {
            var rotation = Matrix44D.CreateRotation(body.Frame.Offset, axis.Direction, angle);
            body.Frame = rotation * body.Frame;
        }
    }
}
