namespace IGraphics.Graphics.Creators.Builder
{
    public interface IBeginFaceAndEndBody
    {
        IBeginTriangleOrHasBorder BeginFace { get; }
        ICreateBody EndBody { get; }
    }
}