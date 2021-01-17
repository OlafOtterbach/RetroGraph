using System;

namespace RetroGraph.Models
{
    public class BodyStateDto
    {
        public Guid BodyId { get; set; }

        public CardanFrameDto Frame { get; set; }
    }
}
