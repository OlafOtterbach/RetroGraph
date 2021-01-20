using System;

namespace IGraphics.Mathmatics
{
    public struct Plane3D : IEquatable<Plane3D>
    {
        public Plane3D(Position3D offset, Vector3D normal)
        {
            Offset = offset;
            Normal = normal;
        }

        public Position3D Offset { get; }

        public Vector3D Normal { get; }

        public override int GetHashCode()
        {
            return Offset.GetHashCode() * Normal.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is Plane3D) && (this.Equals((Plane3D)obj));
        }

        public bool Equals(Plane3D other)
        {
            return Offset == other.Offset && Normal == other.Normal;
        }

        public static bool operator ==(Plane3D first, Plane3D second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Plane3D first, Plane3D second)
        {
            return !(first.Equals(second));
        }
    }
}
