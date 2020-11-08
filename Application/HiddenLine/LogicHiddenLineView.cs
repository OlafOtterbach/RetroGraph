using System.Linq;
using RetroGraph.Application.Graphics;
using RetroGraph.Application.LogicViewing;

namespace RetroGraph.Application.HiddenLine
{
    public class LogicHiddenLineView : LogicView
    {
        public LogicHiddenLineView(Scene scene) : base(scene) {}

        public (Camera, int[]) Orbit( double deltaX,  double deltaY,  int canvasWidth,  int canvasHeight, Camera camera)
        {
            var rotatedCamera = base.Orbit(deltaX, deltaY, camera);
            var lines = HiddenLineGraphics.GetHiddenLineGraphics(Scene, camera, canvasWidth, canvasHeight).ToArray();
            return (rotatedCamera, lines);
        }

        public (Camera, int[]) Zoom(double delta, int canvasWidth, int canvasHeight, Camera camera)
        {
            var zoomedCamera = base.Zoom(delta, camera);
            var lines = HiddenLineGraphics.GetHiddenLineGraphics(Scene, camera, canvasWidth, canvasHeight).ToArray();
            return (zoomedCamera, lines);
        }

        public (Camera, int[]) Select(double canvasX, double canvasY, int canvasWidth, int canvasHeight, Camera camera)
        {
            var movedCamera = base.Select(canvasX, canvasY, canvasWidth, canvasHeight, camera);
            var lines = HiddenLineGraphics.GetHiddenLineGraphics(Scene, camera, canvasWidth, canvasHeight).ToArray();
            return (movedCamera, lines);
        }
    }
}
