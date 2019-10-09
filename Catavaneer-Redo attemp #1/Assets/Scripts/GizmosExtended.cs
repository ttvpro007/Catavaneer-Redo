using UnityEngine;
using ViTiet.Library.Generic;

namespace ViTiet.Library.UnityExtension.Gizmos
{
    public static class GizmosExtended
    {
        public static void DrawRectangleBox(Vector3 center, float width, float height, float depth, Color color)
        {
            UnityEngine.Gizmos.color = color;
            Vector3[] points = new Vector3[8];

            //// point 1 = left(-) upper(+) rear(-)
            //points[0] = new Vector3(center.x - width / 2,
            //                        center.y + depth / 2,
            //                        center.z - height / 2);
            //// point 2 = right(+) upper(+) rear(-)
            //points[1] = new Vector3(center.x + width / 2,
            //                        center.y + depth / 2,
            //                        center.z - height / 2);
            //// point 3 = right(+) lower(-) rear(-)
            //points[2] = new Vector3(center.x + width / 2,
            //                        center.y - depth / 2,
            //                        center.z - height / 2);
            //// point 4 = left(-) lower(-) rear(-)
            //points[3] = new Vector3(center.x - width / 2,
            //                        center.y - depth / 2,
            //                        center.z - height / 2);
            //// point 5 = left(-) upper(+) front(+)
            //points[4] = new Vector3(center.x - width / 2,
            //                        center.y + depth / 2,
            //                        center.z + height / 2);
            //// point 6 = right(+) upper(+) front(+)
            //points[5] = new Vector3(center.x + width / 2,
            //                        center.y + depth / 2,
            //                        center.z + height / 2);
            //// point 7 = right(+) lower(-) front(+)
            //points[6] = new Vector3(center.x + width / 2,
            //                        center.y - depth / 2,
            //                        center.z + height / 2);
            //// point 8 = left(-) lower(-) front(+)
            //points[7] = new Vector3(center.x - width / 2,
            //                        center.y - depth / 2,
            //                        center.z + height / 2);

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
                     points[i].x = center.x - width / 2;
                else points[i].x = center.x + width / 2;

                if (i == 0 || i == 1)
                     points[i].y = center.y + depth / 2;
                else points[i].y = center.y - depth / 2;

                points[i].z = center.z - height / 2;

                points[i + 4].x = points[i].x;
                points[i + 4].y = points[i].y;
                points[i + 4].z = points[i].z + height;
            }

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
    }
}