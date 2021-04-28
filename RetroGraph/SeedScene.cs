using IGraphics.Graphics;
using IGraphics.Graphics.Creators;
using IGraphics.Mathematics;

namespace RetroGraph
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
            cube2.Sensor = new CylinderSensor(new Vector3D(0, 0, 1));
            scene.Bodies.Add(cube2);

            var cylinder = Cylinder.Create(16, 4.0, 10.0);
            cylinder.Frame = Matrix44D.CreateTranslation(new Vector3D(10, 10, 5.1));
            cylinder.Sensor = new LinearSensor(new Vector3D(0, 1, 0));
            scene.Bodies.Add(cylinder);

            var floor = Floor.Create(4, 20);
            scene.Bodies.Add(floor);


            double[][] segments = new double[][]
            {
                new [] { 0.0,  0.0, 5.0, 0.0 },
                new [] { 5.0,  0.0, 5.0, 1.0 },
                new [] { 5.0,  1.0, 1.0, 5.0 },
                new [] { 1.0,  5.0, 1.0, 7.0 },
                new [] { 1.0,  7.0, 5.0, 11.0},
                new [] { 5.0, 11.0, 5.0, 12.0},
                new [] { 5.0, 12.0, 0.0, 12.0},
            };
            bool[] borderFlags = new bool[] { true, true, true, true, true, true, true };
            bool[] facetsFlags = new bool[] { false, false, false, false, false, false, false };

            var rotationBody = RotationBody.Create(16, segments, borderFlags, facetsFlags);
            rotationBody.Sensor = new PlaneSensor(new Vector3D(0, 0, 1));
            rotationBody.Frame = Matrix44D.CreateTranslation(new Vector3D(30, -30, 0.1));
            scene.Bodies.Add(rotationBody);

            var sphere = Sphere.Create(16, 8);
            sphere.Sensor = new SphereSensor();
            sphere.Frame = Matrix44D.CreateTranslation(new Vector3D(-30, 30, 10));
            scene.Bodies.Add(sphere);

            return scene;
        }
    }
}