using IGraphics.Graphics;
using IGraphics.Mathmatics;
using IHiddenLineGraphics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;
using System;

namespace RetroGraph.Controllers
{
    public class HomeController : Controller
    {
        private LogicHiddenLineViewService _logicView;
        ILogger<HomeController> _log;
        private Scene _scene;

        public HomeController(LogicHiddenLineViewService logicView, Scene scene, ILogger<HomeController> logger)
        {
            _logicView = logicView;
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

        [HttpPost("select-body")]
        public ActionResult SelectBody(double canvasX, [FromQuery] double canvasY, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var bodySelection = _logicView.SelectBody(canvasX, canvasY, canvasWidth, canvasHeight, cameraDto.ToCamera());
            return Ok(bodySelection);
        }

        [HttpPost("select")]
        public ActionResult Select(double canvasX, [FromQuery] double canvasY, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var graphics = _logicView.Select(canvasX, canvasY, canvasWidth, canvasHeight, cameraDto.ToCamera());
            graphics.Camera.Id = cameraDto.Id;
            return Ok(graphics);
        }

        [HttpPost("move")]
        public ActionResult Move([FromQuery] Guid bodyId, [FromQuery] double startX, [FromQuery] double startY, [FromQuery] double endX, [FromQuery] double endY, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var delta = Vector2DMath.Length(startX, startY, endX, endY);

            var graphics = _logicView.Move(bodyId, startX, startY, endX, endY, canvasWidth, canvasHeight, cameraDto.ToCamera());
            graphics.Camera.Id = cameraDto.Id;
            return Ok(graphics);
        }

        [HttpPost("zoom")]
        public ActionResult Zoom([FromQuery] double delta, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var graphics = _logicView.Zoom(delta, canvasWidth, canvasHeight, cameraDto.ToCamera());
            graphics.Camera.Id = cameraDto.Id;
            return Ok(graphics);
        }
    }
}