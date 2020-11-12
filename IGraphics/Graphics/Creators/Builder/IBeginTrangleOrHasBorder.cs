namespace IGraphics.Graphics.Creators.Builder
{
    public interface IBeginTriangleOrHasBorder
    {
        IBeginTriangle HasBorder { get; }
        IAddPoint1 BeginTriangle { get; }
    }
}