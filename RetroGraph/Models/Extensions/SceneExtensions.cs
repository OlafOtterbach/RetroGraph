using IGraphics.Graphics;
using IGraphics.Mathematics.Extensions;
using System.Linq;

namespace RetroGraph.Models.Extensions
{
    public static class SceneExtensions
    {
        public static BodyStateDto[] GetBodyStates(this Scene scene)
        {
            var bodyStates = scene.Bodies.Select(body => new BodyStateDto { BodyId = body.Id, Frame = body.Frame.ToCardanFrame().ToCardanFrameDto() }).ToArray();
            return bodyStates;
        }

        public static void SetBodyStates(this Scene scene, BodyStateDto[] bodyStates)
        {
            foreach(var body in scene.Bodies)
            {
                var bodyState = bodyStates.FirstOrDefault(state => state.BodyId == body.Id);
                body.Frame = bodyState?.Frame?.ToCardanFrame().ToMatrix44D() ?? body.Frame;
            }

        }
    }
}
