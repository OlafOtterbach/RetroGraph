﻿using IGraphics.Graphics;
using IGraphics.LogicViewing;
using IGraphics.Mathmatics;
using IHiddenLineGraphics;
using RetroGraph.Models;
using RetroGraph.Models.Extensions;
using System;
using System.Linq;

namespace RetroGraph.Services

{
    public class LogicHiddenLineViewService
    {
        private IHiddenLineService _hiddenLineService;
        private ILogicView _view;
        private Scene _scene;

        public LogicHiddenLineViewService(IHiddenLineService hiddenLineService, ILogicView view, Scene scene)
        {
            _hiddenLineService = hiddenLineService;
            _view = view;
            _scene = scene;
        }

        public GraphicsDto GetScene(int canvasWidth, int canvasHeight)
        {
            var camera = new Camera();
            camera.NearPlane = 1.0;

            camera.Frame = Matrix44D.CreateCoordinateSystem(
                new Position3D(-56.19932556152344, 77.98228454589844, 50.94441223144531),
                new Vector3D(-0.7851186990737915, -0.6140340566635132, 0.07365952432155609),
                new Vector3D(0.34082478284835815, -0.3296760022640228, 0.8801576495170593));


            //var rot = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), 10.0.DegToRad());
            //camera.Frame = rot * Matrix44D.CreateCoordinateSystem(
            //    new Position3D(0, 0, 100),
            //    new Vector3D(1.0, 0.0, 0.0),
            //    new Vector3D(0.0, 1.0, 0.0));

            var graphics = new GraphicsDto()
            {
                Camera = camera.ToCameraDto(),
                DrawLines = _hiddenLineService.GetHiddenLineGraphics(_scene, camera, canvasWidth, canvasHeight).ToArray()
            };

            return graphics;
        }

        public BodySelectionDto SelectBody(double canvasX, double canvasY, int canvasWidth, int canvasHeight, Camera camera)
        {
            var bodyId = _view.SelectBody(canvasX, canvasY, canvasWidth, canvasHeight, camera);
            var selection = new BodySelectionDto(bodyId);
            return selection;
        }

        public GraphicsDto Select(double canvasX, double canvasY, int canvasWidth, int canvasHeight, Camera camera)
        {
            var movedCamera = _view.Select(canvasX, canvasY, canvasWidth, canvasHeight, camera);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, movedCamera, canvasWidth, canvasHeight).ToArray();
            var graphics = new GraphicsDto()
            {
                Camera = movedCamera.ToCameraDto(),
                DrawLines = lines
            };

            return graphics;
        }

        public GraphicsDto Move(MoveState moveState)
        {
            var rotatedCamera = _view.Move(moveState);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, rotatedCamera, moveState.CanvasWidth, moveState.CanvasHeight).ToArray();
            var graphics = new GraphicsDto()
            {
                Camera = rotatedCamera.ToCameraDto(),
                DrawLines = lines
            };

            return graphics;
        }

        public GraphicsDto Zoom(double delta, int canvasWidth, int canvasHeight, Camera camera)
        {
            var zoomedCamera = _view.Zoom(delta, camera);
            var lines = _hiddenLineService.GetHiddenLineGraphics(_scene, zoomedCamera, canvasWidth, canvasHeight).ToArray();
            var graphics = new GraphicsDto()
            {
                Camera = zoomedCamera.ToCameraDto(),
                DrawLines = lines
            };

            return graphics;
        }
    }
}