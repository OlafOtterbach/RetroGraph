using System;

namespace RetroGraph.Models
{
    public class BodySelectionDto
    {
        public BodySelectionDto(Guid bodyId)
        {
            BodyId = bodyId;
        }

        public Guid BodyId { get; }
    }
}
