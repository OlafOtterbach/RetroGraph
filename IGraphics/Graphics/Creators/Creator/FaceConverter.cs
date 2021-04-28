using IGraphics.Extensions;
using IGraphics.Mathematics;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace IGraphics.Graphics.Creators.Creator
{
    public static class FaceConverter
    {
        public static IEnumerable<Face> Convert(this IEnumerable<CreatorFace> CreatorFaces)
        {
            var faces = CreatorFaces.Select(bf => Convert(bf));
            return faces;
        }

        private static Face Convert(CreatorFace CreatorFace)
        {
            var CreatorTriangles = CreatorFace.Triangles.ToList();
            var vertices = CreatorTriangles.SelectMany(GetTrianglesVertices).ToList();
            var dict = new ConcurrentDictionary<Point3D, HashSet<Vector3D>>();
            vertices.AsParallel()
                    .ToList()
                    .ForEach(v => dict.AddOrUpdate(v.Point,
                                                    (k, v) => Create(v.Normal),
                                                    (k, h, val) => Add(h, val.Normal),
                                                    v
                                                  )
                            );
            var vertexDict = dict.ToDictionary(p => p.Key, p => Aggregate(p.Value));
            var triangles = CreatorTriangles.Select(bt => ConvertToTriangle(bt, vertexDict)).ToArray();

            var face = new Face() { Triangles = triangles, HasBorder = CreatorFace.HasBorder, HasFacets = CreatorFace.HasFacets };

            triangles.ForEach(triangle => triangle.ParentFace = face);

            return face;
        }

        private static Vector3D Aggregate(HashSet<Vector3D> vectors)
        {
            var aggregatedVector = (vectors.Aggregate((x, y) => x + y) / vectors.Count).Normalize();
            return aggregatedVector;
        }

        private static HashSet<Vector3D> Add(HashSet<Vector3D> hashSet, Vector3D value)
        {
            hashSet.Add(value);
            return hashSet;
        }

        private static HashSet<Vector3D> Create(Vector3D value)
        {
            var hashset = new HashSet<Vector3D>();
            hashset.Add(value);
            return hashset;
        }

        private static IEnumerable<CreatorVertex> GetTrianglesVertices(CreatorTriangle triangle)
        {
            yield return triangle.P1;
            yield return triangle.P2;
            yield return triangle.P3;
        }

        private static Triangle ConvertToTriangle(
            CreatorTriangle CreatorTriangle,
            Dictionary<Point3D, Vector3D> verticesAndNormals)
        {
            var p1 = new Vertex()
            {
                Point = CreatorTriangle.P1.Point,
                Normal = verticesAndNormals[CreatorTriangle.P1.Point]
            };

            var p2 = new Vertex()
            {
                Point = CreatorTriangle.P2.Point,
                Normal = verticesAndNormals[CreatorTriangle.P2.Point]
            };

            var p3 = new Vertex()
            {
                Point = CreatorTriangle.P3.Point,
                Normal = verticesAndNormals[CreatorTriangle.P3.Point]
            };

            var coEdges = new CoEdge[3];
            coEdges[0] = new CoEdge { Start = p1.Point, End = p2.Point };
            coEdges[1] = new CoEdge { Start = p2.Point, End = p3.Point };
            coEdges[2] = new CoEdge { Start = p3.Point, End = p1.Point };

            var triangle = new Triangle
            {
                P1 = p1,
                P2 = p2,
                P3 = p3,
                Normal = CreatorTriangle.Normal,
                CoEdges = coEdges
            };

            coEdges.ForEach(coedge => coedge.ParentTriangle = triangle);

            return triangle;
        }
    }
}