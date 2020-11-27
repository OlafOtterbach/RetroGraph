using System.Collections.Generic;
using System.Linq;
using IGraphics.Extensions;
using IGraphics.Mathmatics;

namespace IGraphics.Graphics.Creators.Creator
{
    public class GraphicsCreator
    {
        private PointStore _store;

        public GraphicsCreator()
        {
            Reset();
        }


        public void Reset()
        {
            _store = new PointStore();
            Faces = new List<CreatorFace>();
        }

        private List<CreatorFace> Faces { get; set;  } = new List<CreatorFace>();
        private CreatorFace CurrentFace => Faces.Last();

        public void AddFace(bool hasBorder, bool hasFacets)
        {
            Faces.Add(new CreatorFace() { HasBorder = hasBorder, HasFacets = hasFacets });
        }


        public void AddTriangle(Position3D p1, Position3D p2, Position3D p3)
        {
            AddTriangle(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z, p3.X, p3.Y, p3.Z);
        }

        public void AddTriangle(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            var point1 = _store.Get(x1, y1, z1);
            var point2 = _store.Get(x2, y2, z2);
            var point3 = _store.Get(x3, y3, z3);
            var normal = GetNormal(point1.Position, point2.Position, point3.Position);

            var triangle = new CreatorTriangle()
            {
                Normal = normal,
                P1 = new CreatorVertex() { Point = point1, Normal = normal },
                P2 = new CreatorVertex() { Point = point2, Normal = normal },
                P3 = new CreatorVertex() { Point = point3, Normal = normal }
            };

            CurrentFace.AddTriangle(triangle);
        }

        public Body CreateBody()
        {
            var faces = Faces.Convert().ToArray();
            var points = faces.SelectMany(face => face.Triangles).SelectMany(triangle => new Point3D[] { triangle.P1.Point, triangle.P2.Point, triangle.P3.Point }).Distinct().ToArray();
            var edges = GetEdges(faces).ToArray();

            var body = new Body()
            {
                Points = points,
                Faces = faces,
                Edges = edges
            };

            return body;
        }


        private static Vector3D GetNormal(Position3D p1, Position3D p2, Position3D p3)
        {
            var v1 = p2 - p1;
            var v2 = p3 - p1;
            var v3 = v1 & v2;
            var normal = v3.Normalize();
            return normal;
        }

        private static IEnumerable<Edge> GetEdges(IEnumerable<Face> faces)
        {
            var coedges = GetCoedges(faces).ToArray();
            var coedgeDict = CreateCounterParts(coedges);
            var edges = coedgeDict.Keys.Select(coedge => CreateEdge(coedge, coedgeDict));
            var filteredEdges = FilterEdges(edges).ToList();
            return filteredEdges;
        }

        private static IEnumerable<Edge> FilterEdges(IEnumerable<Edge> edges)
        {
            foreach (var edge in edges)
            {
                var has_same_coedges = edge.First == edge.Second;
                var parent_face_has_border = edge.First.ParentTriangle.ParentFace.HasBorder || edge.Second.ParentTriangle.ParentFace.HasBorder;
                var triangles_have_different_face = edge.First.ParentTriangle.ParentFace != edge.Second.ParentTriangle.ParentFace;
                var triangle_normals_are_different = edge.First.ParentTriangle.Normal != edge.Second.ParentTriangle.Normal;

                bool xxx = edge.First.ParentTriangle.ParentFace == edge.Second.ParentTriangle.ParentFace;

                var canBeVisible =    has_same_coedges && parent_face_has_border
                                   || !has_same_coedges && triangle_normals_are_different
                                   || triangles_have_different_face && parent_face_has_border;

                if (canBeVisible)
                {
                    yield return edge;
                }
            }
        }

        private static Edge CreateEdge(CoEdge coedge, Dictionary<CoEdge, CoEdge> counterPartDict)
        {
            var counterPart = counterPartDict.ContainsKey(coedge) ? counterPartDict[coedge] : coedge; ;
            var edge = new Edge()
            {
                First = coedge,
                Second = counterPart
            };

            return edge;
        }

        private static IEnumerable<CoEdge> GetCoedges(IEnumerable<Face> faces)
        {
            var coedges = faces.SelectMany(f => f.Triangles).SelectMany(triangle => triangle.CoEdges);
            return coedges;
        }

        private static Dictionary<CoEdge, CoEdge> CreateCounterParts(CoEdge[] coedges)
        {
            var dict = new Dictionary<CoEdge, CoEdge>();
            foreach (var coedge in coedges)
            {
                if (!dict.ContainsKey(coedge) && !dict.Values.Contains(coedge))
                {
                    var counterParts = coedges.Where(partner => partner != coedge && IsCounterPartCoedge(coedge, partner));
                    if (counterParts.Any())
                    {
                        dict[coedge] = counterParts.First();
                    }
                    else
                    {
                        dict[coedge] = coedge;
                    }
                }
            }

            return dict;
        }

        private static bool IsCounterPartCoedge(CoEdge first, CoEdge second)
        {
            if (first == second) return false;
            var isCounterPart = first.Start == second.End && first.End == second.Start || first.End == second.Start && first.Start == second.End;
            return isCounterPart;
        }
    }
}
