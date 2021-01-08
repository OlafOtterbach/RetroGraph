﻿using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathmatics;

namespace IGraphics.LogicViewing.Services
{
    public class PlaneSensorService : IMoveSensorService
    {
        private Scene _scene;

        public PlaneSensorService(Scene scene)
        {
            _scene = scene;
        }

        public bool CanProcess(ISensor sensor) => sensor is PlaneSensor;

        public void Process(ISensor sensor, MoveEvent moveEvent)
        {
            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.StartMoveX, moveEvent.StartMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.EndMoveX, moveEvent.EndMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var offset = moveEvent.Camera.Frame.Offset;
            var startMoveDirection = startMoveOffset - offset;
            var endMoveDirection = endMoveOffset - offset;
            var body = _scene.GetBody(moveEvent.SelectedBodyId);

            Process(sensor as PlaneSensor,
                body,
                startMoveOffset,
                startMoveDirection,
                endMoveOffset,
                endMoveDirection);
        }

        private static void Process(PlaneSensor planeSensor,
            Body body,
            Position3D startOffset,
            Vector3D startDirection,
            Position3D endOffset,
            Vector3D endDirection)
        {
            if (startOffset == endOffset) return;
            var moveVector = CalculateMove(body, planeSensor.PlaneNormal, startOffset, startDirection, endOffset, endDirection);
            var moveFrame = Matrix44D.CreateTranslation(moveVector);

            body.Frame = moveFrame * body.Frame;
        }

        private static Vector3D CalculateMove(Body body, Vector3D normal, Position3D startOffset, Vector3D startDirection, Position3D endOffset, Vector3D endDirection)
        {
            var planeOffset = body.Frame.Offset;
            var planeNormal = body.Frame * normal;

            var (startIsIntersecting, startIntersection) = IntersectionMath.Intersect(planeOffset, planeNormal, startOffset, startDirection);
            var (endIsIntersecting, endIntersection) = IntersectionMath.Intersect(planeOffset, planeNormal, endOffset, endDirection);
            var moveVector = startIsIntersecting && endIsIntersecting ? endIntersection - startIntersection : new Vector3D();

            return moveVector;
        }
    }
}
