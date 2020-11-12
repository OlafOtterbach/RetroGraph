using IGraphics.Mathmatics;
using System.Collections.Generic;
using System.Linq;

namespace IGraphics.Graphics.Creators.Builder
{
    public class GraphicsBuilder
        : IBeginBody, IBeginFace, IBeginTriangle, IAddPoint1, IAddPoint2, IAddPoint3, IEndTriangle,
            IBeginTriangleAndEndFace, IBeginTriangleOrHasBorder, IBeginFaceAndEndBody, ICreateBody, IGraphicsBuilder
    {
        private Point3D _point1;
        private Point3D _point2;
        private Point3D _point3;
        private List<BuilderTriangle> _builderTriangles;
        private List<BuilderFace> _builderFaces;
        private PointStore _store;
        private bool _faceHasBorder;

        public GraphicsBuilder()
        {
            _builderTriangles = new List<BuilderTriangle>();
            _builderFaces = new List<BuilderFace>();
            _store = new PointStore();
        }

        public static IBeginBody Definition => new GraphicsBuilder();

        public IBeginFace BeginBody
        {
            get
            {
                _builderFaces = new List<BuilderFace>();
                return this;
            }
        }

        public IBeginTriangleOrHasBorder BeginFace
        {
            get
            {
                _builderTriangles = new List<BuilderTriangle>();
                _faceHasBorder = false;
                return this;
            }
        }

        public IBeginTriangle HasBorder
        {
            get
            {
                _faceHasBorder = true;
                return this;
            }
        }

        public IAddPoint1 BeginTriangle
        {
            get
            {
                return this;
            }
        }

        public IAddPoint2 P1(Position3D p1)
        {
            _point1 = _store.Get(p1);
            return this;
        }

        public IAddPoint3 P2(Position3D p2)
        {
            _point2 = _store.Get(p2);
            return this;
        }

        public IEndTriangle P3(Position3D p3)
        {
            _point3 = _store.Get(p3);
            return this;
        }

        public IAddPoint2 P1(double x, double y, double z)
        {
            _point1 = _store.Get(x, y, z);
            return this;
        }

        public IAddPoint3 P2(double x, double y, double z)
        {
            _point2 = _store.Get(x, y, z);
            return this;
        }

        public IEndTriangle P3(double x, double y, double z)
        {
            _point3 = _store.Get(x, y, z);
            return this;
        }

        public IBeginTriangleAndEndFace EndTriangle
        {
            get
            {
                var normal = GetNormal(_point1.Position, _point2.Position, _point3.Position);
                var triangle = new BuilderTriangle
                {
                    Normal = normal,
                    P1 = new BuilderVertex() { Point = _point1, Normal = normal },
                    P2 = new BuilderVertex() { Point = _point2, Normal = normal },
                    P3 = new BuilderVertex() { Point = _point3, Normal = normal }
                };
                _builderTriangles.Add(triangle);

                return this;
            }
        }

        public IBeginFaceAndEndBody EndFace
        {
            get
            {
                var face = new BuilderFace(_builderTriangles) { HasBorder = _faceHasBorder };
                _builderFaces.Add(face);
                return this;
            }
        }

        public ICreateBody EndBody => this;

        public Body CreateBody()
        {
            var faces = _builderFaces.Convert().ToArray();
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

                var canBeVisible = has_same_coedges && parent_face_has_border
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