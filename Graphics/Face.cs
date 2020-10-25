using System.Collections.Generic;
using System.Linq;

namespace RetroGraph.Graphics
{
    public class Face
    {
        public Triangle[] Triangles { get; set; }

        public bool HasBorder { get; set; }
    }
}