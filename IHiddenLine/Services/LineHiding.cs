using IGraphics.Mathmatics;
using IGraphics.Mathmatics.Extensions;
using IHiddenLineGraphics.Model;
using System.Collections.Generic;
using System.Linq;

namespace IHiddenLineGraphics.Services
{
    public static class LineHiding
    {
        public static IEnumerable<LineHL> FilterOutHiddenLines(this IEnumerable<LineHL> lines, SceneHL scene)
        {
            var triangles = scene.Triangles;
            var visibleLines = lines.Where(line => IsLineVisible(line, scene.NearPlaneDistance, triangles)).ToList();
            return visibleLines;
        }

        private static bool IsLineVisible(LineHL line, double nearPlaneDistance, TriangleHL[] triangles)
        {
            var (rayOffset, rayDirection) = GetRaytracingRay(line, nearPlaneDistance);
            var (success, edgeIntersection) = GetRayIntersectionWithEdge(rayOffset, rayDirection, line.Edge);
            if (!success) return true;

            rayDirection = (edgeIntersection - rayOffset);
            var edgeDistanceSquared = rayDirection.SquaredLength();
            foreach (var triangle in triangles)
            {
                if (!IsTriangleNeighbourOfLine(triangle, line))
                {
                    if (!IsLineInTrianglePlane(line, triangle))
                    {
                        var (hasIntersection, triangleDistanceSquared) = IntersectionMath.GetSquaredDistanceOfIntersectionOfRayAndTriangle(rayOffset, rayDirection, triangle.P1, triangle.P2, triangle.P3);
                        if (hasIntersection && triangleDistanceSquared < edgeDistanceSquared)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static (Position3D offset, Vector3D direction) GetRaytracingRay(LineHL line, double nearPlaneDistance)
        {
            var start = line.Start;
            var end = line.End;
            var centerViewLine = new PointHL((start.X + end.X) / 2.0, (start.Y + end.Y) / 2.0);
            var centerCameraPlaneLine = Projection.ProjectCameraPlaneToCameraSystem(centerViewLine, nearPlaneDistance);
            var rayOffset = new Position3D(0.0, 0.0, 0.0);
            var rayDirection = centerCameraPlaneLine - rayOffset;
            return (rayOffset, rayDirection);
        }

        private static (bool, Position3D) GetRayIntersectionWithEdge(Position3D rayOffset, Vector3D rayDirection, EdgeHL edge)
        {
            var edgeEnd = edge.End;
            var edgeOffset = edge.Start;
            var edgeDirection = edgeEnd - edgeOffset;

            var (success, intersection) = IntersectionMath.CalculatePerpendicularPoint(edgeOffset, edgeDirection, rayOffset, rayDirection);

            return (success, intersection);
        }

        private static bool IsTriangleNeighbourOfLine(TriangleHL triangle, LineHL line)
        {
            var firstTriangle = line.Edge.First;
            var secondTriangle = line.Edge.Second;
            var isNeighbour = firstTriangle == triangle || secondTriangle == triangle;
            return isNeighbour;
        }

        private static bool IsLineInTrianglePlane(LineHL line, TriangleHL triangle)
        {
            var start = line.Edge.Start;
            var end = line.Edge.End;
            var distStart = start.DistancePositionToPlane(triangle.P1, triangle.Normal);
            var distEnd = end.DistancePositionToPlane(triangle.P1, triangle.Normal);
            var isInPlane = distStart.EqualsTo(0.0) && distEnd.EqualsTo(0.0);
            return isInPlane;
        }
    }
}