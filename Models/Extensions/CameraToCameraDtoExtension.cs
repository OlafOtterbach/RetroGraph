using RetroGraph.LogicViewing;
using RetroGraph.Mathmatics;

namespace RetroGraph.Models.Extensions
{
    public static class CameraToCameraDtoExtension
    {
        public static CameraDto ToCameraDto(this Camera camera)
        {
            var cameraDto = new CameraDto();

            cameraDto.TargetDistance = (camera.Target - camera.Frame.Offset).Length;

            cameraDto.A11 = camera.Frame.A11;
            cameraDto.A21 = camera.Frame.A21;
            cameraDto.A31 = camera.Frame.A31;
            cameraDto.A41 = camera.Frame.A41;

            cameraDto.A12 = camera.Frame.A12;
            cameraDto.A22 = camera.Frame.A22;
            cameraDto.A32 = camera.Frame.A32;
            cameraDto.A42 = camera.Frame.A42;

            cameraDto.A13 = camera.Frame.A13;
            cameraDto.A23 = camera.Frame.A23;
            cameraDto.A33 = camera.Frame.A33;
            cameraDto.A43 = camera.Frame.A43;

            cameraDto.A14 = camera.Frame.A14;
            cameraDto.A24 = camera.Frame.A24;
            cameraDto.A34 = camera.Frame.A34;
            cameraDto.A44 = camera.Frame.A44;

            return cameraDto;
        }

        public static Camera ToCamera(this CameraDto cameraDto)
        {
            var frame = new Matrix44D(
                cameraDto.A11, cameraDto.A12, cameraDto.A13, cameraDto.A14,
                cameraDto.A21, cameraDto.A22, cameraDto.A23, cameraDto.A24,
                cameraDto.A31, cameraDto.A32, cameraDto.A33, cameraDto.A34,
                cameraDto.A41, cameraDto.A42, cameraDto.A43, cameraDto.A44);

            var position = frame.Offset;
            var direction = frame.Ey;
            var target = position + direction * cameraDto.TargetDistance;

            var camera = new Camera();
            camera.Frame = frame;
            camera.Target = target;
            camera.NearPlane = 1.0;

            return camera;
        }
    }
}
