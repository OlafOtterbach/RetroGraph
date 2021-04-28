using IGraphics.Mathematics;
using IGraphics.Mathematics.Extensions;
using System;
using System.Linq;
using System.Numerics;
using Xunit;

namespace IGraphics.Tests
{
    
    public class Matrix44DTests
    {
        private Random _rand;

        
        public Matrix44DTests()
        {
            _rand = new Random();
        }

        [Fact]
        public void ConstructorTest_WhenMatrixIsCreated_ThenElementsAreEqualToParameters()
        {
            var mat = new Matrix44D(1.1, 1.2, 1.3, 1.4,
                                    2.1, 2.2, 2.3, 2.4,
                                    3.1, 3.2, 3.3, 3.4,
                                    4.1, 4.2, 4.3, 4.4);

            Assert.Equal(1.1, mat.A11, ConstantsMath.Precision);
            Assert.Equal(1.2, mat.A12, ConstantsMath.Precision);
            Assert.Equal(1.3, mat.A13, ConstantsMath.Precision);
            Assert.Equal(1.4, mat.A14, ConstantsMath.Precision);
            Assert.Equal(2.1, mat.A21, ConstantsMath.Precision);
            Assert.Equal(2.2, mat.A22, ConstantsMath.Precision);
            Assert.Equal(2.3, mat.A23, ConstantsMath.Precision);
            Assert.Equal(2.4, mat.A24, ConstantsMath.Precision);
            Assert.Equal(3.1, mat.A31, ConstantsMath.Precision);
            Assert.Equal(3.2, mat.A32, ConstantsMath.Precision);
            Assert.Equal(3.3, mat.A33, ConstantsMath.Precision);
            Assert.Equal(3.4, mat.A34, ConstantsMath.Precision);
            Assert.Equal(4.1, mat.A41, ConstantsMath.Precision);
            Assert.Equal(4.2, mat.A42, ConstantsMath.Precision);
            Assert.Equal(4.3, mat.A43, ConstantsMath.Precision);
            Assert.Equal(4.4, mat.A44, ConstantsMath.Precision);
        }

        [Fact]
        public void ExEyEzTranslationTest_WhenMatrixIsCreated_ThenExEyEzAndTranslationAreColumnsOfMatrix()
        {
            var mat = new Matrix44D(1.1, 1.2, 1.3, 1.4,
                                    2.1, 2.2, 2.3, 2.4,
                                    3.1, 3.2, 3.3, 3.4,
                                    0.0, 0.0, 0.0, 1.0);

            var expectedEx = new Vector3D(1.1, 2.1, 3.1);
            var expectedEy = new Vector3D(1.2, 2.2, 3.2);
            var expectedEz = new Vector3D(1.3, 2.3, 3.3);
            var expectedOffset = new Position3D(1.4, 2.4, 3.4);
            Assert.Equal(expectedEx, mat.Ex);
            Assert.Equal(expectedEy, mat.Ey);
            Assert.Equal(expectedEz, mat.Ez);
            Assert.Equal(expectedOffset, mat.Offset);
            Assert.Equal(expectedOffset.ToVector3D(), mat.Translation);
        }

        [Fact]
        public void AffineTest_WhenAffineMatrixIsRead_ThenAffineIsMatrixWithoutTranslation()
        {
            var mat = new Matrix44D(1.1, 1.2, 1.3, 1.4,
                                    2.1, 2.2, 2.3, 2.4,
                                    3.1, 3.2, 3.3, 3.4,
                                    4.1, 4.2, 4.3, 4.4);

            var affine = mat.Affine;

            Assert.Equal(mat.Ex, affine.Ex);
            Assert.Equal(mat.Ey, affine.Ey);
            Assert.Equal(mat.Ez, affine.Ez);
            Assert.Equal(new Position3D(0.0, 0.0, 0.0), affine.Offset);
            Assert.Equal(new Vector3D(0.0, 0.0, 0.0), affine.Translation);
            Assert.Equal(0.0, affine.A41, ConstantsMath.Precision);
            Assert.Equal(0.0, affine.A42, ConstantsMath.Precision);
            Assert.Equal(0.0, affine.A43, ConstantsMath.Precision);
            Assert.Equal(1.0, affine.A44, ConstantsMath.Precision);
        }

        [Fact]
        public void ObjectEqualsTest_WhenTwoIdentMatricesAreCompared_ThenResultIsTrue()
        {
            object first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            object second = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);

            Assert.True(object.Equals(first, second));
        }

        [Fact]
        public void ObjectEqualsTest_WhenTwoDifferentMatricesAreCompared_ThenResultIsFalse()
        {
            object first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            object second = new Matrix44D(7.1, 8.2, 9.9, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);

            Assert.False(object.Equals(first, second));
        }

        [Fact]
        public void EqualsTest_WhenTwoIdentMatricesAreCompared_ThenResultIsTrue()
        {
            Matrix44D first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            Matrix44D second = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);

            Assert.True(first.Equals(second));
        }

        [Fact]
        public void EqualsTest_WhenTwoDifferentMatricesAreCompared_ThenResultIsFalse()
        {
            Matrix44D first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            Matrix44D second = new Matrix44D(7.1, 8.2, 9.9, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);

            Assert.False(first.Equals(second));
        }

        [Fact]
        public void EqualsOperatorTest_WhenTwoIdentMatricesAreCompared_ThenResultIsTrue()
        {
            Matrix44D first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            Matrix44D second = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);

            Assert.True(first == second);
        }

        [Fact]
        public void EqualsOperatorTest_WhenTwoDifferentMatricesAreCompared_ThenResultIsFalse()
        {
            Matrix44D first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            Matrix44D second = new Matrix44D(7.1, 8.2, 9.9, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);

            Assert.False(first == second);
        }

        [Fact]
        public void NotEqualsOperatorTest_WhenTwoIdentMatricesAreCompared_ThenResultIsFalse()
        {
            Matrix44D first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            Matrix44D second = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);

            Assert.False(first != second);
        }

        [Fact]
        public void NotEqualsOperatorTest_WhenTwoDifferentMatricesAreCompared_ThenResultIsTrue()
        {
            Matrix44D first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            Matrix44D second = new Matrix44D(7.1, 8.2, 9.9, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);

            Assert.True(first != second);
        }

        [Fact]
        public void MultiplicationOperatorTest_WhenMatrixMultpliedWithXAxis_ThenResultVectorIsExOfMatrix()
        {
            var ex = new Vector3D(1.0, 1.0, 0.0).Normalize();
            var ey = new Vector3D(0.0, 0.0, -1.0);
            var ez = new Vector3D(-1.0, 1.0, 0.0);
            var mat = Matrix44D.CreateCoordinateSystem(new Position3D(), ex, ey, ez);
            var xAxis = new Vector3D(1.0, 0.0, 0.0);

            var exNew = mat * xAxis;

            Assert.Equal(ex, exNew);
        }

        [Fact]
        public void MultiplicationOperatorTest_WhenMatrixMultpliedWithZAxis_ThenResultVectorIsEzOfMatrix()
        {
            var ex = new Vector3D(1.0, 1.0, 0.0);
            var ey = new Vector3D(0.0, 0.0, -1.0);
            var ez = new Vector3D(-1.0, 1.0, 0.0);
            var mat = Matrix44D.CreateCoordinateSystem(new Position3D(), ex, ey, ez);
            var zAxis = new Vector3D(0.0, 0.0, 1.0);

            var ezNew = mat * zAxis;

            Assert.Equal(ez, ezNew);
        }

        [Fact]
        public void MultiplicationOperatorTest_WhenMatrixMultpliedWithYAxis_ThenResultVectorIsEyOfMatrix()
        {
            var ex = new Vector3D(1.0, 1.0, 0.0);
            var ey = new Vector3D(-1.0, 1.0, 0.0);
            var ez = new Vector3D(0.0, 0.0, 1.0);
            var mat = Matrix44D.CreateCoordinateSystem(new Position3D(), ex, ey, ez);
            var yAxis = new Vector3D(0.0, 1.0, 0.0);

            var eyNew = mat * yAxis;

            Assert.Equal(ey, eyNew);
        }

        [Fact]
        public void OperatorMultiplyTest_WhenRoationWithTranslationAreMultiplidWithPosition_ThenPositionIsRotatedAndThenTranslated()
        {
            var first = Matrix44D.CreateTranslation(new Vector3D(100.0, 0.0, 0.0));
            var second = Matrix44D.CreateRotation(new Position3D(), new Vector3D(0, 0, 1), 90.0.DegToRad());
            var position = new Position3D(1.0, 0.0, 0.0);

            var newPosition = first * second * position;

            var expected = new Position3D(100.0, 1.0, 0.0);
            Assert.Equal(expected, newPosition);
        }

        [Fact]
        public void OperatorMultiplyTest_WhenTranslationWithRoationAreMultiplidWithPosition_ThenPositionIsTranslatedAndThenRotated()
        {
            var first = Matrix44D.CreateRotation(new Position3D(), new Vector3D(0, 0, 1), 90.0.DegToRad());
            var second = Matrix44D.CreateTranslation(new Vector3D(100.0, 0.0, 0.0));
            var position = new Position3D(1.0, 0.0, 0.0);

            var newPosition = first * second * position;

            var expected = new Position3D(0.0, 101.0, 0.0);
            Assert.Equal(expected, newPosition);
        }

        [Fact]
        public void IdentityTest_WhenIdentityIsCalled_ThenResultIsIdentityMatrix()
        {
            var mat = Matrix44D.Identity;

            Assert.Equal(1.0, mat.A11, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A12, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A13, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A14, ConstantsMath.Precision);

            Assert.Equal(0.0, mat.A21, ConstantsMath.Precision);
            Assert.Equal(1.0, mat.A22, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A23, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A24, ConstantsMath.Precision);

            Assert.Equal(0.0, mat.A31, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A32, ConstantsMath.Precision);
            Assert.Equal(1.0, mat.A33, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A34, ConstantsMath.Precision);

            Assert.Equal(0.0, mat.A41, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A42, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.A43, ConstantsMath.Precision);
            Assert.Equal(1.0, mat.A44, ConstantsMath.Precision);
        }

        [Fact]
        public void DeterminanteTest_WhenDeterminanteIsCalled_ThenDeterminanteIsCalculated()
        {
            Matrix44D first = new Matrix44D(1.1, 1.2, 1.3, 1.4, 2.1, 2.2, 2.3, 2.4, 3.1, 3.2, 3.3, 3.4, 4.1, 4.2, 4.3, 4.4);
            Matrix4x4 second = new Matrix4x4(1.1f, 1.2f, 1.3f, 1.4f, 2.1f, 2.2f, 2.3f, 2.4f, 3.1f, 3.2f, 3.3f, 3.4f, 4.1f, 4.2f, 4.3f, 4.4f);

            var firstDeterminante = first.Determinante;
            var secondDeterminante = (double)second.GetDeterminant();

            Assert.Equal(firstDeterminante, secondDeterminante, ConstantsMath.Precision);
        }

        [Fact]
        public void InverseTest_WhenStandardCoordinateSystemIsGiven_ThenCoordinateSystemMultiplyWithInverseIsIdentity()
        {
            var vec = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, 0.0), new Vector3D(1.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0));

            var inverse = vec.Inverse();

            var ident = vec * inverse;
            Assert.Equal(Matrix44D.Identity, ident);
        }

        [Fact]
        public void InverseTest_WhenStandardCoordinateSystemIsGiven_ThenInverseMultiplyWithCoordinateSystemIsIdentity()
        {
            var vec = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, 0.0), new Vector3D(1.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0));

            var inverse = vec.Inverse();

            var ident = inverse * vec;
            Assert.Equal(Matrix44D.Identity, ident);
        }

        [Fact]
        public void InverseTest_WhenRandomFramesAreGenerated_ThenFramesMultipliedWithThereInverseAreIdentity()
        {
            var helper = new CreateRandomMatrixHelper();
            var matrices = Enumerable.Range(0, 100).Select(x => helper.CreateFrame()).ToList();

            var inverses = matrices.Select(x => x.Inverse()).ToList();
            var results = matrices.Select(x => x * x.Inverse()).ToList();

            const double epsilon = 0.001;
            Assert.True(results.All(x => x.Equals(Matrix44D.Identity, epsilon)));
        }

        [Fact]
        public void InverseTest_WhenRandomFramesAreGenerated_ThenInverseMultipliedWithThereFramesAreIdentity()
        {
            var helper = new CreateRandomMatrixHelper();
            var matrices = Enumerable.Range(0, 1000).Select(x => helper.CreateFrame()).ToList();

            var results = matrices.Select(x => x.Inverse() * x).ToList();
            foreach (var res in matrices)
            {
                var ident = (res * res.Inverse());
                if (ident != Matrix44D.Identity)
                {
                    var a = 0;
                    a++;
                }
            }

            const double epsilon = 0.001;
            Assert.True(results.All(x => x.Equals(Matrix44D.Identity, epsilon)));
        }

        [Fact]
        public void CreateCoordinateSystemTest_WhenCoordinateSystemIsCalledWithExEyEzAndTranslation_ThenExEyEzAndTranslationAreEqualToParameters()
        {
            var ex = new Vector3D(1.0, 0.0, 0.0);
            var ey = new Vector3D(0.0, 0.0, -1.0);
            var ez = new Vector3D(-1.0, 1.0, 0.0);
            var offset = new Position3D(2, 3, 4);

            var mat = Matrix44D.CreateCoordinateSystem(offset, ex, ey, ez);

            Assert.Equal(ex, mat.Ex);
            Assert.Equal(ey, mat.Ey);
            Assert.Equal(ez, mat.Ez);
            Assert.Equal(offset, mat.Offset);
            Assert.Equal(offset.ToVector3D(), mat.Translation);
        }

        [Fact]
        public void CreateCoordinateSystemTest_WhenCoordinateSystemIsCalledExEzAndTranslation_ThenExEzAndTranslationAreEqualToParametersAndEyIsOrthogonalToExEz()
        {
            var ex = new Vector3D(1.0, 0.0, 0.0);
            var ez = new Vector3D(-1.0, 1.0, 0.0);
            var offset = new Position3D(2, 3, 4);

            var mat = Matrix44D.CreateCoordinateSystem(offset, ex, ez);

            var ey = new Vector3D(0.0, 0.0, -1.0);
            Assert.Equal(ex, mat.Ex);
            Assert.Equal(ey, mat.Ey);
            Assert.Equal(ez, mat.Ez);
            Assert.Equal(offset, mat.Offset);
            Assert.Equal(offset.ToVector3D(), mat.Translation);
        }

        [Fact]
        public void CreateCoordinateSystemTest_WhenCoordinateSystemIsCalledExEz_ThenExEyEzAreEqualToParametersAndAndTranslationIsOrigin()
        {
            var ex = new Vector3D(1.0, 0.0, 0.0);
            var ez = new Vector3D(-1.0, 1.0, 0.0);

            var mat = Matrix44D.CreateCoordinateSystem(ex, ez);

            var ey = new Vector3D(0.0, 0.0, -1.0);
            Assert.Equal(ex, mat.Ex);
            Assert.Equal(ey, mat.Ey);
            Assert.Equal(ez, mat.Ez);
            Assert.Equal(new Position3D(), mat.Offset);
            Assert.Equal(new Vector3D(), mat.Translation);
        }

        [Fact]
        public void CreatePlaneCoordinateSystemTest_WhenCoordinateSystemIsCalledWithOffsetAndNormal_ThenTranslationIsOffsetAndEzIsNormal()
        {
            var normal = new Vector3D(1.0, 1.0, 1.0).Normalize();
            var offset = new Position3D(2, 3, 4);

            var mat = Matrix44D.CreatePlaneCoordinateSystem(offset, normal);

            Assert.Equal(offset, mat.Offset);
            Assert.Equal(offset.ToVector3D(), mat.Translation);
            Assert.Equal(normal, mat.Ez);
            Assert.Equal(0.0, mat.Ex * mat.Ey, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.Ey * mat.Ez, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.Ez * mat.Ex, ConstantsMath.Precision);
        }

        [Fact]
        public void CreatePlaneCoordinateSystemTest_WhenCoordinateSystemIsCalledWithOffsetAndTwoPlaneVectors_ThenTranslationIsOffsetAndEzIsNormal()
        {
            var first = new Vector3D(1.0, 1.0, 0.0).Normalize();
            var second = new Vector3D(1.0, -1.0, 0.0).Normalize();
            var offset = new Position3D(2, 3, 4);

            var mat = Matrix44D.CreatePlaneCoordinateSystem(offset, first, second);

            var expectedNormal = (first & second).Normalize();
            Assert.Equal(offset, mat.Offset);
            Assert.Equal(offset.ToVector3D(), mat.Translation);
            Assert.Equal(expectedNormal, mat.Ez);
            Assert.Equal(0.0, mat.Ex * mat.Ey, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.Ey * mat.Ez, ConstantsMath.Precision);
            Assert.Equal(0.0, mat.Ez * mat.Ex, ConstantsMath.Precision);
        }

        [Fact]
        public void CreateTranslationTest_WhenCreateTranslationIsCalledWithOffset_ThenTranslationIsOffsetAndExEyEzAreCoordinateAxes()
        {
            var offset = new Vector3D(2.0, 3.0, 4.0);

            var mat = Matrix44D.CreateTranslation(offset);
        }

        [Fact]
        public void CreateRotationTest_WhenParameterOffsetAxisAndAngle_ThenPositionIsRotatedAndTranslated()
        {
            var axis = new Vector3D(1, 0, 0);
            var rot = Matrix44D.CreateRotation(new Position3D(0, 0, 10), axis, 90.0.DegToRad());

            var pos = rot * new Position3D(0.0, 0.0, 0.0);
            Assert.Equal(new Position3D(0.0, 10.0, 10.0), pos);
        }

        [Fact]
        public void CreateRotationTest_WhenParameterOnlyAxisAndAngle_ThenPositioIsOnlyRotated()
        {
            var axis = new Vector3D(1, 0, 0);
            var rot = Matrix44D.CreateRotation(axis, 90.0.DegToRad());

            Assert.Equal(new Position3D(), rot.Offset);
            Assert.Equal(new Vector3D(), rot.Translation);
            var pos = rot * new Position3D(0.0, 0.0, 1.0);
            Assert.Equal(new Position3D(0.0, -1.0, 0.0), pos);
        }
    }
}