namespace RetroGraph.Models
{
    public class SelectStateDto
    {
        public double selectPositionX { get; set; }
        public double selectPositionY { get; set; }
        public int canvasWidth { get; set; }
        public int canvasHeight { get; set; }
        public CameraDto camera { get; set; }
    }
}
