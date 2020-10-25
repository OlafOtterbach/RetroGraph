﻿using RetroGraph.Mathmatics;

namespace RetroGraph.Graphics.Creators.Builder
{
    public class BuilderBody
    {
        public Position3D[] Points { get; set; }
        public BuilderEdge[] Edges { get; set; }
        public BuilderFace[] Faces { get; set; }
    }
}