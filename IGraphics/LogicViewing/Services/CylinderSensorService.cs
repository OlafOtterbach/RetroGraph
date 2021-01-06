using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
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

        public void Process(ISensor sensor, MoveState moveState)
        {
            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveState.StartMoveX, moveState.StartMoveY, moveState.CanvasWidth, moveState.CanvasHeight, moveState.Camera.NearPlane, moveState.Camera.Frame);
            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveState.EndMoveX, moveState.EndMoveY, moveState.CanvasWidth, moveState.CanvasHeight, moveState.Camera.NearPlane, moveState.Camera.Frame);
            var offset = moveState.Camera.Frame.Offset;
            var startMoveDirection = startMoveOffset - offset;
            var endMoveDirection = endMoveOffset - offset;
            var body = _scene.GetBody(moveState.SelectedBodyId);

            Process(sensor as CylinderSensor,
                    body,
                    moveState.BodyTouchPosition,
                    moveState.StartMoveX,
                    moveState.StartMoveY,
                    startMoveOffset,
                    startMoveDirection,
                    moveState.EndMoveX,
                    moveState.EndMoveY,
                    endMoveOffset,
                    endMoveDirection,
                    moveState.CanvasWidth,
                    moveState.CanvasHeight,
                    moveState.Camera);
        }

        private static void Process(CylinderSensor cylinderSensor,
            Body body,
            Position3D bodyTouchPosition,
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
                angle = CalculateAngleForAxisLieingInCanvas(bodyTouchPosition ,startX, startY, endOffset, endDirection, axisFrame, canvasWidth, canvasHeight, camera);
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
            Position3D bodyTouchPosition,
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
                var axisOffset = axisFrame.Offset;
                var plumpPoint = IntersectionMath.CalculatePerpendicularPoint(bodyTouchPosition, axisOffset, axis);
                var distance = (bodyTouchPosition - plumpPoint).Length;
                direction = direction.Normalize() * distance;
                var startPosition = axisOffset - direction;
                var endPosition = axisOffset + direction;
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
