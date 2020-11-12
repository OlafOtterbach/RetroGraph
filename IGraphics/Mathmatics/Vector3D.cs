using System;
using System.Numerics;

namespace IGraphics.Mathmatics
{
    public struct Vector3D : IEquatable<Vector3D>
    {
        private Vector3 _vector;

        public Vector3D(double x, double y, double z) : this()
        {
            _vector = new Vector3((float)x, (float)y, (float)z);
        }

        public Vector3D(Vector3D other) : this(other.X, other.Y, other.Z)
        { }

        internal Vector3D(Vector3 other) : this()
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
            return (obj is Vector3D) && (this.Equals((Vector3D)obj));
        }

        public bool Equals(Vector3D other)
        {
            return (Math.Abs(Vector.X - other.Vector.X) < ConstantsMath.Epsilon)
                    && (Math.Abs(Vector.Y - other.Vector.Y) < ConstantsMath.Epsilon)
                    && (Math.Abs(Vector.Z - other.Vector.Z) < ConstantsMath.Epsilon);
        }

        public static bool operator ==(Vector3D first, Vector3D second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Vector3D first, Vector3D second)
        {
            return !(first.Equals(second));
        }

        public double Length => Vector.Length();

        /// <summary>
        /// Vector multiplication. Calculates a vector that is orthographic on the two vectors.
        /// </summary>
        /// <param name="first">First vector</param>
        /// <param name="second">Second vector</param>
        /// <returns>Crossproduct</returns>
        public static Vector3D operator &(Vector3D first, Vector3D second)
        {
            return new Vector3D(Vector3.Cross(first.Vector, second.Vector));
        }

        /// <summary>
        /// Scalar product.
        /// </summary>
        /// <param name="first">First vector</param>
        /// <param name="second">Second vector</param>
        /// <returns>double  result of multiplication</returns>
        public static double operator *(Vector3D first, Vector3D second)
        {
            return Vector3.Dot(first.Vector, second.Vector);
        }

        public static Vector3D operator *(Vector3D vector, double scalar)
        {
            return new Vector3D(Vector3.Multiply((float)scalar, vector.Vector));
        }

        public static Vector3D operator *(double scalar, Vector3D vector)
        {
            return new Vector3D(Vector3.Multiply(vector.Vector, (float)scalar));
        }

        public static Vector3D operator /(Vector3D vector, double divisor)
        {
            return new Vector3D(Vector3.Divide(vector.Vector, (float)divisor));
        }

        public static Vector3D operator +(Vector3D first, Vector3D second)
        {
            return new Vector3D(first.Vector + second.Vector);
        }

        public static Vector3D operator -(Vector3D first, Vector3D second)
        {
            return new Vector3D(first.Vector - second.Vector);
        }

        public Vector3D Normalize()
        {
            return new Vector3D(Vector3.Normalize(Vector));
        }

        public override string ToString()
        {
            var text = $"Vector3D({X}, {Y}, {Z})";
            return text;
        }
    }
}