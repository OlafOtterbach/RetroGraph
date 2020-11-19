using IGraphics.Mathmatics;
using System;

namespace IGraphics.Graphics.Creators.Creator
{
    public struct DoubleValue : IEquatable<DoubleValue>
    {
        public DoubleValue(double val) : this()
        {
            Value = val;
        }

        public double Value { get; }

        public override int GetHashCode()
        {
            var truncatedValue = Math.Truncate((1 / ConstantsMath.Epsilon) * Value);
            return truncatedValue.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            return (obj is DoubleValue) ? (this.Equals((DoubleValue)obj)) : false;
        }

        public bool Equals(DoubleValue other)
        {
            return Math.Abs(Value - other.Value) < ConstantsMath.Epsilon;
        }

        public static bool operator ==(DoubleValue first, DoubleValue second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(DoubleValue first, DoubleValue second)
        {
            return !(first.Equals(second));
        }

        public override string ToString()
        {
            var text = $"DoubleValue({Value}";
            return text;
        }
    }
}