using RetroGraph.Application.HiddenLine.Model;
using System.Collections.Generic;
using System.Linq;

namespace RetroGraph.Application.HiddenLine.Services
{
    public static class LineToCameraPlaneProjecting
    {
        public static IEnumerable<LineHL> ProjectToCameraPlane(this IEnumerable<EdgeHL> edges, double nearPlaneDist)
        {
            return edges.Select(edge => edge.ToLine2D(nearPlaneDist));
        }

        private static LineHL ToLine2D(this EdgeHL edge, double nearPlaneDist)
        {
            var start = edge.Start.ProjectCameraSystemToCameraPlane(nearPlaneDist);
            var end = edge.End.ProjectCameraSystemToCameraPlane(nearPlaneDist);
            var line2d = new LineHL { Start = start, End = end, Edge = edge };
            return line2d;
        }
    }
}
