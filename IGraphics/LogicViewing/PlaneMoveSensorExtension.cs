using IGraphics.Graphics;
using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using System;

namespace IGraphics.LogicViewing
{
    public static class PlaneMoveSensorExtension
    {
        /// <summary>
        /// Processes the event.
        /// </summary>
        /// <param name="inputEvent">Event, that is processed</param>
        /// <returns>bool  true=event is used, false=not used</returns>
        public static void Process(this PlaneMoveSensor planeSensor,
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
            var planeOffset = new Position3D();
            var planeNormal = body.Frame * normal;

            var (startIsIntersecting, startIntersection) = IntersectionMath.Intersect(planeOffset, planeNormal, startOffset, startDirection);
            var (endIsIntersecting, endIntersection) = IntersectionMath.Intersect(planeOffset, planeNormal, endOffset, endDirection);
            var moveVector = startIsIntersecting && endIsIntersecting ? endIntersection - startIntersection : new Vector3D();

            return moveVector;
        }
    }
}