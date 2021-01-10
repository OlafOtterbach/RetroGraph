using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using System;

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
            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.EndMoveX, moveEvent.EndMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var offset = moveEvent.Camera.Frame.Offset;
            var endMoveDirection = endMoveOffset - offset;
            var body = _scene.GetBody(moveEvent.SelectedBodyId);

            Process(body,
                    moveEvent.BodyTouchPosition,
                    moveEvent.StartMoveX,
                    moveEvent.StartMoveY,
                    moveEvent.EndMoveX,
                    moveEvent.EndMoveY,
                    endMoveOffset,
                    endMoveDirection,
                    moveEvent.CanvasWidth,
                    moveEvent.CanvasHeight,
                    moveEvent.Camera);

        }

        private static void Process(
            Body body,
            Position3D bodyTouchPosition,
            double startX,
            double startY,
            double endX,
            double endY,
            Position3D endOffset,
            Vector3D endDirection,
            double canvasWidth,
            double canvasHeight,
            Camera camera)
        {
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;

            var canvasOrigin = ViewProjection.ProjectCanvasToSceneSystem(0.0, 0.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var canvasExPos = ViewProjection.ProjectCanvasToSceneSystem(1.0, 0.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var canvasEyPos = ViewProjection.ProjectCanvasToSceneSystem(0.0, 1.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var exAxis = (canvasExPos - canvasOrigin).Normalize();
            var ezAxis = (canvasEyPos - canvasOrigin).Normalize();
            var eyAxis = (ezAxis & exAxis).Normalize();
            var zAxis = Matrix44D.CreateCoordinateSystem(body.Frame.Offset, exAxis, ezAxis);
            var xAxis = Matrix44D.CreateCoordinateSystem(body.Frame.Offset, eyAxis, exAxis);

            Process(body, bodyTouchPosition, startX, startY, endX, endY, endOffset, endDirection, zAxis, canvasWidth, canvasHeight, camera);
            Process(body, bodyTouchPosition, startX, startY, endX, endY, endOffset, endDirection, xAxis, canvasWidth, canvasHeight, camera);
        }

        private static void Process(
            Body body,
            Position3D bodyTouchPosition,
            double startX,
            double startY,
            double endX,
            double endY,
            Position3D endOffset,
            Vector3D endDirection,
            Matrix44D axisFrame,
            double canvasWidth,
            double canvasHeight,
            Camera camera)
        {
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;
            var angle = CalculateAngle(bodyTouchPosition, startX, startY, endOffset, endDirection, axisFrame, canvasWidth, canvasHeight, camera);
            var rotation = Matrix44D.CreateRotation(axisFrame.Offset, axisFrame.Ez, angle);
            body.Frame = rotation * body.Frame;
        }

        private static double CalculateAngle(
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
                var sign = (plump - p1).Length < (plump - p2).Length ? -1.0 : 1.0;
                var (endX, endY) = ViewProjection.ProjectSceneSystemToCanvas(plump, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
                double delta = Vector2DMath.Length(startX, startY, endX, endY);
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
    }
}
