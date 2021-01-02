using IGraphics.Mathmatics;
using System;

namespace RetroGraph.Models
{
    public class BodySelectionDto
    {
        public bool IsBodyIntersected { get; set; }

        public Guid BodyId { get; set; }

        public IntersectionDto BodyIntersection { get; set; }
    }
}
