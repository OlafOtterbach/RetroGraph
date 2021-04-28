using IGraphics.Mathematics;
using System;

namespace IGraphics.LogicViewing
{
    public class SelectedBodyState
    {
        public Guid SelectedBodyId { get; set; }

        public bool IsBodySelected { get; set; }

        public Position3D BodyIntersection { get; set; }
    }
}
