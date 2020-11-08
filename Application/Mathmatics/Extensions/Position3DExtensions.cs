namespace RetroGraph.Application.Mathmatics.Extensions
{
    public static class Position3DExtensions
    {
        public static Vector3D ToVector3D(this Position3D pos) => new Vector3D(pos.X, pos.Y, pos.Z);
    }
}