using IGraphics.Graphics;

namespace IGraphics.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        SelectedBodyState SelectBody(SelectState selectState);

        Camera Touch(TouchState touchState);

        Camera Move(MoveState moveState);

        Camera Zoom(ZoomState zoomState);
    }
}