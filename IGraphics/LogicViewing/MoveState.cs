﻿using IGraphics.Graphics;
using IGraphics.Mathmatics;

namespace IGraphics.LogicViewing
{
    public class MoveState
    {
        public ILogicView View { get; set; } 
        public Body SelectedBody { get; set; }

        public double StartMoveX { get; set; }
        public double StartMoveY { get; set; }
        public Position3D StartMoveOffset { get; set; }
        public Vector3D StartMoveDirection { get; set; }
 
        public double EndMoveX { get; set; }
        public double EndMoveY { get; set; }
        public Position3D EndMoveOffset { get; set; }
        public Vector3D EndMoveDirection { get; set; }
 
        public Camera Camera { get; set; }
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }
    }
}
