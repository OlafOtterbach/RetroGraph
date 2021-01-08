using System;

namespace RetroGraph.Models
{
    public class MoveEventDto
    {
        public Guid BodyId { get; set; }
        public PositionDto BodyIntersection { get; set; }
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
        public CameraDto Camera { get; set; }
    }
}
