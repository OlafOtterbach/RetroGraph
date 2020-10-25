using System;
using RetroGraph.Mathmatics;

namespace RetroGraph.Graphics
{
    public interface IGraphics
    {
        public Guid Id { get; }

        public Matrix44D Frame { get; set; }
    }
}