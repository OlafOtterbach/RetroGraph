using IGraphics.Graphics;

namespace IGraphics.LogicViewing
{
    public interface ILogicView
    {
        public Scene Scene { get; }

        public Camera Select(double canvasX, double canvasY, double canvasWidth, double canvasHeight, Camera camera);

        public Camera Orbit(double pixelDeltaX, double pixelDeltaY, Camera camera);

        public Camera Zoom(double pixelDeltaY, Camera camera);
    }
}