using IGraphics.Mathmatics;
using System;

namespace IGraphics.Graphics
{
    public class Body : IGraphics
    {
        public Body()
        {
            Id = Guid.NewGuid();
            Frame = Matrix44D.Identity;
        }

        public Guid Id { get; }

        public Matrix44D Frame { get; set; }
        
        public ISensor Sensor { get; set; }

        public Point3D[] Points { get; set; }
        public Face[] Faces { get; set; }
        public Edge[] Edges { get; set; }
    }
}