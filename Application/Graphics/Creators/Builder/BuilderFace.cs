using System.Collections.Generic;
using System.Linq;

namespace RetroGraph.Application.Graphics.Creators.Builder
{
    public class BuilderFace
    {
        public BuilderFace(IEnumerable<BuilderTriangle> triangles)
        {
            Triangles = triangles.ToArray();
            for(var index = 0; index < Triangles.Length; index++)
            {
                Triangles[index].Parent = this;
            }
        }

        public BuilderTriangle[] Triangles { get; }
        public bool HasBorder { get; set; }
    }
}