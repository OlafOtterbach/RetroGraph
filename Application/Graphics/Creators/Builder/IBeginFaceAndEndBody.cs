namespace RetroGraph.Application.Graphics.Creators.Builder
{
    public interface IBeginFaceAndEndBody
    {
        IBeginTriangleOrHasBorder BeginFace { get; }
        ICreateBody EndBody { get; }
    }
}