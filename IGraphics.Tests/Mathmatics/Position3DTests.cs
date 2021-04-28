using IGraphics.Mathematics;
using Xunit;

namespace IGraphics.Tests
{
   
   public class Position3DTests
   {
      [Fact]
      public void ConstructorTest_WhenDefaultConstructorIsCalled_ThenCoordinatesAreZero()
      {
         var pos = new Position3D();

         Assert.Equal(0.0, pos.X);
         Assert.Equal(0.0, pos.Y);
         Assert.Equal(0.0, pos.Z);
      }

      [Fact]
      public void ConstructorTest_WhenCalledWIthThreeCoordinatesXYZ_ThenPropertiesAreCoordinatesXYZ()
      {
         var pos = new Position3D(77.123, 99.456, 88.789);

         Assert.Equal(77.123, pos.X, ConstantsMath.Precision);
         Assert.Equal(99.456, pos.Y, ConstantsMath.Precision);
         Assert.Equal(88.789, pos.Z, ConstantsMath.Precision);
      }

      [Fact]
      public void ConstructorTest_WhenCalledWIthOtherPosition_ThenPropertiesAreContentOfPosition()
      {
         var other = new Position3D(77.123, 99.456, 88.789);

         var pos = new Position3D(other);

         Assert.Equal(77.123, pos.X, ConstantsMath.Precision);
         Assert.Equal(99.456, pos.Y, ConstantsMath.Precision);
         Assert.Equal(88.789, pos.Z, ConstantsMath.Precision);
      }

      [Fact]
      public void GetHashCodeTest_WhenTwoEqualPositionsCreated_ThenTheyHaveEqualHashCodes()
      {
         var first = new Position3D(77.123, 99.456, 88.789);
         var second = new Position3D(77.123, 99.456, 88.789);

         Assert.Equal(first.GetHashCode(), second.GetHashCode());
      }

      [Fact]
      public void GetHashCodeTest_WhenZeroPositionAndDefaultPositionCreated_ThenTheyHaveEqualHashCodes()
      {
         var first = new Position3D(0.0, 0.0, 0.0);
         var second = new Position3D();

         Assert.Equal(first.GetHashCode(), second.GetHashCode());
      }

      [Fact]
      public void GetHashCodeTest_WhenTwoDifferenPositionsCreated_ThenTheyHaveDifferentHashCodes()
      {
         var first = new Position3D(77.123, 99.456, 88.789);
         var second = new Position3D(88.789, 77.123, 99.456);

         Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
      }

      [Fact]
      public void EqualsObjectTest_WhenTwoEqualPositionsAreCompared_ThenTheAreEqual()
      {
         object first = new Position3D(77.123, 99.456, 88.789);
         object second = new Position3D(77.123, 99.456, 88.789);

         bool areEqual = first.Equals(second);

         Assert.True(areEqual);
      }

      [Fact]
      public void EqualsObjectTest_WhenTwoDifferentPositionsAreCompared_ThenTheNotEqual()
      {
         object first = new Position3D(77.123, 99.456, 88.789);
         object second = new Position3D(88.789, 77.123, 99.456);

         bool areEqual = first.Equals(second);

         Assert.False(areEqual);
      }

      [Fact]
      public void EqualsObjectTest_WhenPositionAndOtherTypeIsCompared_ThenTheNotEqual()
      {
         object first = new Position3D(77.123, 99.456, 88.789);
         object second = new Vector3D(77.123, 99.456, 88.789);

         bool areEqual = first.Equals(second);

         Assert.False(areEqual);
      }

      [Fact]
      public void EqualsObjectTest_WhenPositionAndNullIsCompared_ThenTheNotEqual()
      {
         object first = new Position3D(77.123, 99.456, 88.789);

         bool areEqual = first.Equals(null);

         Assert.False(areEqual);
      }

      [Fact]
      public void EqualsPositionTest_WhenTwoEqualPositionsAreCompared_ThenTheAreEqual()
      {
         var first = new Position3D(77.123, 99.456, 88.789);
         var second = new Position3D(77.123, 99.456, 88.789);

         bool areEqual = first.Equals(second);

         Assert.True(areEqual);
      }

      [Fact]
      public void EqualsPositionTest_WhenTwoDifferentPositionsAreCompared_ThenTheNotEqual()
      {
         var first = new Position3D(77.123, 99.456, 88.789);
         var second = new Position3D(88.789, 77.123, 99.456);

         bool areEqual = first.Equals(second);

         Assert.False(areEqual);
      }

      [Fact]
      public void EqualsOperatorTest_WhenTwoEqualPositionsAreCompared_ThenTheAreEqual()
      {
         var first = new Position3D(77.123, 99.456, 88.789);
         var second = new Position3D(77.123, 99.456, 88.789);

         bool areEqual = first == second;

         Assert.True(areEqual);
      }

      [Fact]
      public void EqualsOperatorTest_WhenTwoDifferentPositionsAreCompared_ThenTheNotEqual()
      {
         var first = new Position3D(77.123, 99.456, 88.789);
         var second = new Position3D(88.789, 77.123, 99.456);

         bool areEqual = first == second;

         Assert.False(areEqual);
      }

      [Fact]
      public void NotEqualsOperatorTest_WhenTwoEqualPositionsAreCompared_ThenTheAreEqual()
      {
         var first = new Position3D(77.123, 99.456, 88.789);
         var second = new Position3D(77.123, 99.456, 88.789);

         bool NotEqual = first != second;

         Assert.False(NotEqual);
      }

      [Fact]
      public void NotEqualsOperatorTest_WhenTwoDifferentPositionsAreCompared_ThenTheNotEqual()
      {
         var first = new Position3D(77.123, 99.456, 88.789);
         var second = new Position3D(88.789, 77.123, 99.456);

         bool NotEqual = first != second;

         Assert.True(NotEqual);
      }

      [Fact]
      public void MultiplicationOperatorTest_WhenPositionIsMultipliedWithScalar_ThenResultIsPositionCoordinatesAreMultipliedWithScalar()
      {
         var pos = new Position3D(1.0, 2.0, 3.0);

         var upScaledPos = pos * 2.0f;

         Assert.Equal(new Position3D(2.0, 4.0, 6.0), upScaledPos);
      }

      [Fact]
      public void DivisionOperatorTest_WhenPositionIsDividideByScalar_ThenResultIsPositionCoordinatesAreDividideByScalar()
      {
         var pos = new Position3D(2.0, 4.0, 6.0);

         var downScaledPos = pos / 2.0f;

         Assert.Equal(new Position3D(1.0, 2.0, 3.0), downScaledPos);
      }

      [Fact]
      public void AdditionOperatorTest_WhenVectorIsAddedToPosition_ThenVectorCoordinatesAreAddedToPositionCoordinates()
      {
         var pos = new Position3D(1.0, 2.0, 3.0);
         var vec = new Vector3D(1.0, 2.0, 3.0);

         var addedPos = pos + vec;

         Assert.Equal(new Position3D(2.0, 4.0, 6.0), addedPos);
      }

      [Fact]
      public void SubtractionOperatorTest_WhenVectorIsSubtractedFromPosition_ThenVectorCoordinatesAreSubtractedFromPositionCoordinates()
      {
         var pos = new Position3D(5.0, 7.0, 9.0);
         var vec = new Vector3D(1.0, 2.0, 3.0);

         var subtractedPos = pos - vec;

         Assert.Equal(new Position3D(4.0, 5.0, 6.0), subtractedPos);
      }

      [Fact]
      public void AdditionOperatorTest_WhenPositionIsAddedToVector_ThenVectorCoordinatesAreAddedToPositionCoordinates()
      {
         var vec = new Vector3D(1.0, 2.0, 3.0);
         var pos = new Position3D(1.0, 2.0, 3.0);

         var addedPos = vec + pos;

         Assert.Equal(new Position3D(2.0, 4.0, 6.0), addedPos);
      }

      [Fact]
      public void AdditionOperatorTest_WhenPositionIsAddedToPosition_ThenPositionCoordinatesAreAddedToPositionCoordinates()
      {
         var first = new Position3D(1.0, 2.0, 3.0);
         var second = new Position3D(1.0, 2.0, 3.0);

         var addedPos = first + second;

         Assert.Equal(new Position3D(2.0, 4.0, 6.0), addedPos);
      }

      [Fact]
      public void SubtractionOperatorTest_WhenPositionIsSubtractedFromPosition_ThenPositionCoordinatesAreSubtractedFromPositionCoordinates()
      {
         var first = new Position3D(5.0, 7.0, 9.0);
         var second = new Position3D(1.0, 2.0, 3.0);

         var subtractedPos = first - second;

         Assert.Equal(new Vector3D(4.0, 5.0, 6.0), subtractedPos);
      }

      [Fact]
      public void ToStringTest_WhenPositionIsConvertedToString_ThenResultIsPositionInAString()
      {
         var pos = new Position3D(5.0, 7.0, 9.0);

         var output = pos.ToString();

         Assert.Equal("Position3D(5, 7, 9)", output);
      }
   }
}