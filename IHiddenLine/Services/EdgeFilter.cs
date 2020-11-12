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

            if (firstTriangle.HasParentFaceBorder || secondTriangle.HasParentFaceBorder)
                return true;

            var isFirstTriangleVisible = IsTriangleVisible(firstTriangle);
            var isSecondTriangleVisible = IsTriangleVisible(secondTriangle);
            var isLineBorderEdge = isFirstTriangleVisible != isSecondTriangleVisible;

            return isLineBorderEdge;
        }

        private static bool IsTriangleVisible(TriangleHL triangle)
            => triangle.Spin == TriangleSpin.counter_clockwise;
    }
}