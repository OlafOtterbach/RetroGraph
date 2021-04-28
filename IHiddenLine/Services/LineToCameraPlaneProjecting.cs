using IGraphics.Mathematics;
using IHiddenLineGraphics.Model;
using System.Collections.Generic;
using System.Linq;

namespace IHiddenLineGraphics.Services
{
    public static class LineToCameraPlaneProjecting
    {
        public static IEnumerable<LineHL> ProjectToCameraPlane(this IEnumerable<EdgeHL> edges, double nearPlaneDist)
        {
            return edges.Select(edge => edge.ToLine2D(nearPlaneDist));
        }

        private static LineHL ToLine2D(this EdgeHL edge, double nearPlaneDist)
        {
            var (x1, y1) = ViewProjection.ProjectCameraSystemToCameraPlane(edge.Start, nearPlaneDist);
            var (x2, y2) = ViewProjection.ProjectCameraSystemToCameraPlane(edge.End, nearPlaneDist);
            var line2d = new LineHL { Start = new PointHL(x1, y1), End = new PointHL(x2, y2), Edge = edge };
            return line2d;
        }
    }
}