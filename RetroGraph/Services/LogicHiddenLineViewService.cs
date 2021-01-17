using IGraphics.Graphics;
using IGraphics.LogicViewing;
using IGraphics.Mathmatics;
using IHiddenLineGraphics;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;
using System.Linq;

namespace RetroGraph.Services

{
    public class LogicHiddenLineViewService
    {
        private IHiddenLineService _hiddenLineService;
        private ILogicView _view;
        private Scene _scene;
        private BodyStateDto[] _bodyStates;

        public LogicHiddenLineViewService(IHiddenLineService hiddenLineService, ILogicView view, Scene scene)
        {
            _hiddenLineService = hiddenLineService;
            _view = view;
            _scene = scene;
            _bodyStates = _scene.GetBodyStates();
        }

        public SceneStateDto GetScene(int canvasWidth, int canvasHeight)
        {
            var camera = new Camera();
            camera.NearPlane = 1.0;
            camera.Frame = Matrix44D.CreateCoordinateSystem(
                new Position3D(-56.19932556152344, 77.98228454589844, 50.94441223144531),
                new Vector3D(-0.7851186990737915, -0.6140340566635132, 0.07365952432155609),
                new Vector3D(0.34082478284835815, -0.3296760022640228, 0.8801576495170593));

            _scene.SetBodyStates(_bodyStates);

            var sceneState = new SceneStateDto()
            {
                Camera = camera.ToCameraDto(),
                BodyStates = _bodyStates,
                DrawLines = _hiddenLineService.GetHiddenLineGraphics(_scene, camera, canvasWidth, canvasHeight).ToArray()
            };

            return sceneState;
        }

        public SelectedBodyStateDto SelectBody(SelectEventDto selectEventDto)
        {
            var selectEvent = selectEventDto.ToSelectEvent();
            var selection = _view.SelectBody(selectEvent).ToBodySelectionDto();
            return selection;
        }

        public SceneStateDto Touch(TouchEventDto touchEventDto)
        {
            _scene.SetBodyStates(touchEventDto.BodyStates);
            var touchEvent = touchEventDto.ToTouchEvent();
            var touchCamera = _view.Touch(touchEvent);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, touchCamera, touchEvent.CanvasWidth, touchEvent.CanvasHeight).ToArray();
            var sceneState = new SceneStateDto()
            {
                Camera = touchCamera.ToCameraDto(),
                BodyStates = _scene.GetBodyStates(),
                DrawLines = lines
            };

            return sceneState;
        }

        public SceneStateDto Move(MoveEventDto moveEventDto)
        {
            _scene.SetBodyStates(moveEventDto.BodyStates);
            var moveEvent = moveEventDto.ToMoveEvent();
            var rotatedCamera = _view.Move(moveEvent);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, rotatedCamera, moveEvent.CanvasWidth, moveEvent.CanvasHeight).ToArray();
            var sceneState = new SceneStateDto()
            {
                Camera = rotatedCamera.ToCameraDto(),
                BodyStates = _scene.GetBodyStates(),
                DrawLines = lines
            };

            return sceneState;
        }

        public SceneStateDto Zoom(ZoomEventDto zoomEventDto)
        {
            _scene.SetBodyStates(zoomEventDto.BodyStates);
            var zoomEvent = zoomEventDto.ToZoomEvent();
            var zoomedCamera = _view.Zoom(zoomEvent);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, zoomedCamera, zoomEvent.CanvasWidth, zoomEvent.CanvasHeight).ToArray();
            var sceneState = new SceneStateDto()
            {
                Camera = zoomedCamera.ToCameraDto(),
                BodyStates = _scene.GetBodyStates(),
                DrawLines = lines
            };

            return sceneState;
        }
    }
}