using IGraphics.Graphics;
using System;

namespace IGraphics.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        Guid SelectBody(double canvasX, double canvasY, int canvasWidth, int canvasHeight, Camera camera);

        Camera Select(double canvasX, double canvasY, double canvasWidth, double canvasHeight, Camera camera);

        Camera Move(Guid bodyId, double startX, double startY, double endX, double endY, int canvasWidth, int canvasHeight, Camera camera);

        Camera Zoom(double pixelDeltaY, Camera camera);
    }
}