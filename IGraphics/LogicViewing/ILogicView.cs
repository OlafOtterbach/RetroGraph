using IGraphics.Graphics;

namespace IGraphics.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        SelectedBodyState SelectBody(SelectEvent selectEvent);

        Camera Touch(TouchState touchState);

        Camera Move(MoveEvent moveEvent);

        Camera Zoom(ZoomEvent zoomEvent);
    }
}