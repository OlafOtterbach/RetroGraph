using System;
using System.Linq;

namespace IGraphics.Graphics.Services
{
    public static class SceneExtensions
    {
        public static Body GetBody(this Scene scene, Guid bodyId)
        {
            var body = scene.Bodies.FirstOrDefault(body => body.Id == bodyId);
            return body;
        }
    }
}
