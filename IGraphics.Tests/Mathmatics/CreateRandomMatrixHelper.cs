using IGraphics.Mathmatics;
using System;

namespace IGraphics.Tests
{
   public class CreateRandomMatrixHelper
   {
      private Random _rand;

      public CreateRandomMatrixHelper()
      {
         _rand = new Random();
      }

      public Matrix44D CreateFrame()
      {
         var rotAlpha = Matrix44D.CreateRotation(new Vector3D(1, 0, 0), CreateAngle());
         var rotBeta = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), CreateAngle());
         var rotGamma = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), CreateAngle());
         var translation = Matrix44D.CreateTranslation(CreateVector());

         var randomMatrix = translation * rotAlpha * rotBeta * rotGamma;

         return randomMatrix;
      }

      private Vector3D CreateVector()
      {
         Vector3D vec;
         do
         {
            vec = new Vector3D((int)(_rand.NextDouble() * 100.0), (int)(_rand.NextDouble() * 100.0), (int)(_rand.NextDouble() * 100.0));
         }
         while (vec.Length == 0.0);

         return vec;
      }

      private double CreateAngle()
      {
         return ((double)((int)(_rand.NextDouble() * 360.0))).DegToRad();
      }
   }
}