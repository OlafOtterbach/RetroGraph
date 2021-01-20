using System;

namespace IGraphics.Mathmatics
{
    public struct Axis3D : IEquatable<Axis3D>
    {
        public Axis3D(Position3D offset, Vector3D direction)
        {
            Frame = Matrix44D.CreateRotation(offset, direction);
        }

        public Matrix44D Frame { get; }

        public Position3D Offset => Frame.Offset;

        public Vector3D Direction => Frame.Ez;

        public override int GetHashCode()
        {
            return Frame.GetHashCode();;
        }

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
    }
}