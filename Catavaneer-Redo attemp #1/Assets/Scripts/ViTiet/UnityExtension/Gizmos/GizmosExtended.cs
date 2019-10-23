using UnityEngine;
using ViTiet.Generic;
using ViTiet.UnityExtension.Math;
using Plane = ViTiet.UnityExtension.Math.Plane;

namespace ViTiet.UnityExtension.Gizmos
{
    /// <summary>
    /// Extends Unity Gizmos
    /// </summary>
    public static class GizmosExtended
    {
        /// <summary>
        /// Draws a 2D rectangle
        /// </summary>
        public static void DrawWireRectangle2D(Transform center, float width, float height, Color color)
        {
            Vector3[] points = new Vector3[4];

            points[0] = new Vector3(center.position.x - width / 2, center.position.y, center.position.z + height / 2);
            points[1] = new Vector3(center.position.x + width / 2, center.position.y, center.position.z + height / 2);
            points[2] = new Vector3(center.position.x + width / 2, center.position.y, center.position.z - height / 2);
            points[3] = new Vector3(center.position.x - width / 2, center.position.y, center.position.z - height / 2);

            //points = GetRotatePointsXAxis(center, points);
            points = VectorExtended.GetRotatePointsYAxis(center, points);
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
        public static void DrawWireRectangle3D(Transform center, float width, float height, float depth, Color color)
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

                points[i].z = center.position.z - height / 2;

                points[i + 4].x = points[i].x;
                points[i + 4].y = points[i].y;
                points[i + 4].z = points[i].z + height;
            }

            points = VectorExtended.GetRotatePointsYAxis(center, points);

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
                points[i] = VectorExtended.GetPoint(center.position, pointA, pointB, plane);

                //pointA = diameterA / 2 * Mathf.Cos(segmentAngle * LoopLogic.GetNextLoopIndex(i, 0, segments));
                //pointB = diameterB / 2 * Mathf.Sin(segmentAngle * LoopLogic.GetNextLoopIndex(i, 0, segments));
                //nextPoint = GetPoint(center, pointA, pointB, plane);

                //UnityEngine.Gizmos.color = color;
                //UnityEngine.Gizmos.DrawLine(currentPoint, nextPoint);
            }

            points = VectorExtended.GetRotatePointsYAxis(center, points);

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
    }
}