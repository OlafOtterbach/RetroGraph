using IGraphics.Graphics;
using IGraphics.Mathmatics;

namespace IGraphics.LogicViewing
{
    public class MoveState
    {
        public Body SelectedBody { get; set; }
        public Position3D BodyIntersection { get; set; }
        public double StartMoveX { get; set; }
        public double StartMoveY { get; set; }
        public Position3D StartMoveOffset { get; set; }
        public Vector3D StartMoveDirection { get; set; }
 
        public double EndMoveX { get; set; }
        public double EndMoveY { get; set; }
        public Position3D EndMoveOffset { get; set; }
        public Vector3D EndMoveDirection { get; set; }
 
        public Camera Camera { get; set; }
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
    }
}
