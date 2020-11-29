using IGraphics.Graphics;
using IGraphics.LogicViewing;
using IGraphics.Mathmatics;
using IHiddenLineGraphics.Model;
using System.Collections.Generic;
using System.Linq;

namespace IHiddenLineGraphics.Services
{
    public static class Converting
    {
        public static SceneHL ToHiddenLineScene(this Scene scene, Camera camera)
        {
            var cameraFrame = camera.Frame.Inverse();

            var dict = new Dictionary<Triangle, TriangleHL>();
            scene.Bodies.ForEach(body => ConvertTrianglesOfBody(body, dict, cameraFrame, camera.NearPlane));
            var trianglesHL = dict.Values.Where(triangle => triangle.Spin == TriangleSpin.counter_clockwise).ToArray();
            var edgesHL = scene.Bodies.SelectMany(body => body.ConvertToEdgeHL(dict, cameraFrame)).FilterOutBorderEdges().ToArray();

            var sceneHL = new SceneHL
            {
                NearPlaneDistance = camera.NearPlane,
                Edges = edgesHL,
                Triangles = trianglesHL
            };

            return sceneHL;
        }

        private static IEnumerable<EdgeHL> ConvertToEdgeHL(this Body body, Dictionary<Triangle, TriangleHL> dict, Matrix44D cameraFrame)
        {
            var cameraAndBodyFrame = cameraFrame * body.Frame;
            var edgesHL = body.Edges.Select(edge => ConvertToEdgeHL(edge, dict, cameraAndBodyFrame));
            return edgesHL;
        }

        private static EdgeHL ConvertToEdgeHL(Edge edge, Dictionary<Triangle, TriangleHL> dict, Matrix44D cameraAndBodyFrame)
        {
            var edgeHL = new EdgeHL()
            {
                Start = cameraAndBodyFrame * edge.Start.Position,
                End = cameraAndBodyFrame * edge.End.Position,
                First = dict.ContainsKey(edge.First.ParentTriangle) ? dict[edge.First.ParentTriangle] : null,
                Second = dict.ContainsKey(edge.Second.ParentTriangle) ? dict[edge.Second.ParentTriangle] : null,
                Edge = edge
            };

            return edgeHL;
        }

        private static void ConvertTrianglesOfBody(Body body, Dictionary<Triangle, TriangleHL> dict, Matrix44D cameraFrame, double nearPlane)
        {
            var cameraBodyFrame = cameraFrame * body.Frame;

            var trianglePairs = body.Faces.Select(face => ConvertFace(face, cameraBodyFrame, nearPlane)).SelectMany(d => d.ToList());
            trianglePairs.ToList().ForEach(pair => dict.Add(pair.Key, pair.Value));
        }

        private static Dictionary<Triangle, TriangleHL> ConvertFace(Face face, Matrix44D cameraBodyFrame, double nearPlane)
        {
            var faceHL = new FaceHL() { HasBorder = face.HasBorder, HasFacets = face.HasFacets };
            var triangleDict = face.Triangles.ToDictionary(t => t, t => ConvertTriangle(t, faceHL, cameraBodyFrame, nearPlane));
            return triangleDict;
        }

        private static TriangleHL ConvertTriangle(Triangle triangle, FaceHL face, Matrix44D cameraBodyFrame, double nearPlane)
        {
            // var normal = cameraBodyFrame * triangle.Normal;
            var p1 = cameraBodyFrame * triangle.P1.Point.Position;
            var p2 = cameraBodyFrame * triangle.P2.Point.Position;
            var p3 = cameraBodyFrame * triangle.P3.Point.Position;

            var ex = p2 - p1;
            var ey = p3 - p1;
            var normal = (ex & ey).Normalize();

            var triangleHL = new TriangleHL()
            {
                Normal = normal,
                P1 = p1,
                P2 = p2,
                P3 = p3,
                Face = face,
                Spin = DetermineTriangleSpin(p1, p2, p3, nearPlane),
                Triangle = triangle
            };

            return triangleHL;
        }

        private static TriangleSpin DetermineTriangleSpin(Position3D p1, Position3D p2, Position3D p3, double nearPlane)
        {
            var (x1, y1) = ViewProjection.ProjectCameraSystemToCameraPlane(p1, nearPlane);
            var (x2, y2) = ViewProjection.ProjectCameraSystemToCameraPlane(p2, nearPlane);
            var (x3, y3) = ViewProjection.ProjectCameraSystemToCameraPlane(p3, nearPlane);
            var area = 0.5 * (x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2));
            var result = area > 0.0 ? TriangleSpin.counter_clockwise : area < 0.0 ? TriangleSpin.clockwise : TriangleSpin.no_clockwise;
            return result;
        }
    }
}