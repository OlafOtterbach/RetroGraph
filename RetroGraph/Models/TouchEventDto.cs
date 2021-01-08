using System;

namespace RetroGraph.Models
{
    public class TouchEventDto
    {
        public bool IsBodyTouched { get; set; }

        public Guid BodyId { get; set; }

        public PositionDto TouchPosition { get; set; }

        public CameraDto Camera { get; set; }

        public int CanvasWidth { get; set; }

        public int CanvasHeight { get; set; }
    }
}
