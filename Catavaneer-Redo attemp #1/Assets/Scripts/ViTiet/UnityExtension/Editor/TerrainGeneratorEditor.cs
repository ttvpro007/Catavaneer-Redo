using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGenerator terrainGenerator = (TerrainGenerator)target;

        if (DrawDefaultInspector())
        {
            if (terrainGenerator.autoGenerate)
            {
                terrainGenerator.GenerateTerrain();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            terrainGenerator.GenerateTerrain();
        }
    }
}
