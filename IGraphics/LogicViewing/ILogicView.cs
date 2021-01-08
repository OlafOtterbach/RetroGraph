using IGraphics.Graphics;

namespace IGraphics.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        SelectedBodyState SelectBody(SelectEvent selectEvent);

        Camera Touch(TouchEvent touchEvent);

        Camera Move(MoveEvent moveEvent);

        Camera Zoom(ZoomEvent zoomEvent);
    }
}