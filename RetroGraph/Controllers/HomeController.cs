using IGraphics.Graphics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;
using RetroGraph.Services;

namespace RetroGraph.Controllers
{
    public class HomeController : Controller
    {
        private LogicHiddenLineViewService _logicView;
        private IConverterToMoveState _converterToMoveState;
        private IConverterToSelectState _converterToSelectState;
        private IConverterToZoomState _converterToZoomState;
        private ILogger<HomeController> _log;
        private Scene _scene;

        public HomeController(LogicHiddenLineViewService logicView,
            Scene scene,
            IConverterToMoveState converterToMoveState,
            IConverterToSelectState converterToSelectState,
            IConverterToZoomState converterToZoomState,
            ILogger<HomeController> logger)
        {
            _logicView = logicView;
            _converterToMoveState = converterToMoveState;
            _converterToSelectState = converterToSelectState;
            _converterToZoomState = converterToZoomState;
            _log = logger;
            _scene = scene;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("initial-graphics")]
        public ActionResult GetScene([FromQuery] int canvasWidth, [FromQuery] int canvasHeight)
        {
            return Ok(_logicView.GetScene(canvasWidth, canvasHeight));
        }

        [HttpPost("select")]
        public ActionResult<SelectedBodyStateDto> Select([FromBody] SelectStateDto selectStateDto)
        {
            var selectState = _converterToSelectState.Convert(selectStateDto);
            var bodySelection = _logicView.SelectBody(selectState);
            return Ok(bodySelection);
        }

        [HttpPost("touch")]
        public ActionResult<GraphicsDto> Touch([FromBody] TouchStateDto touchStateDto)
        {
            var graphics = _logicView.Touch(touchStateDto);
            graphics.Camera.Id = touchStateDto.Camera.Id;
            return Ok(graphics);
        }

        [HttpPost("move")]
        public ActionResult<GraphicsDto> Move([FromBody] MoveStateDto moveStateDto)
        {
            var moveState = _converterToMoveState.Convert(moveStateDto);
            var graphics = _logicView.Move(moveState);
            graphics.Camera.Id = moveStateDto.camera.Id;
            return Ok(graphics);
        }

        [HttpPost("zoom")]
        public ActionResult<GraphicsDto> Zoom([FromBody] ZoomStateDto zoomStateDto)
        {
            var zoomState = _converterToZoomState.Convert(zoomStateDto);
            var graphics = _logicView.Zoom(zoomState);
            graphics.Camera.Id = zoomStateDto.camera.Id;
            return Ok(graphics);
        }
    }
}