using RetroGraph.Application.Mathmatics.Extensions;
using System;
using System.Numerics;

namespace RetroGraph.Application.Mathmatics
{
    public struct Matrix44D : IEquatable<Matrix44D>
    {
        private Matrix4x4 _matrix;

        public Matrix44D
        (
           double a11, double a12, double a13, double a14,
           double a21, double a22, double a23, double a24,
           double a31, double a32, double a33, double a34,
           double a41, double a42, double a43, double a44
        )
        : this(new Matrix4x4((float)(a11), (float)(a21), (float)(a31), (float)(a41),
                             (float)(a12), (float)(a22), (float)(a32), (float)(a42),
                             (float)(a13), (float)(a23), (float)(a33), (float)(a43),
                             (float)(a14), (float)(a24), (float)(a34), (float)(a44)))
        { }

        internal Matrix44D(Matrix4x4 matrix) : this()
        {
            _matrix = matrix;
        }

        internal Matrix4x4 Matrix => _matrix;

        public double A11 => _matrix.M11;
        public double A12 => _matrix.M21;
        public double A13 => _matrix.M31;
        public double A14 => _matrix.M41;
        public double A21 => _matrix.M12;
        public double A22 => _matrix.M22;
        public double A23 => _matrix.M32;
        public double A24 => _matrix.M42;
        public double A31 => _matrix.M13;
        public double A32 => _matrix.M23;
        public double A33 => _matrix.M33;
        public double A34 => _matrix.M43;
        public double A41 => _matrix.M14;
        public double A42 => _matrix.M24;
        public double A43 => _matrix.M34;
        public double A44 => _matrix.M44;

        public override int GetHashCode()
        {
            return _matrix.GetHashCode();
        }

        public Matrix44D Affine
        {
            get
            {
                return new Matrix44D(new Matrix4x4(_matrix.M11, _matrix.M12, _matrix.M13, 0f,
                                                   _matrix.M21, _matrix.M22, _matrix.M23, 0f,
                                                   _matrix.M31, _matrix.M32, _matrix.M33, 0f,
                                                   0f, 0f, 0f, 1f));
            }
        }

        public Position3D Offset
        {
            get
            {
                return new Position3D(_matrix.Translation);
            }
        }

        public Vector3D Translation
        {
            get
            {
                return new Vector3D(_matrix.Translation);
            }
        }

        public Vector3D Ex
        {
            get
            {
                return new Vector3D(_matrix.M11, _matrix.M12, _matrix.M13);
            }
        }

        public Vector3D Ey
        {
            get
            {
                return new Vector3D(_matrix.M21, _matrix.M22, _matrix.M23);
            }
        }

        public Vector3D Ez
        {
            get
            {
                return new Vector3D(_matrix.M31, _matrix.M32, _matrix.M33);
            }
        }

        public override bool Equals(Object obj)
        {
            return (obj is Matrix44D) && (this.Equals((Matrix44D)obj));
        }

        public bool Equals(Matrix44D other)
        {
            bool result = (Math.Abs(Matrix.M11 - other.Matrix.M11) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M12 - other.Matrix.M12) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M13 - other.Matrix.M13) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M14 - other.Matrix.M14) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M21 - other.Matrix.M21) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M22 - other.Matrix.M22) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M23 - other.Matrix.M23) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M24 - other.Matrix.M24) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M31 - other.Matrix.M31) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M32 - other.Matrix.M32) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M33 - other.Matrix.M33) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M34 - other.Matrix.M34) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M41 - other.Matrix.M41) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M42 - other.Matrix.M42) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M43 - other.Matrix.M43) < ConstantsMath.Epsilon)
                           && (Math.Abs(Matrix.M44 - other.Matrix.M44) < ConstantsMath.Epsilon);
            return result;
        }

        public bool Equals(Matrix44D other, double epsilon)
        {
            bool result = (Math.Abs(Matrix.M11 - other.Matrix.M11) < epsilon)
                           && (Math.Abs(Matrix.M12 - other.Matrix.M12) < epsilon)
                           && (Math.Abs(Matrix.M13 - other.Matrix.M13) < epsilon)
                           && (Math.Abs(Matrix.M14 - other.Matrix.M14) < epsilon)
                           && (Math.Abs(Matrix.M21 - other.Matrix.M21) < epsilon)
                           && (Math.Abs(Matrix.M22 - other.Matrix.M22) < epsilon)
                           && (Math.Abs(Matrix.M23 - other.Matrix.M23) < epsilon)
                           && (Math.Abs(Matrix.M24 - other.Matrix.M24) < epsilon)
                           && (Math.Abs(Matrix.M31 - other.Matrix.M31) < epsilon)
                           && (Math.Abs(Matrix.M32 - other.Matrix.M32) < epsilon)
                           && (Math.Abs(Matrix.M33 - other.Matrix.M33) < epsilon)
                           && (Math.Abs(Matrix.M34 - other.Matrix.M34) < epsilon)
                           && (Math.Abs(Matrix.M41 - other.Matrix.M41) < epsilon)
                           && (Math.Abs(Matrix.M42 - other.Matrix.M42) < epsilon)
                           && (Math.Abs(Matrix.M43 - other.Matrix.M43) < epsilon)
                           && (Math.Abs(Matrix.M44 - other.Matrix.M44) < epsilon);
            return result;
        }

        public static bool operator ==(Matrix44D first, Matrix44D second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Matrix44D first, Matrix44D second)
        {
            return !first.Equals(second);
        }

        public static Matrix44D operator *(Matrix44D first, Matrix44D second)
        {
            return new Matrix44D(second.Matrix * first.Matrix);
        }

        public static Vector3D operator *(Matrix44D matrix, Vector3D vector)
        {
            return new Vector3D(Vector3.TransformNormal(vector.Vector, matrix.Matrix));
        }

        public static Position3D operator *(Matrix44D matrix, Position3D position)
        {
            return new Position3D(Vector3.Transform(position.Vector, matrix.Matrix));
        }

        private static Matrix44D _identity = new Matrix44D(Matrix4x4.Identity);

        public static Matrix44D Identity => _identity;

        public double Determinante
        {
            get
            {
                return (double)_matrix.GetDeterminant();
            }
        }

        public Matrix44D Inverse()
        {
            Matrix4x4 inverse;
            Matrix4x4.Invert(_matrix, out inverse);
            return new Matrix44D(inverse);
        }

        public static Matrix44D CreateCoordinateSystem(Position3D offset, Vector3D ex, Vector3D ey, Vector3D ez)
        {
            return new Matrix44D(new Matrix4x4((float)ex.X, (float)ex.Y, (float)ex.Z, 0f,
                                                (float)ey.X, (float)ey.Y, (float)ey.Z, 0f,
                                                (float)ez.X, (float)ez.Y, (float)ez.Z, 0f,
                                                (float)offset.X, (float)offset.Y, (float)offset.Z, 1f));
        }

        public static Matrix44D CreateCoordinateSystem(Position3D offset, Vector3D ex, Vector3D ez)
        {
            var ey = (ez & ex).Normalize();
            return CreateCoordinateSystem(offset, ex, ey, ez);
        }

        public static Matrix44D CreateCoordinateSystem(Vector3D ex, Vector3D ez)
        {
            return CreateCoordinateSystem(new Position3D(), ex, ez);
        }

        public static Matrix44D CreatePlaneCoordinateSystem(Position3D offset, Vector3D normal)
        {
            normal = normal.Normalize();
            Vector3D ex = new Vector3D(1.0f, 0.0f, 0.0f);
            Vector3D ey = normal & ex;
            double len = ey.Length;
            if (len > 0.0f)
            {
                ey = ey.Normalize();
                ex = ey & normal;
            }
            else
            {
                ex = new Vector3D(0.0f, 1.0f, 0.0f);
                ey = normal & ex;
                ey = ey.Normalize();
                ex = ey & normal;
            }
            ex = ex.Normalize();
            return CreateCoordinateSystem(offset, ex, normal);
        }

        public static Matrix44D CreatePlaneCoordinateSystem(Position3D offset, Vector3D xAxis, Vector3D secondAxis)
        {
            bool isLinear = xAxis.IsLinearTo(secondAxis);
            if (!isLinear)
            {
                var ez = (xAxis & secondAxis).Normalize();
                return CreateCoordinateSystem(offset, xAxis, ez);
            }
            else
            {
                return CreatePlaneCoordinateSystem(offset, secondAxis);
            }
        }

        public static Matrix44D CreateRotation(Position3D offset, Vector3D axis, double angle)
        {
            // create rotation coordinate system
            Vector3D ez = new Vector3D(axis).Normalize();
            Matrix44D M1 = CreatePlaneCoordinateSystem(offset, ez);

            // Create to and back transfomation
            Matrix44D M3 = M1.Inverse();

            // create xy-rotation
            var cos = Math.Cos(angle);
            var sin = Math.Sin(angle);
            Matrix44D M2 = new Matrix44D(cos, -sin, 0.0f, 0.0f,
                                          sin, cos, 0.0f, 0.0f,
                                         0.0f, 0.0f, 1.0f, 0.0f,
                                         0.0f, 0.0f, 0.0f, 1.0f);

            // Calculate matrix
            Matrix44D M = M2 * M3;
            M = M1 * M;
            return M;
        }

        public static Matrix44D CreateRotation(Vector3D axis, double angle)
        {
            return CreateRotation(new Position3D(), axis, angle);
        }

        public static Matrix44D CreateTranslation(Vector3D translation)
        {
            return new Matrix44D(Matrix4x4.CreateTranslation(translation.Vector));
        }
    }
}