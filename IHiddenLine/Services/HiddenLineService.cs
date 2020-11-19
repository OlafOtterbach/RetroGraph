using IGraphics.Graphics;
using IGraphics.LogicViewing;
using IHiddenLineGraphics.Services;
using System.Collections.Generic;
using System.Linq;

namespace IHiddenLineGraphics
{
    public class HiddenLineService : IHiddenLineService
    {
        public IEnumerable<int> GetHiddenLineGraphics(Scene scene, Camera camera, double canvasWidth, double canvasHeight)
        {
            // Convert to hidden line graphics scene
            var sceneHL = scene.ToHiddenLineScene(camera);

            // clipp 3D lines at near planle
            var clippedEdges = sceneHL.Edges.ClippAtNearPlane(sceneHL.NearPlaneDistance);

            // Project 3D lines on Nearplane to 2D lines
            var planeLines = clippedEdges.ProjectToCameraPlane(sceneHL.NearPlaneDistance);

            // Filter lines with at least one visible triangle
            var visiblePlaneLines = planeLines.FilterLinesOfVisibleTriangles();

            // Clipp lines at the projected screen limits
            var (left, bottom) = Projection.ProjectCanvasToCameraPlane(0, canvasHeight, canvasWidth, canvasHeight);
            var (right, top) = Projection.ProjectCanvasToCameraPlane(canvasWidth, 0, canvasWidth, canvasHeight);
            var clippedLines = visiblePlaneLines.Clipp(left, right, bottom, top);

            // Cut 2D lines in uncutted 2D lines
            var cuttedLines = clippedLines.CutLines();

            var visibleLines = cuttedLines.FilterOutHiddenLines(sceneHL);

            var visibleLineCoordinates = visibleLines.SelectMany(line => line.ToLineCoordinates(canvasWidth, canvasHeight));

            return visibleLineCoordinates;
        }
    }
}