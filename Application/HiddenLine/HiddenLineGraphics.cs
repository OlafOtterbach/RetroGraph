using RetroGraph.Application.HiddenLine.Model;
using RetroGraph.Application.HiddenLine.Services;
using RetroGraph.Application.Graphics;
using System.Collections.Generic;
using System.Linq;
using RetroGraph.Application.LogicViewing;

namespace RetroGraph.Application.HiddenLine
{
    public static class HiddenLineGraphics
    {
        public static IEnumerable<int> GetHiddenLineGraphics(this Scene scene, Camera camera, double canvasWidth, double canvasHeight)
        {
            // Convert to hidden line graphics scene
            var sceneHL = scene.ToHiddenLineScene(camera);

            // clipp 3D lines at near planle
            var clippedEdges = sceneHL.Edges.ClippAtNearPlane(sceneHL.NearPlaneDistance);

            // Project 3D lines on Nearplane to 2D lines
            var planeLines = clippedEdges.ProjectToCameraPlane(sceneHL.NearPlaneDistance);

            // Clipp lines at the projected screen limits
            var (left, bottom) = Projection.ProjectCanvasToCameraPlane(0, canvasHeight, canvasWidth, canvasHeight);
            var (right, top) = Projection.ProjectCanvasToCameraPlane(canvasWidth, 0, canvasWidth, canvasHeight);
            var clippedLines = planeLines.Clipp(left, right, bottom, top);

            // Cut 2D lines in uncutted 2D lines
            var cuttedLines = clippedLines.CutLines();

            var visibleLines = cuttedLines.FilterOutHiddenLines(sceneHL);

            var visibleLineCoordinates = visibleLines.SelectMany(line => line.ToLineCoordinates(canvasWidth, canvasHeight));

            return visibleLineCoordinates;
        }

        private static IEnumerable<int> ToLineCoordinates(this LineHL line, double canvasWidth, double canvasHeight)
        {
            var (x1, y1) = line.Start.ProjectCameraPlaneToCanvas(canvasWidth, canvasHeight);
            var (x2, y2) = line.End.ProjectCameraPlaneToCanvas(canvasWidth, canvasHeight);
            yield return x1;
            yield return y1;
            yield return x2;
            yield return y2;
        }
    }
}
