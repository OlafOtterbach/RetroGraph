using IGraphics.Graphics;
using IGraphics.LogicViewing;
using System.Collections.Generic;

namespace IHiddenLineGraphics
{
    public interface IHiddenLineService
    {
        IEnumerable<int> GetHiddenLineGraphics(Scene scene, Camera camera, double canvasWidth, double canvasHeight);
    }
}