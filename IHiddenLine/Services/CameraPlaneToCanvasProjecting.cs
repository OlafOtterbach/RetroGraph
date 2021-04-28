using IGraphics.Mathematics;
using IHiddenLineGraphics.Model;
using System.Collections.Generic;

namespace IHiddenLineGraphics.Services
{
    public static class CameraPlaneToCanvasProjecting
    {
        public static IEnumerable<int> ToLineCoordinates(this LineHL line, double canvasWidth, double canvasHeight)
        {
            var (x1, y1) = ViewProjection.ProjectCameraPlaneToCanvas(line.Start.X, line.Start.Y, canvasWidth, canvasHeight);
            var (x2, y2) = ViewProjection.ProjectCameraPlaneToCanvas(line.End.X, line.End.Y, canvasWidth, canvasHeight);
            yield return x1;
            yield return y1;
            yield return x2;
            yield return y2;
        }
    }
}