using IGraphics.LogicViewing;

namespace RetroGraph.Models.Extensions
{
    public static class BodySelectionDtoExtension
    {
        public static BodySelectionDto ToBodySelectionDto(this SelectedBodyState selectedBodyState)
        {
            return new BodySelectionDto
            {
                BodyId = selectedBodyState.SelectedBodyId,
                IsBodyIntersected = selectedBodyState.IsBodySelected,
                BodyIntersection = selectedBodyState.BodyIntersection.ToIntersectionDto()
            };
        }
    }
}
