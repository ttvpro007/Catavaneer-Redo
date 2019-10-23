using System;
using UnityEngine;

namespace ViTiet.ProceduralGenerator.Helper
{
    [Serializable]
    public class BorderInfo
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
        public float zMin;
        public float zMax;

        public BorderInfo()
        {
        }

        public BorderInfo(Vector3 center, float width, float height, float depth)
        {
            xMin = center.x - width / 2;
            xMax = center.x + width / 2;
            yMin = center.y - depth / 2;
            yMax = center.y + depth / 2;
            zMin = center.z - height / 2;
            zMax = center.z + height / 2;
        }
    }
}