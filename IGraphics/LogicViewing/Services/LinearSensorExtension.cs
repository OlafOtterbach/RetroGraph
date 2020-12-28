using IGraphics.Graphics;
using IGraphics.Mathmatics;

namespace IGraphics.LogicViewing
{
    public static class LinearSensorExtension
    {
        public static void Process(this LinearSensor linearSensor,
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
