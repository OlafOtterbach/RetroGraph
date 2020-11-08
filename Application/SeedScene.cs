using RetroGraph.Application.Graphics;
using RetroGraph.Application.Graphics.Graphics.Creators;
using RetroGraph.Application.Mathmatics;

namespace RetroGraph.Application
{
    public static class SeedScene
    {
        public static Scene CreateAndPopulateScene()
        {
            var scene = new Scene();

            var cube = Cube.Create(10.0);
            cube.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 5));
            scene.Bodies.Add(cube);

            var cube2 = Cube.Create(10.0);
            cube2.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 15)) * Matrix44D.CreateRotation(new Vector3D(0, 0, 1), 45.0.DegToRad()); ;
            scene.Bodies.Add(cube2);

            var cylinder = Cylinder.Create(16, 4.0, 10.0);
            cylinder.Frame = Matrix44D.CreateTranslation(new Vector3D(10, 10, 5));
            scene.Bodies.Add(cylinder);

            var floor = Floor.Create(4, 20);
            scene.Bodies.Add(floor);

            return scene;
        }
    }
}