using IGraphics.Mathmatics;

namespace RetroGraph.Models
{
    public class CameraDto
    {
        public int Id { get; set; }

        public double TargetDistance { get; set; }

        public CardanFrameDto Frame { get; set; }
    }
}