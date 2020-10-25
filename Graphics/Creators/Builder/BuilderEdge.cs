using System;

namespace RetroGraph.Graphics.Creators.Builder
{
    public struct BuilderEdge : IEquatable<BuilderEdge>
    {
        private int _hashCode;

        public BuilderEdge(BuilderVertex start, BuilderVertex end)
        {
            Start = start;
            End = end;

            // n = (x+1) + (y+1) - 2, key = n * (n+1)/2 + x
            var x = Start.Point.Position.GetHashCode() >= End.Point.Position.GetHashCode() ? Start.Point.Position.GetHashCode() : End.Point.Position.GetHashCode();
            var y = Start.Point.Position.GetHashCode() >= End.Point.Position.GetHashCode() ? End.Point.Position.GetHashCode() : Start.Point.Position.GetHashCode();

            var n = (x + 1) + (y + 1);
            _hashCode = n * (n + 1) / 2 + x;
        }

        public BuilderVertex Start { get; }

        public BuilderVertex End { get; }

        public override bool Equals(Object obj) => (obj is BuilderEdge) && (this.Equals((BuilderEdge)obj));

        public bool Equals(BuilderEdge other) => GetHashCode() == other.GetHashCode();

        public override int GetHashCode() => _hashCode;

        public static bool operator ==(BuilderEdge first, BuilderEdge second) => first.Equals(second);

        public static bool operator !=(BuilderEdge first, BuilderEdge second) => !first.Equals(second);
    }
}