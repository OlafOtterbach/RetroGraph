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
        public ActionResult SelectBody([FromBody] SelectStateDto selectState)
        {
            var bodySelection = _logicView.SelectBody(selectState.selectPositionX, selectState.selectPositionY, selectState.canvasWidth, selectState.canvasHeight, selectState.camera.ToCamera());
            return Ok(bodySelection);
        }

        [HttpPost("select")]
        public ActionResult Select([FromBody] SelectStateDto selectState)
        {
            var graphics = _logicView.Select(selectState.selectPositionX, selectState.selectPositionY, selectState.canvasWidth, selectState.canvasHeight, selectState.camera.ToCamera());
            graphics.Camera.Id = selectState.camera.Id;
            return Ok(graphics);
        }

        [HttpPost("move")]
        public ActionResult Move([FromBody] MoveStateDto moveState)
        {
            var graphics = _logicView.Move(moveState.bodyId, moveState.startX, moveState.startY, moveState.endX, moveState.endY, moveState.canvasWidth, moveState.canvasHeight, moveState.camera.ToCamera());
            graphics.Camera.Id = moveState.camera.Id;
            return Ok(graphics);
        }

        [HttpPost("zoom")]
        public ActionResult Zoom([FromBody] ZoomStateDto zoomState)
        {
            var graphics = _logicView.Zoom(zoomState.delta, zoomState.canvasWidth, zoomState.canvasHeight, zoomState.camera.ToCamera());
            graphics.Camera.Id = zoomState.camera.Id;
            return Ok(graphics);
        }
    }
}