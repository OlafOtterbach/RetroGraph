using System;

namespace IGraphics.Mathematics
{
    public struct Axis3D : IEquatable<Axis3D>
    {
        private readonly int _hashCode;

        public Axis3D(Position3D offset, Vector3D direction)
        {
            Offset = offset;
            Direction = direction;
            _hashCode = Offset.GetHashCode() * Direction.GetHashCode();
        }

        public Position3D Offset { get; }

        public Vector3D Direction { get; }

        public override bool Equals(object obj)
        {
            return (obj is Axis3D) && (this.Equals((Axis3D)obj));
        }

        public bool Equals(Axis3D other)
        {
            return Offset == other.Offset && Direction == other.Direction;
        }

        public static bool operator ==(Axis3D first, Axis3D second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Axis3D first, Axis3D second)
        {
            return !(first.Equals(second));
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}