using System;
using System.Numerics;

namespace IGraphics.Mathmatics
{
    public struct Position3D : IEquatable<Position3D>
    {
        private Vector3 _vector;

        public Position3D(double x, double y, double z) : this()
        {
            _vector = new Vector3((float)x, (float)y, (float)z);
        }

        public Position3D(Position3D other) : this(other.X, other.Y, other.Z)
        { }

        internal Position3D(Vector3 other) : this()
        {
            _vector = other;
        }

        internal Vector3 Vector => _vector;

        public double X => _vector.X;
        public double Y => _vector.Y;
        public double Z => _vector.Z;

        public override int GetHashCode()
        {
            return _vector.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            return (obj is Position3D) && (this.Equals((Position3D)obj));
        }

        public bool Equals(Position3D other)
        {
            return (Math.Abs(Vector.X - other.Vector.X) < ConstantsMath.Epsilon)
                    && (Math.Abs(Vector.Y - other.Vector.Y) < ConstantsMath.Epsilon)
                    && (Math.Abs(Vector.Z - other.Vector.Z) < ConstantsMath.Epsilon);
        }

        public static bool operator ==(Position3D first, Position3D second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Position3D first, Position3D second)
        {
            return !first.Equals(second);
        }

        public static Position3D operator *(Position3D position, double scalar)
        {
            return new Position3D(Vector3.Multiply((float)scalar, position.Vector));
        }

        public static Position3D operator *(double scalar, Position3D position)
        {
            return new Position3D(Vector3.Multiply(position.Vector, (float)scalar));
        }

        public static Position3D operator /(Position3D position, double divisor)
        {
            return new Position3D(Vector3.Divide(position.Vector, (float)divisor));
        }

        public static Position3D operator +(Position3D first, Vector3D second)
        {
            return new Position3D(first.Vector + second.Vector);
        }

        public static Position3D operator -(Position3D first, Vector3D second)
        {
            return new Position3D(first.Vector - second.Vector);
        }

        public static Position3D operator +(Vector3D first, Position3D second)
        {
            return new Position3D(first.Vector + second.Vector);
        }

        public static Position3D operator +(Position3D first, Position3D second)
        {
            return new Position3D(first.Vector + second.Vector);
        }

        public static Vector3D operator -(Position3D first, Position3D second)
        {
            return new Vector3D(first.Vector - second.Vector);
        }

        public override string ToString()
        {
            var text = $"Position3D({X}, {Y}, {Z})";
            return text;
        }
    }
}