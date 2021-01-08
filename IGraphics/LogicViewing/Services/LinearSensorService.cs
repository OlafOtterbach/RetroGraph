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
            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.StartMoveX, moveEvent.StartMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.EndMoveX, moveEvent.EndMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var offset = moveEvent.Camera.Frame.Offset;
            var startMoveDirection = startMoveOffset - offset;
            var endMoveDirection = endMoveOffset - offset;
            var body = _scene.GetBody(moveEvent.SelectedBodyId);


            Process(sensor as LinearSensor,
                body,
                startMoveOffset,
                startMoveDirection,
                endMoveOffset,
                endMoveDirection);
        }

        private static void Process(LinearSensor linearSensor,
             Body body,
             Position3D startOffset,
             Vector3D startDirection,
             Position3D endOffset,
             Vector3D endDirection)
        {
            if (startOffset == endOffset) return;
            var moveVector = CalculateMove(body, linearSensor.Axis, startOffset, startDirection, endOffset, endDirection);
            var moveFrame = Matrix44D.CreateTranslation(moveVector);

            body.Frame = moveFrame * body.Frame;
        }

        private static Vector3D CalculateMove(Body body, Vector3D axis, Position3D startOffset, Vector3D startDirection, Position3D endOffset, Vector3D endDirection)
        {
            var axisOffset = body.Frame.Offset;
            var axisDirection = body.Frame * axis;
            var (startExist, startPlump) = IntersectionMath.CalculatePerpendicularPoint(axisOffset, axisDirection, startOffset, startDirection);
            var (endExist, endPlump) = IntersectionMath.CalculatePerpendicularPoint(axisOffset, axisDirection, endOffset, endDirection);
            var moveVector = startExist && endExist ? endPlump - startPlump : new Vector3D();

            return moveVector;
        }
    }
}

