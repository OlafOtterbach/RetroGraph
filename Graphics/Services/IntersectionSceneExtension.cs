using RetroGraph.Mathmatics;
using System.Collections.Generic;
using System.Linq;

namespace RetroGraph.Graphics.Services
{
    public static class IntersectionSceneExtension
    {
        public static (bool, Position3D) GetIntersectionOfRayAndScene(this Scene scene, Position3D rayOffset, Vector3D rayDirection)
        {
            var triangles = GetSceneTriangles(scene);
            var minIntersectionResult
                  = triangles.Select(triangle => IntersectionMath.GetIntersectionAndSquaredDistanceOfRayAndTriangle(rayOffset, rayDirection, triangle.Item1, triangle.Item2, triangle.Item3))
                             .Where(result => result.Item1)
                             .AsParallel()
                             .Aggregate((false, new Position3D(), double.MaxValue), (acc, x) => x.Item3 < acc.Item3 ? x : acc);
            return (minIntersectionResult.Item1, minIntersectionResult.Item2);
        }

        private static IEnumerable<(Position3D, Position3D, Position3D)> GetSceneTriangles(Scene scene)
        {
            return scene.Bodies.SelectMany(GetBodyTriangles);
        }

        private static IEnumerable<(Position3D, Position3D, Position3D)> GetBodyTriangles(Body body)
        {
            var triangles = body.Faces.SelectMany(face => face.Triangles);
            foreach(var triangle in triangles)
            {
                var p1 = body.Frame * triangle.P1.Point.Position;
                var p2 = body.Frame * triangle.P2.Point.Position;
                var p3 = body.Frame * triangle.P3.Point.Position;
                yield return (p1, p2, p3);
            }
        }
    }
}
