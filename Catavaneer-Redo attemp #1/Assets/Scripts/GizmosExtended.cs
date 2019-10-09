using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosExtended
{
    public static void DrawRectangleBox(Transform center, float width, float height, float depth, Color color)
    {
        Gizmos.color = color;
        Vector3[] points = new Vector3[8];
        // point 1 = left(-) upper(+) rear(-)
        points[0] = new Vector3(center.position.x - width / 2,
                                center.position.y + depth / 2,
                                center.position.z - height / 2);
        // point 2 = right(+) upper(+) rear(-)
        points[1] = new Vector3(center.position.x + width / 2,
                                center.position.y + depth / 2,
                                center.position.z - height / 2);
        // point 3 = right(+) lower(-) rear(-)
        points[2] = new Vector3(center.position.x + width / 2,
                                center.position.y - depth / 2,
                                center.position.z - height / 2);
        // point 4 = left(-) lower(-) rear(-)
        points[3] = new Vector3(center.position.x - width / 2,
                                center.position.y - depth / 2,
                                center.position.z - height / 2);
        // point 5 = left(-) upper(+) front(+)
        points[4] = new Vector3(center.position.x - width / 2,
                                center.position.y + depth / 2,
                                center.position.z + height / 2);
        // point 6 = right(+) upper(+) front(+)
        points[5] = new Vector3(center.position.x + width / 2,
                                center.position.y + depth / 2,
                                center.position.z + height / 2);
        // point 7 = right(+) lower(-) front(+)
        points[6] = new Vector3(center.position.x + width / 2,
                                center.position.y - depth / 2,
                                center.position.z + height / 2);
        // point 8 = left(-) lower(-) front(+)
        points[7] = new Vector3(center.position.x - width / 2,
                                center.position.y - depth / 2,
                                center.position.z + height / 2);

        // draw rear rect
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 0, 4)]);
        }

        // draw front rect
        for (int i = 4; i < 8; i++)
        {
            Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 4, 8)]);
        }

        // connect rear & front rect
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 4]);
        }
    }
}
