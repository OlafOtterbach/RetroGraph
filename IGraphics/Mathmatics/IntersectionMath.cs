using IGraphics.Mathmatics.Extensions;
using System;

namespace IGraphics.Mathmatics
{
    public static class IntersectionMath
    {
        public static bool AreLinesBoundedBoxesOverlapped(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            if (x2 < x1) (x1, x2) = Swap(x1, x2);
            if (y2 < y1) (y1, y2) = Swap(y1, y2);
            if (x4 < x3) (x3, x4) = Swap(x3, x4);
            if (y4 < y3) (y3, y4) = Swap(y3, y4);

            var xOverlapped = (x1 > x3 && x1 < x4) || (x2 > x3 && x2 < x4) || (x3 > x1 && x3 < x2) || (x4 > x1 && x4 < x2) || x1.EqualsTo(x3) || x2.EqualsTo(x4);
            var yOverlapped = (y1 > y3 && y1 < y4) || (y2 > y3 && y2 < y4) || (y3 > y1 && y3 < y2) || (y4 > y1 && y4 < y2) || y1.EqualsTo(y3) || y2.EqualsTo(y4);
            var areOverlapped = xOverlapped && yOverlapped;

            return areOverlapped;
        }

        private static (double, double) Swap(double first, double second) => (second, first);

        public static (bool, double, double) Check2DLineWithLine(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            // Calculating intersection between p1p2 and p3p4
            var dxi = (y4 - y3) * (x2 - x1) - (y2 - y1) * (x4 - x3);
            if (dxi == 0.0) return (false, 0.0, 0.0);
            var qxi = (x4 - x3) * (x2 * y1 - x1 * y2) - (x2 - x1) * (x4 * y3 - x3 * y4);
            var xi = qxi / dxi;

            var dyi = (y4 - y3) * (x2 - x1) - (y2 - y1) * (x4 - x3);
            if (dyi == 0.0) return (false, 0.0, 0.0);
            var qyi = (y1 - y2) * (x4 * y3 - x3 * y4) - (y3 - y4) * (x2 * y1 - x1 * y2);
            var yi = qyi / dyi;

            // Intersection pi lies on p1p2
            var lenP1P2 = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
            var lenP1PI = (x1 - xi) * (x1 - xi) + (y1 - yi) * (y1 - yi);
            var lenP2PI = (x2 - xi) * (x2 - xi) + (y2 - yi) * (y2 - yi);
            if (lenP1PI > (lenP1P2 + ConstantsMath.Epsilon) || lenP2PI > (lenP1P2 + ConstantsMath.Epsilon)) return (false, 0.0, 0.0);

            // Intersection pi lies on p3p4
            var lenP3P4 = (x3 - x4) * (x3 - x4) + (y3 - y4) * (y3 - y4);
            var lenP3PI = (x3 - xi) * (x3 - xi) + (y3 - yi) * (y3 - yi);
            var lenP4PI = (x4 - xi) * (x4 - xi) + (y4 - yi) * (y4 - yi);
            if (lenP3PI > (lenP3P4 + ConstantsMath.Epsilon) || lenP4PI > (lenP3P4 + ConstantsMath.Epsilon)) return (false, 0.0, 0.0);

            if ((lenP1PI.EqualsTo(lenP1P2) || lenP2PI.EqualsTo(lenP1P2))
                && (lenP3PI.EqualsTo(lenP3P4) || lenP4PI.EqualsTo(lenP3P4))) return (false, 0.0, 0.0);

            return (true, xi, yi);
        }

        public static Position3D CalculatePerpendicularPoint(this Position3D position, Position3D offset, Vector3D direction)
        {
            var point = position;

            var delta = position - offset;
            var len = delta.Length;
            if (!len.EqualsTo(0.0))
            {
                var normal = direction.Normalize();
                double product = delta * normal;
                double divProduct = direction * normal;
                if (divProduct.Equals(0.0))
                {
                    divProduct = 0.01;
                }
                double lamda = -product / divProduct;
                normal = normal * lamda;
                point = offset + normal;
            }
            return point;
        }

        public static (bool, Position3D) CalculatePerpendicularPoint(Position3D baseLineOffset, Vector3D baseLineDirection, Position3D neighbourOffset, Vector3D neighbourDirection)
        {
            // Lotpunkte ermittel, deren Lot gebildet aus deren Verbindungslinie
            // senkrecht auf beiden Geraden steht

            // Parameter fuer Gerade 1: g1=a+k*v initialisieren
            bool result = false;
            var perpendicularPoint = new Position3D(double.MinValue, double.MinValue, double.MinValue);

            double a1 = baseLineOffset.X;
            double a2 = baseLineOffset.Y;
            double a3 = baseLineOffset.Z;
            double v1 = baseLineDirection.X;
            double v2 = baseLineDirection.Y;
            double v3 = baseLineDirection.Z;

            double b1 = neighbourOffset.X;
            double b2 = neighbourOffset.Y;
            double b3 = neighbourOffset.Z;
            double w1 = neighbourDirection.X;
            double w2 = neighbourDirection.Y;
            double w3 = neighbourDirection.Z;

            // Gleichung 1: A1-k*K1+l*L1=0 erzeugen
            double A1 = v1 * b1 + v2 * b2 + v3 * b3 - v1 * a1 - v2 * a2 - v3 * a3;
            double K1 = v1 * v1 + v2 * v2 + v3 * v3;
            double L1 = v1 * w1 + v2 * w2 + v3 * w3; // = K2

            // Gleichung 2: A2-k*K2+l*L2=0 erzeugen
            double A2 = w1 * b1 + w2 * b2 + w3 * b3 - w1 * a1 - w2 * a2 - w3 * a3;
            double K2 = w1 * v1 + w2 * v2 + w3 * v3; // = L1
            double L2 = w1 * w1 + w2 * w2 + w3 * w3;

            // Calculate factor k
            double k = L1 * K2 - L2 * K1;
            if (k != 0.0)
            {
                k = (L1 * A2 - L2 * A1) / k;

                // Calculate plump point
                var delta = baseLineDirection * k;
                perpendicularPoint = baseLineOffset + delta;
                result = true;
            }
            return (result, perpendicularPoint);
        }

        public static (bool, Position3D) Intersect(Position3D planeOffset, Vector3D planeNormal, Position3D lineOffset, Vector3D lineDirection)
        {
            bool result;

            var nx = planeNormal.X;
            var ny = planeNormal.Y;
            var nz = planeNormal.Z;
            var x = planeOffset.X;
            var y = planeOffset.Y;
            var z = planeOffset.Z;
            var sx = lineOffset.X;
            var sy = lineOffset.Y;
            var sz = lineOffset.Z;
            var dx = lineDirection.X;
            var dy = lineDirection.Y;
            var dz = lineDirection.Z;

            // Liniengleichung in Hessenormalform der Ebene einsetzen
            var help1 = (nx * x) + (ny * y) + (nz * z)
                        - (nx * sx) - (ny * sy) - (nz * sz);
            var help2 = (nx * dx) + (ny * dy) + (nz * dz);
            Position3D intersection;
            if (help2.NotEqualsTo(0.0))
            {
                // Schnittpunkt errechnen
                var lamda = help1 / help2;
                intersection = lineOffset + (lineDirection * lamda);
                result = true;
            }
            else
            {
                // No intersection, Line parallel to plane
                intersection = new Position3D(0.0, 0.0, 0.0);
                result = false;
            }

            return (result, intersection);
        }

        public static double DistancePositionToPlane(this Position3D position, Position3D offset, Vector3D normal)
        {
            var nx = normal.X;
            var ny = normal.Y;
            var nz = normal.Z;
            var x = offset.X;
            var y = offset.Y;
            var z = offset.Z;
            var px = position.X;
            var py = position.Y;
            var pz = position.Z;
            var direction = (px - x) * nx + (py - y) * ny + (pz - z) * nz;
            var dist = Math.Abs(direction);
            return dist;
        }

        public static (bool, double) GetSquaredDistanceOfIntersectionOfRayAndTriangle(Position3D rayOffset, Vector3D rayDirection, Position3D p1, Position3D p2, Position3D p3)
        {
            var normal = ((p2 - p1) & (p3 - p1)).Normalize();
            var (hasIntersection, intersection) = IntersectionMath.Intersect(p1, normal, rayOffset, rayDirection);
            if (hasIntersection)
            {
                var intersectDirection = (intersection - rayOffset).Normalize();
                if(intersectDirection != rayDirection.Normalize()) return (false, -1.0);

                var mat = Matrix44D.CreatePlaneCoordinateSystem(p1, normal).Inverse();
                p1 = mat * p1;
                p2 = mat * p2;
                p3 = mat * p3;
                var position = mat * intersection;

                if (IsPositionInsideTriangle(position.X, position.Y, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y))
                {
                    var distVec = intersection - rayOffset;
                    var distSquare = distVec.X * distVec.X + distVec.Y * distVec.Y + distVec.Z * distVec.Z;
                    return (true, distSquare);
                }
            }

            return (false, -1.0);
        }

        public static (bool, Position3D, double) GetIntersectionAndSquaredDistanceOfRayAndTriangle(Position3D rayOffset, Vector3D rayDirection, Position3D p1, Position3D p2, Position3D p3)
        {
            var normal = ((p2 - p1) & (p3 - p1)).Normalize();
            var (hasIntersection, intersection) = IntersectionMath.Intersect(p1, normal, rayOffset, rayDirection);
            if (hasIntersection)
            {
                var mat = Matrix44D.CreatePlaneCoordinateSystem(p1, normal).Inverse();
                p1 = mat * p1;
                p2 = mat * p2;
                p3 = mat * p3;
                var position = mat * intersection;

                if (IsPositionInsideTriangle(position.X, position.Y, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y))
                {
                    var distVec = intersection - rayOffset;
                    var distSquare = distVec.X * distVec.X + distVec.Y * distVec.Y + distVec.Z * distVec.Z;
                    return (true, intersection, distSquare);
                }
            }

            return (false, new Position3D(), -1.0);
        }

        private static bool IsPositionInsideTriangle(double x, double y, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var res1 = TriangleMath.IsCounterClockwise(x1, y1, x2, y2, x, y);
            var res2 = TriangleMath.IsCounterClockwise(x2, y2, x3, y3, x, y);
            var res3 = TriangleMath.IsCounterClockwise(x3, y3, x1, y1, x, y);

            var isInside = res1 == res2 && res2 == res3;

            return isInside;
        }

        private enum TriangleSpin
        {
            no_clockwise = 0,
            clockwise = -1,
            counter_clockwise = 1,
        }
    }
}