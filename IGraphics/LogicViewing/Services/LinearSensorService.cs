using IGraphics.Graphics;
using IGraphics.Graphics.Services;
using IGraphics.Mathmatics;

namespace IGraphics.LogicViewing.Services
{
    public class LinearSensorService : IMoveSensorService
    {
        private Scene _scene;

        public LinearSensorService(Scene scene)
        {
            _scene = scene;
        }

        public bool CanProcess(ISensor sensor) => sensor is LinearSensor;

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

            Process(sensor as LinearSensor, body, startMoveRay, endMoveRay);
        }

        private static void Process(LinearSensor linearSensor, Body body, Axis3D startMoveRay, Axis3D endMoveRay)
        {
            if (startMoveRay.Offset == endMoveRay.Offset) return;

            var moveVector = CalculateMove(body, linearSensor.Axis, startMoveRay, endMoveRay);
            var moveFrame = Matrix44D.CreateTranslation(moveVector);

            body.Frame = moveFrame * body.Frame;
        }

        private static Vector3D CalculateMove(Body body, Vector3D axis, Axis3D startMoveRay, Axis3D endMoveRay)
        {
            var axisOffset = body.Frame.Offset;
            var axisDirection = body.Frame * axis;
            var moveAxis = new Axis3D(axisOffset, axisDirection);

            var (startExist, startPlump) = moveAxis.CalculatePerpendicularPoint(startMoveRay);
            var (endExist, endPlump) = moveAxis.CalculatePerpendicularPoint(endMoveRay);
            var moveVector = startExist && endExist ? endPlump - startPlump : new Vector3D();

            return moveVector;
        }
    }
}

