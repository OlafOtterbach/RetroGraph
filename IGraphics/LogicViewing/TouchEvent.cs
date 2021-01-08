using IGraphics.Mathmatics;
using System;

namespace IGraphics.LogicViewing
{
    public class TouchEvent
    {
        public bool IsBodyTouched { get; set; }

        public Guid BodyId { get; set; }

        public Position3D TouchPosition { get; set; }

        public Camera Camera { get; set; }

        public int CanvasWidth { get; set; }

        public int CanvasHeight { get; set; }
    }
}