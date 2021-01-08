using IGraphics.Graphics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetroGraph.Models;
using RetroGraph.Services;

namespace RetroGraph.Controllers
{
    public class HomeController : Controller
    {
        private LogicHiddenLineViewService _logicView;
        private ILogger<HomeController> _log;

        public HomeController(LogicHiddenLineViewService logicView,
            ILogger<HomeController> logger)
        {
            _logicView = logicView;
            _log = logger;
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
        public ActionResult<SelectedBodyStateDto> Select([FromBody] SelectEventDto selectEventDto)
        {
            var selection = _logicView.SelectBody(selectEventDto);
            return Ok(selection);
        }

        [HttpPost("touch")]
        public ActionResult<GraphicsDto> Touch([FromBody] TouchStateDto touchStateDto)
        {
            var graphics = _logicView.Touch(touchStateDto);
            graphics.Camera.Id = touchStateDto.Camera.Id;
            return Ok(graphics);
        }

        [HttpPost("move")]
        public ActionResult<GraphicsDto> Move([FromBody] MoveEventDto moveEventDto)
        {
            var graphics = _logicView.Move(moveEventDto);
            graphics.Camera.Id = moveEventDto.Camera.Id;
            return Ok(graphics);
        }

        [HttpPost("zoom")]
        public ActionResult<GraphicsDto> Zoom([FromBody] ZoomEventDto zoomEventDto)
        {
            var graphics = _logicView.Zoom(zoomEventDto);
            graphics.Camera.Id = zoomEventDto.camera.Id;
            return Ok(graphics);
        }
    }
}