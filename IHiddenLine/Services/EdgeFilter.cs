using IHiddenLineGraphics.Model;
using System.Collections.Generic;
using System.Linq;

namespace IHiddenLineGraphics.Services
{
    public static class EdgeFilter
    {
        public static IEnumerable<EdgeHL> FilterOutBorderEdges(this IEnumerable<EdgeHL> edges)
        {
            return edges.Where(IsLineBorderEdge);
        }

        private static bool IsLineBorderEdge(EdgeHL edge)
        {
            var firstTriangle = edge.First;
            var secondTriangle = edge.Second;

            var isFirstTriangleVisible = IsTriangleVisible(firstTriangle);
            var isSecondTriangleVisible = IsTriangleVisible(secondTriangle);
            if (isFirstTriangleVisible != isSecondTriangleVisible) return true;

            if (((firstTriangle.Face.HasBorder || secondTriangle.Face.HasBorder) && firstTriangle.Face != secondTriangle.Face)
                || (firstTriangle.Face.HasFacets || secondTriangle.Face.HasFacets))
            {
                return true;
            }

            return false;
        }

        private static bool IsTriangleVisible(TriangleHL triangle)
            => triangle.Spin == TriangleSpin.counter_clockwise;
    }
}