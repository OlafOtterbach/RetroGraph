using System;

namespace RetroGraph.Models
{
    public class MoveStateDto
    {
        public Guid bodyId { get; set; }
        public double startX { get; set; }
        public double startY { get; set; }
        public double endX { get; set; }
        public double endY { get; set; }
        public int canvasWidth { get; set; }
        public int canvasHeight { get; set; }
        public CameraDto camera { get; set; }
    }
}
