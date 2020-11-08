using System;
using RetroGraph.Application.Mathmatics;

namespace RetroGraph.Application.Graphics
{
    public interface IGraphics
    {
        public Guid Id { get; }

        public Matrix44D Frame { get; set; }
    }
}