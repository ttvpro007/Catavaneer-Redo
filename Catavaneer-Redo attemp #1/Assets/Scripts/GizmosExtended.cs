using UnityEngine;
using ViTiet.Library.Generic;

namespace ViTiet.Library.UnityExtension.Gizmos
{
    /// <summary>
    /// Extends Unity Gizmos
    /// </summary>
    public static class GizmosExtended
    {
        /// <summary>
        /// Draws a 2D rectangle
        /// </summary>
        public static void DrawWireRectangle2D(Transform center, float width, float heigth, Color color)
        {
            Vector3[] points = new Vector3[4];

            points[0] = new Vector3(center.position.x - width / 2, center.position.y, center.position.z + heigth / 2);
            points[1] = new Vector3(center.position.x + width / 2, center.position.y, center.position.z + heigth / 2);
            points[2] = new Vector3(center.position.x + width / 2, center.position.y, center.position.z - heigth / 2);
            points[3] = new Vector3(center.position.x - width / 2, center.position.y, center.position.z - heigth / 2);

            //points = GetRotatePointsXAxis(center, points);
            points = GetRotatePointsYAxis(center, points);
            //points = GetRotatePointsZAxis(center, points);

            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Gizmos.color = color;
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 0, 4)]);
            }
        }

        /// <summary>
        /// Draws a 3D rectangle box
        /// </summary>
        public static void DrawWireRectangle3D(Transform center, float width, float heigth, float depth, Color color)
        {
            UnityEngine.Gizmos.color = color;
            Vector3[] points = new Vector3[8];

            // Loop from 1 to 4
            // Left plane  [1,4,8,5]
            // Right plane [2,3,7,6]
            // x[1,4] = x[5,8] = center x - width / 2
            // x[2,3] = x[6,7] = center x + width / 2
            // Top plane    [1,5,6,2]
            // Bottom plane [3,4,8,7]
            // y[1,2] = y[5,6] = center y + depth / 2
            // y[3,4] = y[7,8] = center y - depth / 2
            // Rear plane  [1,2,3,4]
            // Front plane [5,6,7,8]
            // z[1,2,3,4] + heigth = z[5,6,7,8]
            for (int i = 0; i < 4; i++)
            {
                if (i == 0 || i == 3)
                     points[i].x = center.position.x - width / 2;
                else points[i].x = center.position.x + width / 2;

                if (i == 0 || i == 1)
                     points[i].y = center.position.y + depth / 2;
                else points[i].y = center.position.y - depth / 2;

                points[i].z = center.position.z - heigth / 2;

                points[i + 4].x = points[i].x;
                points[i + 4].y = points[i].y;
                points[i + 4].z = points[i].z + heigth;
            }

            points = GetRotatePointsYAxis(center, points);

            // draw rear rect
            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 0, 4)]);
            }

            // draw front rect
            for (int i = 4; i < 8; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 4, 8)]);
            }

            // connect rear & front rect
            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[i + 4]);
            }
        }
        
        /// <summary>
        /// Draws a 2D ellipse
        /// </summary>
        public static void DrawWireEllipse2D(Transform center, float diameterA, float diameterB, int segments, Plane plane, Color color)
        {
            float segmentAngle = 2 * Mathf.PI / segments;
            float pointA = 0;
            float pointB = 0;
            Vector3[] points = new Vector3[segments];
            Vector3 currentPoint = Vector3.zero;
            Vector3 nextPoint = Vector3.zero;
            
            for (int i = 0; i < points.Length; i++)
            {
                pointA = diameterA / 2 * Mathf.Cos(segmentAngle * i);
                pointB = diameterB / 2 * Mathf.Sin(segmentAngle * i);
                //currentPoint = GetPoint(center, pointA, pointB, plane);
                points[i] = GetPoint(center.position, pointA, pointB, plane);

                //pointA = diameterA / 2 * Mathf.Cos(segmentAngle * LoopLogic.GetNextLoopIndex(i, 0, segments));
                //pointB = diameterB / 2 * Mathf.Sin(segmentAngle * LoopLogic.GetNextLoopIndex(i, 0, segments));
                //nextPoint = GetPoint(center, pointA, pointB, plane);

                //UnityEngine.Gizmos.color = color;
                //UnityEngine.Gizmos.DrawLine(currentPoint, nextPoint);
            }

            points = GetRotatePointsYAxis(center, points);

            for (int i = 0; i < points.Length; i++)
            {
                UnityEngine.Gizmos.color = color;
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 0, points.Length)]);
            }
        }

        /// <summary>
        /// Draws a 3D ellipse
        /// </summary>
        public static void DrawWireEllipse3D(Transform center, Vector3 diameter, int segments, Color color)
        {
            for (int i = 0; i < 3; i++)
            {
                // Draw XY
                DrawWireEllipse2D(center, diameter.x, diameter.y, segments, Plane.XY, color);
                // Draw XZ
                DrawWireEllipse2D(center, diameter.x, diameter.z, segments, Plane.XZ, color);
                // Draw YZ
                DrawWireEllipse2D(center, diameter.y, diameter.z, segments, Plane.YZ, color);
            }
        }

        /// <summary>
        /// Draws a symetrical shape based on the number of segments
        /// </summary>
        public static void DrawWireSymetricalShape2D(Transform center, float diameter, int segments, Plane plane, Color color)
        {
            if (segments <= 3) return;

            DrawWireEllipse2D(center, diameter, diameter, segments, plane, color);
        }

        /// <summary>
        /// Returns a point on the selected plane related to the center
        /// </summary>
        private static Vector3 GetPoint(Vector3 center, float pointA, float pointB, Plane plane)
        {
            switch (plane)
            {
                case Plane.XY:
                    return new Vector3(center.x + pointA, center.y + pointB, center.z);
                case Plane.XZ:
                    return new Vector3(center.x + pointA, center.y, center.z + pointB);
                case Plane.YZ:
                    return new Vector3(center.x, center.y + pointA, center.z + pointB);
            }

            return new Vector3(center.x, center.y, center.z);
        }

        /// <summary>
        /// Returns directional vector from 2 points
        /// </summary>
        private static Vector3 GetDirectionVector(Vector3 originPoint, Vector3 destinationPoint)
        {
            Vector3 directionVector = new Vector3(destinationPoint.x - originPoint.x, destinationPoint.y - originPoint.y, destinationPoint.z - originPoint.z);
            return directionVector;
        }

        /// <summary>
        /// Returns destination point
        /// </summary>
        private static Vector3 GetDestinationPoint(Vector3 directionVector, Vector3 originPoint)
        {
            Vector3 destinationPoint = new Vector3(directionVector.x + originPoint.x, directionVector.y + originPoint.y, directionVector.z + originPoint.z);
            return destinationPoint;
        }

        /// <summary>
        /// Returns origin point
        /// </summary>
        private static Vector3 GetOriginPoint(Vector3 directionVector, Vector3 destinationPoint)
        {
            Vector3 originPoint = new Vector3(destinationPoint.x - directionVector.x, destinationPoint.y - directionVector.y, destinationPoint.z - directionVector.z);
            return originPoint;
        }

        /// <summary>
        /// Returns new points that rotates around x axis of center
        /// </summary>
        public static Vector3[] GetRotatePointsXAxis(Transform center, Vector3[] points)
        {
            Vector3[] rotatePoints = points;
            Vector3 directionVector = new Vector3();

            // Rotate x axis
            for (int i = 0; i < rotatePoints.Length; i++)
            {
                directionVector = GetDirectionVector(center.position, rotatePoints[i]);
                rotatePoints[i].x = directionVector.x + center.position.x;
                rotatePoints[i].y = directionVector.y * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.x) - directionVector.z * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.x) + center.position.y;
                rotatePoints[i].z = directionVector.y * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.x) + directionVector.z * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.x) + center.position.z;
            }

            return rotatePoints;
        }

        /// <summary>
        /// Returns new points that rotates around y axis of center
        /// </summary>
        public static Vector3[] GetRotatePointsYAxis(Transform center, Vector3[] points)
        {
            Vector3[] rotatePoints = points;
            Vector3 directionVector = new Vector3();

            // Rotate y axis
            for (int i = 0; i < rotatePoints.Length; i++)
            {
                directionVector = GetDirectionVector(center.position, rotatePoints[i]);
                rotatePoints[i].x = directionVector.x * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.y) + directionVector.z * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.y) + center.position.x;
                rotatePoints[i].y = directionVector.y + center.position.y;
                rotatePoints[i].z = -directionVector.x * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.y) + directionVector.z * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.y) + center.position.z;
                //rotatePoints[i].z = -(directionVector.x * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.y) + directionVector.z * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.y)) + center.position.z;
            }

            return rotatePoints;
        }

        /// <summary>
        /// Returns new points that rotates around z axis of center
        /// </summary>
        public static Vector3[] GetRotatePointsZAxis(Transform center, Vector3[] points)
        {
            Vector3[] rotatePoints = points;
            Vector3 directionVector = new Vector3();

            // Rotate z axis
            for (int i = 0; i < rotatePoints.Length; i++)
            {
                directionVector = GetDirectionVector(center.position, rotatePoints[i]);
                rotatePoints[i].x = directionVector.x * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.z) - directionVector.y * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.z) + center.position.x;
                rotatePoints[i].y = directionVector.x * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.z) + directionVector.y * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.z) + center.position.y;
                rotatePoints[i].z = directionVector.z + center.position.z;
            }

            return rotatePoints;
        }

        /// <summary>
        /// Generate grid points for a rect
        /// </summary>
        /// <param name="size">
        /// x for width, y for depth, z for heigth
        /// </param>
        /// <param name="configure">
        /// x for row, y for layer, z for colum
        /// </param>
        public static void GenerateGridPoints(Transform center, Vector3 size, Vector3 configure)
        {
            Vector3[] points = new Vector3[(int)(configure.x * configure.y * configure.z)];

            int segmentsX = (int)configure.x - 1;
            int segmentsY = (int)configure.y - 1;
            int segmentsZ = (int)configure.z - 1;

            if (configure.x == 1) segmentsX = 1;
            if (configure.y == 1) segmentsY = 1;
            if (configure.z == 1) segmentsZ = 1;

            for (int c = 0, h = 0; h < configure.x; h++)
            {
                for (int w = 0; w < configure.z; w++)
                {
                    for (int d = 0; d < configure.y; d++, c++)
                    {
                        points[c].x = center.position.x - size.x / 2 + (size.x / segmentsX * w);
                        points[c].y = center.position.y - size.y / 2 + (size.y / segmentsY * d);
                        points[c].z = center.position.z - size.z / 2 + (size.z / segmentsZ * h);
                    }
                }
            }

            points = GetRotatePointsYAxis(center, points);

            foreach (Vector3 point in points)
            {
                UnityEngine.Gizmos.DrawSphere(point, .2f);
            }
        }
    }

    /// <summary>
    /// Plane dimention
    /// </summary>
    public enum Plane
    {
        XY, XZ, YZ
    }
}