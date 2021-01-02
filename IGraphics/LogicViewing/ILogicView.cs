using IGraphics.Graphics;
using IGraphics.Mathmatics;

namespace IGraphics.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        SelectedBodyState SelectBody(SelectState selectState);

        Camera Select(SelectState selectState);

        Camera Move(MoveState moveState);

        Camera Zoom(ZoomState zoomState);
    }
}