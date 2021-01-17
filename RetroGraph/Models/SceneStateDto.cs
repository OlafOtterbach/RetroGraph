namespace RetroGraph.Models
{
    public class SceneStateDto
    {
        public CameraDto Camera { get; set; }

        public BodyStateDto[] BodyStates { get; set; }

        public int[] DrawLines { get; set; }
    }
}
