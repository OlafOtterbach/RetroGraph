using IGraphics.LogicViewing;
using IGraphics.Mathematics.Extensions;

namespace RetroGraph.Models.Extensions
{
    public static class CameraExtension
    {
        public static CameraDto ToCameraDto(this Camera camera)
        {
            var cameraDto = new CameraDto();

            cameraDto.TargetDistance = (camera.Target - camera.Frame.Offset).Length;

            cameraDto.Frame = camera.Frame.ToCardanFrame().ToCardanFrameDto();

            return cameraDto;
        }
    }
}