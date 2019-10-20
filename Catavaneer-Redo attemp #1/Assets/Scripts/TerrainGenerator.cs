using UnityEngine;
using ViTiet.Library.ProceduralGenerator.Terrain;

public class TerrainGenerator : MonoBehaviour
{
    [Range(1, 500)]
    [SerializeField] int terrainWidth;
    [Range(1, 500)]
    [SerializeField] int terrainHeight;
    [SerializeField] NoiseProperties noiseProperties;
    [SerializeField] Vector2 offset;
    [SerializeField] int seed;
    [SerializeField] Renderer renderer = null;
    [SerializeField] public bool autoGenerate;
    float[,] noiseMap;

    public TerrainGenerator()
    {
        if (terrainWidth == 0) terrainWidth = 1;
        if (terrainHeight == 0) terrainHeight = 1;
    }

    public void GenerateTerrain()
    {
        // get noise map height values
        noiseMap = NoiseMap.Generate(terrainWidth, terrainHeight, noiseProperties, offset, seed);

        // render texture to target renderer
        NoiseMap.Render(noiseMap, renderer);
    }
}
