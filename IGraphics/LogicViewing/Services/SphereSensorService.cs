using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;

namespace IGraphics.LogicViewing.Services
{
    public class SphereSensorService : IMoveSensorService
    {
        private Scene _scene;

        public SphereSensorService(Scene scene)
        {
            _scene = scene;
        }

        public bool CanProcess(ISensor sensor) => sensor is SphereSensor;

        public void Process(ISensor sensor, MoveEvent moveEvent)
        {
            var startX = moveEvent.StartMoveX;
            var startY = moveEvent.StartMoveY;
            var endX = moveEvent.EndMoveX;
            var endY = moveEvent.EndMoveY;
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;

            var offset = moveEvent.Camera.Frame.Offset;
            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.EndMoveX, moveEvent.EndMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var endMoveDirection = endMoveOffset - offset;
            var endMoveRay = new Axis3D(endMoveOffset, endMoveDirection);
            var body = _scene.GetBody(moveEvent.SelectedBodyId);
            var camera = moveEvent.Camera;
            var canvasWidth = moveEvent.CanvasWidth;
            var canvasHeight = moveEvent.CanvasHeight;
            var bodyTouchPosition = moveEvent.BodyTouchPosition;

            var canvasOrigin = ViewProjection.ProjectCanvasToSceneSystem(0.0, 0.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var canvasExPos = ViewProjection.ProjectCanvasToSceneSystem(1.0, 0.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var canvasEyPos = ViewProjection.ProjectCanvasToSceneSystem(0.0, 1.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var exAxis = (canvasExPos - canvasOrigin).Normalize();
            var ezAxis = (canvasEyPos - canvasOrigin).Normalize();

            var zCylinderAxis = new Axis3D(body.Frame.Offset, ezAxis);
            var xCylinderAxis = new Axis3D(body.Frame.Offset, exAxis);

            Process(body, bodyTouchPosition, startX, startY, endMoveRay, zCylinderAxis, canvasWidth, canvasHeight, camera);
            Process(body, bodyTouchPosition, startX, startY, endMoveRay, xCylinderAxis, canvasWidth, canvasHeight, camera);
        }

        private static void Process(
            Body body,
            Position3D bodyTouchPosition,
            double startX,
            double startY,
            Axis3D endMoveRay,
            Axis3D cylinderAxis,
            double canvasWidth,
            double canvasHeight,
            Camera camera)
        {
            var angle = CalculateAngle(bodyTouchPosition, startX, startY, endMoveRay, cylinderAxis, canvasWidth, canvasHeight, camera);
            var rotation = Matrix44D.CreateRotation(cylinderAxis.Offset, cylinderAxis.Direction, angle);
            body.Frame = rotation * body.Frame;
        }

        private static double CalculateAngle(
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
    }
}
