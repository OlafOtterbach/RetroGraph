namespace RetroGraph.Application.Graphics.Creators.Builder
{
    public interface IBeginTriangleAndEndFace
    {
        IAddPoint1 BeginTriangle { get; }
        IBeginFaceAndEndBody EndFace { get; }
    }
}