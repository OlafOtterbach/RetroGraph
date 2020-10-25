using RetroGraph.Mathmatics;
using System.Collections.Generic;

namespace RetroGraph.Graphics.Creators.Builder
{
    public class PointStore
    {
        private readonly Dictionary<DoubleValue, Dictionary<DoubleValue, Dictionary<DoubleValue, Point3D>>> _dict;

        public PointStore()
        {
            _dict = new Dictionary<DoubleValue, Dictionary<DoubleValue, Dictionary<DoubleValue, Point3D>>>();
        }

        public Point3D Get(Position3D position)
            => Get(position.X, position.Y, position.Z);

        public Point3D Get(double x, double y, double z)
        {
            return Add(new DoubleValue(x), new DoubleValue(y), new DoubleValue(z), _dict);
        }

        private Point3D Add(DoubleValue x, DoubleValue y, DoubleValue z, Dictionary<DoubleValue, Dictionary<DoubleValue, Dictionary<DoubleValue, Point3D>>> dict)
        {
            if (!dict.ContainsKey(x))
            {
                var subDict = new Dictionary<DoubleValue, Dictionary<DoubleValue, Point3D>>();
                dict[x] = subDict;
            }

            return Add(y, z, x.Value, dict[x]);
        }

        private Point3D Add(DoubleValue y, DoubleValue z, double x, Dictionary<DoubleValue, Dictionary<DoubleValue, Point3D>> dict)
        {
            if (!dict.ContainsKey(y))
            {
                var subDict = new Dictionary<DoubleValue, Point3D>();
                dict[y] = subDict;
            }

            return Add(z, y.Value, x, dict[y]);
        }

        private Point3D Add(DoubleValue z, double y, double x, Dictionary<DoubleValue, Point3D> dict)
        {
            if (!dict.ContainsKey(z))
            {
                dict[z] = new Point3D() { Position = new Position3D(x, y, z.Value) };
            }

            return dict[z];
        }
    }
}