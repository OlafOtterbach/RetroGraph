using IGraphics.Mathematics;
using System;

namespace IGraphics.Graphics
{
    public interface IGraphics
    {
        public Guid Id { get; }

        public Matrix44D Frame { get; set; }
    }
}