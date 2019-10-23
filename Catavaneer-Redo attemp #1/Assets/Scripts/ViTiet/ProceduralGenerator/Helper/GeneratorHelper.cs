using UnityEngine;
using ViTiet.UnityExtension.Math;
using ViTiet.ProceduralGenerator.Terrain;

namespace ViTiet.ProceduralGenerator.Helper
{
    /// <summary>
    /// Collection of generator helper methods
    /// </summary>
    public static class GeneratorHelper
    {
        /// <summary>
        /// Generate grid points for a box area
        /// </summary>
        public static Vector3[] GenerateGridPoints3D(Transform center, float width, float height, float depth, int row, int colum, int layer)
        {
            Vector3[] points = new Vector3[row * colum * layer];

            int segmentsX = colum - 1;
            int segmentsY = layer - 1;
            int segmentsZ = row - 1;

            if (colum == 1) segmentsX = 1;
            if (layer == 1) segmentsY = 1;
            if (row == 1) segmentsZ = 1;

            for (int c = 0, h = 0; h < row; h++)
            {
                for (int w = 0; w < colum; w++)
                {
                    for (int d = 0; d < layer; d++, c++)
                    {
                        points[c].x = center.position.x - width / 2 + (width / segmentsX * w);
                        points[c].y = center.position.y - depth / 2 + (depth / segmentsY * d);
                        points[c].z = center.position.z - height / 2 + (height / segmentsZ * h);
                    }
                }
            }

            points = VectorExtended.GetRotatePointsYAxis(center, points);

            return points;
        }
    }
}
