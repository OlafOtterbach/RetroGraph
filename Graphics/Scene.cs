using System.Collections.Generic;

namespace RetroGraph.Graphics
{
    public class Scene
    {
        public Scene()
        {
            Bodies = new List<Body>();
        }

        public List<Body> Bodies { get; }
    }
}
