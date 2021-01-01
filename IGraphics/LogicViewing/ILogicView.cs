using IGraphics.Graphics;
using System;

namespace IGraphics.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        Body SelectBody(SelectState selectState);

        Camera Select(SelectState selectState);

        Camera Move(MoveState moveState);

        Camera Zoom(ZoomState zoomState);
    }
}