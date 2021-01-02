using IGraphics.Mathmatics;

namespace RetroGraph.Models.Extensions
{
    public static class IntersectionDtoExtension
    {
        public static Position3D ToPosition3D(this PositionDto positionDto)
        {
            return new Position3D(positionDto.X, positionDto.Y, positionDto.Z);
        }
    }
}
