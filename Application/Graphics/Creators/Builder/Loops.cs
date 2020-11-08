//using ScienceLibrary.Mathmatics.LinearAlgebra;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;

//namespace Graphics.Extensions
//{
//    public class Loops
//    {
//        private struct EdgeAndCount
//        {
//            public EdgeAndCount(Edge edge, int count)
//            {
//                Edge = edge;
//                Count = count;
//            }

//            public Edge Edge { get; }
//            public int Count { get; }
//        }

//        public List<List<Edge>> GetLoops(IEnumerable<Triangle> triangles, Position3D[] points)
//        {
//            var dict = GetTrianglesDictionary(triangles, points);
//            List<List<Edge>> loops = dict.Values.Select(GetLoops).ToList();

//            return loops;
//        }

//        private List<Edge> GetLoops(IEnumerable<Triangle> triangles)
//        {
//            var edges = triangles.SelectMany(GetEdges);
//            var dict = GetEdgeDictionary(edges);
//            var outerEdges = GetOuterEdges(dict);
//            var outerLoop = GetOuterLoop(outerEdges).ToList();

//            return outerLoop;
//        }

//        private static IEnumerable<Edge> GetEdges(Triangle triangle)
//        {
//            yield return new Edge(triangle.P1.PositionIndex, triangle.P2.PositionIndex);
//            yield return new Edge(triangle.P2.PositionIndex, triangle.P3.PositionIndex);
//            yield return new Edge(triangle.P3.PositionIndex, triangle.P1.PositionIndex);
//        }

//        private Dictionary<Vector3D, List<Triangle>> GetTrianglesDictionary(
//            IEnumerable<Triangle> triangles,
//            Position3D[] points)
//        {
//            var dict = new Dictionary<Vector3D, List<Triangle>>();
//            triangles.ToList().ForEach(x => Add(dict, x, points));
//            return dict;
//        }

//        private static void Add(Dictionary<Vector3D, List<Triangle>> dict, Triangle triangle, Position3D[] points)
//        {
//            var normal = GetNormal(points[triangle.P1.PositionIndex], points[triangle.P2.PositionIndex], points[triangle.P3.PositionIndex]);
//            if (!dict.ContainsKey(normal))
//            {
//                dict[normal] = new List<Triangle>();
//            }
//            dict[normal].Add(triangle);
//        }

//        private static Vector3D GetNormal(Position3D p1, Position3D p2, Position3D p3)
//        {
//            var v1 = p2 - p1;
//            var v2 = p3 - p1;
//            var v3 = v1 & v2;
//            var normal = v3.Normalize();
//            return normal;
//        }

//        private static IEnumerable<Edge> GetOuterEdges(ConcurrentDictionary<int, EdgeAndCount> dict) => dict.Values.Where(x => x.Count == 1).Select(x => x.Edge);

//        private static IEnumerable<Edge> GetOuterLoop(IEnumerable<Edge> outerEdges)
//        {
//            var dict = outerEdges.ToDictionary(x => x.StartIndex);
//            IList<Edge> seed = new List<Edge> { dict.First().Value };
//            var loop = Enumerable.Range(0, dict.Count > 0 ? dict.Count - 1 : 0)
//                                 .Aggregate(seed, (total, next) => Append(total, dict[total.Last().EndIndex]));
//            return loop;
//        }

//        public static IList<Edge> Append(IList<Edge> list, Edge edge)
//        {
//            list.Add(edge);
//            return list;
//        }

//        private static ConcurrentDictionary<int, EdgeAndCount> GetEdgeDictionary(IEnumerable<Edge> edges)
//        {
//            var dict = new ConcurrentDictionary<int, EdgeAndCount>();
//            edges.AsParallel().ToList().ForEach(e => dict.AddOrUpdate(e.ToUnorientedKey(), new EdgeAndCount(e, 1), (k, v) => new EdgeAndCount(v.Edge, v.Count + 1)));
//            return dict;
//        }
//    }
//}