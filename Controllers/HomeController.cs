using IGraphics.Graphics;
using IHiddenLineGraphics;
using Microsoft.AspNetCore.Mvc;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;

namespace RetroGraph.Controllers
{
    public class HomeController : Controller
    {
        private LogicHiddenLineViewService _logicView;
        private Scene _scene;

        public HomeController(LogicHiddenLineViewService logicView, Scene scene)
        {
            _logicView = logicView;
            _scene = scene;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("initial-graphics")]
        public ActionResult GetScene([FromQuery] int canvasWidth, [FromQuery] int canvasHeight)
        {
            return Ok(_logicView.GetScene(canvasWidth, canvasHeight));
        }

        [HttpPost("orbit")]
        public ActionResult Orbit([FromQuery] double deltaX, [FromQuery] double deltaY, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var graphics = _logicView.Orbit(deltaX, deltaY, canvasWidth, canvasHeight, cameraDto.ToCamera());
            return Ok(graphics);
        }

        [HttpPost("zoom")]
        public ActionResult Zoom([FromQuery] double delta, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var graphics = _logicView.Zoom(delta, canvasWidth, canvasHeight, cameraDto.ToCamera());
            return Ok(graphics);
        }

        [HttpPost("select")]
        public ActionResult Select(double canvasX, [FromQuery] double canvasY, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var graphics = _logicView.Select(canvasX, canvasY, canvasWidth, canvasHeight, cameraDto.ToCamera());
            return Ok(graphics);
        }
    }
}