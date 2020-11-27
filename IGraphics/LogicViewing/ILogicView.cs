using IGraphics.Graphics;

namespace IGraphics.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        Camera Select(double canvasX, double canvasY, double canvasWidth, double canvasHeight, Camera camera);

        Camera Move(double startX, double startY, double endX, double endY, int canvasWidth, int canvasHeight, Camera camera);

        Camera Zoom(double pixelDeltaY, Camera camera);
    }
}