using IGraphics.Mathematics;
using System;
using Xunit;

namespace IGraphics.Tests
{
   
   public class Vector3DTests
   {
      [Fact]
      public void ConstructorTest_WhenDefaultConstructorIsCalled_ThenCoordinatesAreZero()
      {
         var vec = new Vector3D();

         Assert.Equal(0.0, vec.X);
         Assert.Equal(0.0, vec.Y);
         Assert.Equal(0.0, vec.Z);
      }

      [Fact]
      public void ConstructorTest_WhenCalledWIthThreeCoordinatesXYZ_ThenPropertiesAreCoordinatesXYZ()
      {
         var vec = new Vector3D(77.123, 99.456, 88.789);

         Assert.Equal(77.123, vec.X, ConstantsMath.Precision);
         Assert.Equal(99.456, vec.Y, ConstantsMath.Precision);
         Assert.Equal(88.789, vec.Z, ConstantsMath.Precision);
      }

      [Fact]
      public void ConstructorTest_WhenCalledWithOtherVector_ThenPropertiesAreContentOfVectorD3()
      {
         var other = new Vector3D(77.123, 99.456, 88.789);

         var vec = new Vector3D(other);

         Assert.Equal(77.123, vec.X, ConstantsMath.Precision);
         Assert.Equal(99.456, vec.Y, ConstantsMath.Precision);
         Assert.Equal(88.789, vec.Z, ConstantsMath.Precision);
      }

      [Fact]
      public void LengthTest_WhenLengthIsCalledForValidVector_ThenLengthOfVectorIsResult()
      {
         var vec = new Vector3D(Math.Sqrt(2.0), Math.Sqrt(3.0), Math.Sqrt(4.0));

         var length = vec.Length;

         Assert.Equal(Math.Sqrt(2.0 + 3.0 + 4.0), length);
      }

      [Fact]
      public void GetHashCodeTest_WhenTwoEqualVectorsCreated_ThenTheyHaveEqualHashCodes()
      {
         var first = new Vector3D(77.123, 99.456, 88.789);
         var second = new Vector3D(77.123, 99.456, 88.789);

         Assert.Equal(first.GetHashCode(), second.GetHashCode());
      }

      [Fact]
      public void GetHashCodeTest_WhenZeroVectorAndDefaultVectorsCreated_ThenTheyHaveEqualHashCodes()
      {
         var first = new Vector3D(0.0, 0.0, 0.0);
         var second = new Vector3D();

         Assert.Equal(first.GetHashCode(), second.GetHashCode());
      }

      [Fact]
      public void GetHashCodeTest_WhenTwoDifferenVectorsCreated_ThenTheyHaveDifferentHashCodes()
      {
         var first = new Vector3D(77.123, 99.456, 88.789);
         var second = new Vector3D(88.789, 77.123, 99.456);

         Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
      }

      [Fact]
      public void EqualsObjectTest_WhenTwoEqualVectorsAreCompared_ThenTheAreEqual()
      {
         object first = new Vector3D(77.123, 99.456, 88.789);
         object second = new Vector3D(77.123, 99.456, 88.789);

         bool areEqual = first.Equals(second);

         Assert.True(areEqual);
      }

      [Fact]
      public void EqualsObjectTest_WhenTwoDifferentVectorsAreCompared_ThenTheNotEqual()
      {
         object first = new Vector3D(77.123, 99.456, 88.789);
         object second = new Vector3D(88.789, 77.123, 99.456);

         bool areEqual = first.Equals(second);

         Assert.False(areEqual);
      }

      [Fact]
      public void EqualsObjectTest_WhenVectorAndOtherTypeIsCompared_ThenTheNotEqual()
      {
         object first = new Vector3D(77.123, 99.456, 88.789);
         object second = new Position3D(77.123, 99.456, 88.789);

         bool areEqual = first.Equals(second);

         Assert.False(areEqual);
      }

      [Fact]
      public void EqualsObjectTest_WhenVectorAndNullIsCompared_ThenTheNotEqual()
      {
         object first = new Vector3D(77.123, 99.456, 88.789);

         bool areEqual = first.Equals(null);

         Assert.False(areEqual);
      }

      [Fact]
      public void EqualsVectorTest_WhenTwoEqualVectorsAreCompared_ThenTheAreEqual()
      {
         var first = new Vector3D(77.123, 99.456, 88.789);
         var second = new Vector3D(77.123, 99.456, 88.789);

         bool areEqual = first.Equals(second);

         Assert.True(areEqual);
      }

      [Fact]
      public void EqualsVectorTest_WhenTwoDifferentVectorsAreCompared_ThenTheNotEqual()
      {
         var first = new Vector3D(77.123, 99.456, 88.789);
         var second = new Vector3D(88.789, 77.123, 99.456);

         bool areEqual = first.Equals(second);

         Assert.False(areEqual);
      }

      [Fact]
      public void EqualsOperatorTest_WhenTwoEqualVectorsAreCompared_ThenTheAreEqual()
      {
         var first = new Vector3D(77.123, 99.456, 88.789);
         var second = new Vector3D(77.123, 99.456, 88.789);

         bool areEqual = first == second;

         Assert.True(areEqual);
      }

      [Fact]
      public void EqualsOperatorTest_WhenTwoDifferentVectorsAreCompared_ThenTheNotEqual()
      {
         var first = new Vector3D(77.123, 99.456, 88.789);
         var second = new Vector3D(88.789, 77.123, 99.456);

         bool areEqual = first == second;

         Assert.False(areEqual);
      }

      [Fact]
      public void NotEqualsOperatorTest_WhenTwoEqualVectorsAreCompared_ThenTheAreEqual()
      {
         var first = new Vector3D(77.123, 99.456, 88.789);
         var second = new Vector3D(77.123, 99.456, 88.789);

         bool NotEqual = first != second;

         Assert.False(NotEqual);
      }

      [Fact]
      public void NotEqualsOperatorTest_WhenTwoDifferentVectorsAreCompared_ThenTheNotEqual()
      {
         var first = new Vector3D(77.123, 99.456, 88.789);
         var second = new Vector3D(88.789, 77.123, 99.456);

         bool NotEqual = first != second;

         Assert.True(NotEqual);
      }

      [Fact]
      public void LengthTest_WhenVectorLengthIsCalled_ThenLengthIsSquareRootOfSummedSquaresOfCoordinates()
      {
         var vec = new Vector3D(2.0, 3.0, 5.0);

         var length = vec.Length;

         var expectedLength = Math.Sqrt(2.0 * 2.0 + 3.0 * 3.0 + 5.0 * 5.0);
         Assert.Equal(expectedLength, length, ConstantsMath.Precision);
      }

      [Fact]
      public void MultiplicationOperatorTest_TwoOrthogonalVectors_Zero()
      {
         Assert.Equal(new Vector3D(1, 0, 0) * new Vector3D(0, 1, 0), 0.0, 2);
         Assert.Equal(new Vector3D(1, 0, 0) * new Vector3D(0, 0, 1), 0.0);
         Assert.Equal(new Vector3D(0, 1, 0) * new Vector3D(0, 0, 1), 0.0);
         Assert.Equal(new Vector3D(0, 1, 0) * new Vector3D(1, 0, 0), 0.0);
         Assert.Equal(new Vector3D(0, 0, 1) * new Vector3D(1, 0, 0), 0.0);
         Assert.Equal(new Vector3D(0, 0, 1) * new Vector3D(0, 1, 0), 0.0);
         Assert.NotEqual(new Vector3D(1, 0, 1) * new Vector3D(1, 1, 0), 0.0);
      }

      [Fact]
      public void CrossPrductOperatorTest_VectorsOfWorldCoordinateSystem_OrthogonalVectorD3()
      {
         var ex = new Vector3D(1.0, 0.0, 0.0);
         var ey = new Vector3D(0.0, 1.0, 0.0);
         var ez = new Vector3D(0.0, 0.0, 1.0);

         var normal1 = ex & ey;
         var normal2 = ey & ez;
         var normal3 = ez & ex;

         Assert.Equal(ez, normal1);
         Assert.Equal(ex, normal2);
         Assert.Equal(ey, normal3);
      }

      [Fact]
      public void MultiplicationOperatorTest_WhenVectorIsMultipliedWithScalar_ThenResultIsVectorMultipliedWithScalar()
      {
         var vec = new Vector3D(1.0, 2.0, 3.0);

         var scaledVec = vec * 2.0f;

         Assert.Equal(new Vector3D(2.0, 4.0, 6.0), scaledVec);
      }

      [Fact]
      public void AdditionOperatorTest_WhenVectorIsAddedToVector_ThenVectorCoordinatesAreAddedToVectorCoordinates()
      {
         var first = new Vector3D(1.0, 2.0, 3.0);
         var second = new Vector3D(1.0, 2.0, 3.0);

         var addedVec = first + second;

         Assert.Equal(new Vector3D(2.0, 4.0, 6.0), addedVec);
      }

      [Fact]
      public void SubtractionOperatorTest_WhenVectorIsSubtractedFromVector_ThenVectorCoordinatesAreSubtractedFromVectorCoordinates()
      {
         var first = new Vector3D(5.0, 7.0, 9.0);
         var second = new Vector3D(1.0, 2.0, 3.0);

         var subtractedVec = first - second;

         Assert.Equal(new Vector3D(4.0, 5.0, 6.0), subtractedVec);
      }

      [Fact]
      public void NormalizeTest_WhenZeroVectorIsNormalized_ThenNormalizedVectorIsEx()
      {
         var vec = new Vector3D(0.0, 0.0, 0.0);

         var normal = vec.Normalize();

         Assert.Equal(float.NaN, normal.X);
         Assert.Equal(float.NaN, normal.Y);
         Assert.Equal(float.NaN, normal.Z);
      }

      [Fact]
      public void NormalizeTest_WhenVectorIsNormalized_ThenNormalizedVectorHasLengthOfOne()
      {
         var vec = new Vector3D(-2.0, 5.0, -13.0);

         var normal = vec.Normalize();

         Assert.Equal(1.0, normal.Length);
      }

      [Fact]
      public void ToStringTest_WhenVectorIsConvertedToString_ThenResultIsVectorInAString()
      {
         var vec = new Vector3D(5.0, 7.0, 9.0);

         var output = vec.ToString();

         Assert.Equal("Vector3D(5, 7, 9)", output);
      }
   }
}