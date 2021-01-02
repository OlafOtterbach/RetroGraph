using IGraphics.Mathmatics;

namespace RetroGraph.Models.Extensions
{
    public static class PositionToIntersectionDtoExtension
    {
        public static IntersectionDto ToIntersectionDto(this Position3D position)
        {
            return new IntersectionDto() { X = position.X, Y = position.Y, Z = position.Z };
        }
    }
}
