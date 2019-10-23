using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViTiet.ProceduralGenerator.Terrain
{
    /// <summary>
    /// Collections of mesh generation methods
    /// </summary>
    public class TerrainMesh
    {
        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uvs;

        int triangleIndex;
        int vertexIndex;
        int meshSimplificationFactor;
        int vertPerLine;

        /// <summary>
        /// Instantiates mesh data
        /// </summary>
        public TerrainMesh(int meshWidth, int meshHeight, int levelOfDetail)
        {
            meshSimplificationFactor = 1;
            for (int i = 0; i < 4 - levelOfDetail; i++)
            {
                meshSimplificationFactor *= 2;
            }
            vertPerLine = (meshWidth - 1) / meshSimplificationFactor + 1;

            vertices = new Vector3[vertPerLine * vertPerLine];
            triangles = new int[(vertPerLine - 1) * (vertPerLine - 1) * 6];
            uvs = new Vector2[vertPerLine * vertPerLine];

            triangleIndex = 0;
            vertexIndex = 0;
        }

        /// <summary>
        /// Generates terrain mesh
        /// </summary>
        public void GenerateMesh(ProceduralTerrain terrain, float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
        {
            Mesh mesh = terrain.GetComponent<MeshFilter>().sharedMesh;
            Vector3 center = terrain.GetComponent<Transform>().position;

            int width = heightMap.GetLength(0);
            int height = heightMap.GetLength(1);
            
            float bottomLeftX = center.x - width / 2;
            float bottomLeftZ = center.z - height / 2;

            for (int y = 0; y < height; y += meshSimplificationFactor)
            {
                for (int x = 0; x < width; x += meshSimplificationFactor)
                {
                    // generate vertices
                    vertices[vertexIndex].x = bottomLeftX + x;
                    vertices[vertexIndex].y = center.y + heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier;
                    vertices[vertexIndex].z = bottomLeftZ + y;

                    // generate uvs
                    uvs[vertexIndex].x = (float)x / width;
                    uvs[vertexIndex].y = (float)y / height;

                    // generate triangles
                    if (x < width - 1 && y < height - 1)
                    {
                        // first triangle (i, i + w, i + width + 1)
                        AddTriangle(vertexIndex, vertexIndex + vertPerLine, vertexIndex + vertPerLine + 1);

                        //second triangle (i, i + w + 1, i + 1)
                        AddTriangle(vertexIndex, vertexIndex + vertPerLine + 1, vertexIndex + 1);
                    }

                    vertexIndex++;
                }
            }
            
            // update mesh
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
        }

        /// <summary>
        /// Generates mesh triangles
        /// </summary>
        private void AddTriangle(int a, int b, int c)
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;

            triangleIndex += 3;
        }
    }
}