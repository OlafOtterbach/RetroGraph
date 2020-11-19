using System;

namespace IGraphics.Graphics.Creators.Creator
{
    public struct CreatorEdge : IEquatable<CreatorEdge>
    {
        private int _hashCode;

        public CreatorEdge(CreatorVertex start, CreatorVertex end)
        {
            Start = start;
            End = end;

            // n = (x+1) + (y+1) - 2, key = n * (n+1)/2 + x
            var x = Start.Point.Position.GetHashCode() >= End.Point.Position.GetHashCode() ? Start.Point.Position.GetHashCode() : End.Point.Position.GetHashCode();
            var y = Start.Point.Position.GetHashCode() >= End.Point.Position.GetHashCode() ? End.Point.Position.GetHashCode() : Start.Point.Position.GetHashCode();

            var n = (x + 1) + (y + 1);
            _hashCode = n * (n + 1) / 2 + x;
        }

        public CreatorVertex Start { get; }

        public CreatorVertex End { get; }

        public override bool Equals(Object obj) => (obj is CreatorEdge) && (this.Equals((CreatorEdge)obj));

        public bool Equals(CreatorEdge other) => GetHashCode() == other.GetHashCode();

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(CreatorEdge first, CreatorEdge second) => first.Equals(second);

        public static bool operator !=(CreatorEdge first, CreatorEdge second) => !first.Equals(second);
    }
}