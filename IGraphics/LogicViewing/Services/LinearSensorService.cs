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

        public void Process(ISensor sensor, MoveState moveState)
        {
            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveState.StartMoveX, moveState.StartMoveY, moveState.CanvasWidth, moveState.CanvasHeight, moveState.Camera.NearPlane, moveState.Camera.Frame);
            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveState.EndMoveX, moveState.EndMoveY, moveState.CanvasWidth, moveState.CanvasHeight, moveState.Camera.NearPlane, moveState.Camera.Frame);
            var offset = moveState.Camera.Frame.Offset;
            var startMoveDirection = startMoveOffset - offset;
            var endMoveDirection = endMoveOffset - offset;
            var body = _scene.GetBody(moveState.SelectedBodyId);


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

