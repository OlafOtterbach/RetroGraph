using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathematics;

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
            var offset = moveEvent.Camera.Frame.Offset;

            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.StartMoveX, moveEvent.StartMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var startMoveDirection = startMoveOffset - offset;
            var startMoveRay = new Axis3D(startMoveOffset, startMoveDirection);

            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.EndMoveX, moveEvent.EndMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var endMoveDirection = endMoveOffset - offset;
            var endMoveRay = new Axis3D(endMoveOffset, endMoveDirection);

            var body = _scene.GetBody(moveEvent.SelectedBodyId);

            Process(sensor as PlaneSensor, body, startMoveRay, endMoveRay);
        }

        private static void Process(PlaneSensor planeSensor,
            Body body,
            Axis3D startMoveRay,
            Axis3D endMoveRay)
        {
            if (startMoveRay.Offset == endMoveRay.Offset) return;
            var moveVector = CalculateMove(body, planeSensor.PlaneNormal, startMoveRay, endMoveRay);
            var moveFrame = Matrix44D.CreateTranslation(moveVector);

            body.Frame = moveFrame * body.Frame;
        }

        private static Vector3D CalculateMove(Body body, Vector3D normal, Axis3D startMoveRay, Axis3D endMoveRay)
        {
            var planeOffset = body.Frame.Offset;
            var planeNormal = body.Frame * normal;
            var movePlane = new Plane3D(planeOffset, planeNormal);

            var (startIsIntersecting, startIntersection) = movePlane.Intersect(startMoveRay);
            var (endIsIntersecting, endIntersection) = movePlane.Intersect(endMoveRay);
            var moveVector = startIsIntersecting && endIsIntersecting ? endIntersection - startIntersection : new Vector3D();

            return moveVector;
        }
    }
}
