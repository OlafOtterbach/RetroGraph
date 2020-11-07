﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroGraph.Graphics;
using RetroGraph.HiddenLine;
using RetroGraph.LogicViewing;
using RetroGraph.Mathmatics;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;

namespace RetroGraph.Controllers
{
    public class HomeController : Controller
    {
        private LogicView _logicView;
        private Scene _scene;

        public HomeController(LogicView logicView, Scene scene)
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

        //[HttpGet("initial-graphics/{canvasWidth}/{canvasHeight}")]
        [HttpGet("initial-graphics")]
        public ActionResult GetScene([FromQuery] int canvasWidth, [FromQuery] int canvasHeight)
        {
            var camera = new Camera();
            camera.NearPlane = 1.0;
            //camera.Frame = Matrix44D.CreateCoordinateSystem(new Position3D(-25.853, -32.245, 28.141), new Vector3D(0.855, -0.404, 0.322), new Vector3D(0.019, 0.648, 0.761));
            camera.Frame = Matrix44D.CreateCoordinateSystem(
                new Position3D(-56.19932556152344, 77.98228454589844, 50.94441223144531),
                new Vector3D(-0.7851186990737915, -0.6140340566635132, 0.07365952432155609),
                new Vector3D(0.34082478284835815, -0.3296760022640228, 0.8801576495170593));

            var graphics = new GraphicsDto()
            {
                DrawLines = HiddenLineGraphics.GetHiddenLineGraphics(_scene, camera, canvasWidth, canvasHeight).ToArray(),
                Camera = camera.ToCameraDto()
            };

            return Ok(graphics);
        }

        //[HttpGet("orbit/{camera}/{canvasWidth}/{canvasHeight}/{deltaX}/{deltaY}")]
        [HttpPost("orbit")]
        public ActionResult Orbit([FromQuery] int deltaX, [FromQuery] int deltaY, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var camera = _logicView.Orbit(deltaX, deltaY, cameraDto.ToCamera());
            var graphics = new GraphicsDto()
            {
                DrawLines = HiddenLineGraphics.GetHiddenLineGraphics(_scene, camera, canvasWidth, canvasHeight).ToArray(),
                Camera = camera.ToCameraDto()
            };

            return Ok(graphics);
        }

        [HttpPost("zoom")]
        public ActionResult Zoom([FromQuery] double delta, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var camera = _logicView.Zoom(delta, cameraDto.ToCamera());
            var graphics = new GraphicsDto()
            {
                DrawLines = HiddenLineGraphics.GetHiddenLineGraphics(_scene, camera, canvasWidth, canvasHeight).ToArray(),
                Camera = camera.ToCameraDto()
            };

            return Ok(graphics);
        }


        [HttpPost("select")]
        public ActionResult Select(double canvasX, [FromQuery] double canvasY, [FromQuery] int canvasWidth, [FromQuery] int canvasHeight, [FromBody] CameraDto cameraDto)
        {
            var camera = _logicView.Select(canvasX, canvasY, canvasWidth, canvasHeight, cameraDto.ToCamera());
            var graphics = new GraphicsDto()
            {
                DrawLines = HiddenLineGraphics.GetHiddenLineGraphics(_scene, camera, canvasWidth, canvasHeight).ToArray(),
                Camera = camera.ToCameraDto()
            };

            return Ok(graphics);
        }

    }
}

