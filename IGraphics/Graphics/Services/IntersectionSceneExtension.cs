using IGraphics.Mathmatics;
using System.Collections.Generic;
using System.Linq;

namespace IGraphics.Graphics.Services
{
    public static class IntersectionSceneExtension
    {
        public static (bool, Position3D, Body) GetIntersectionOfRayAndScene(this Scene scene, Position3D rayOffset, Vector3D rayDirection)
        {
            var triangles = GetSceneTriangles(scene);
            var minIntersectionResult
                  = triangles.Select(triangle => (triangle.Item1, IntersectionMath.GetIntersectionAndSquaredDistanceOfRayAndTriangle(rayOffset, rayDirection, triangle.Item2, triangle.Item3, triangle.Item4)))
                             .Where(result => result.Item2.Item1)
                             .AsParallel()
                             .Aggregate(((Body)null, (false, new Position3D(), double.MaxValue)), (acc, x) => x.Item2.Item3 < acc.Item2.Item3 ? x : acc);
            return (minIntersectionResult.Item2.Item1, minIntersectionResult.Item2.Item2, minIntersectionResult.Item1);
        }

        private static IEnumerable<(Body, Position3D, Position3D, Position3D)> GetSceneTriangles(Scene scene)
        {
            return scene.Bodies.SelectMany(GetBodyTriangles);
        }

        private static IEnumerable<(Body, Position3D, Position3D, Position3D)> GetBodyTriangles(Body body)
        {
            var triangles = body.Faces.SelectMany(face => face.Triangles);
            foreach (var triangle in triangles)
            {
                var p1 = body.Frame * triangle.P1.Point.Position;
                var p2 = body.Frame * triangle.P2.Point.Position;
                var p3 = body.Frame * triangle.P3.Point.Position;
                yield return (body, p1, p2, p3);
            }
        }
    }
}