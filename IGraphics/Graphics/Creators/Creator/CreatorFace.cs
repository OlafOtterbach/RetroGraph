using System.Collections.Generic;

namespace IGraphics.Graphics.Creators.Creator
{
    public class CreatorFace
    {
        private List<CreatorTriangle> _triangles = new List<CreatorTriangle>();

        public CreatorTriangle[] Triangles => _triangles.ToArray();

        public bool HasBorder { get; set; }

        public void AddTriangle(CreatorTriangle triangle)
        {
            _triangles.Add(triangle);
            triangle.Parent = this;
        }
    }
}