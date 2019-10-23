using UnityEngine;
using ViTiet.ProceduralGenerator.Terrain;

public class TerrainGenerator : MonoBehaviour
{
    // 
    const int mapSize = 129;

    [Header("Terrain Target")]
    [SerializeField] ProceduralTerrain terrain = null;

    [Header("Terrain Settings")]
    [SerializeField] NoiseProperties noiseProperties;
    [Range(0, 4)]
    [SerializeField] int levelOfDetail;
    [Range(0, 5000)]
    [SerializeField] int heightMultiplier;
    [SerializeField] AnimationCurve heightCurve;
    [SerializeField] Gradient terrainColors = new Gradient();

    [Header("Generation Settings")]
    [SerializeField] public bool autoGenerate;

    public void GenerateTerrain()
    {
        // get noise map height values
        float[,] heightMap = NoiseMap.Generate(mapSize, mapSize, noiseProperties);

        // render texture to target renderer
        NoiseMap.Render(heightMap, terrainColors, terrain.GetComponent<Renderer>());

        // generate terrain mesh
        TerrainMesh terrainMesh = new TerrainMesh(mapSize, mapSize, levelOfDetail);
        terrainMesh.GenerateMesh(terrain, heightMap, heightMultiplier * noiseProperties.strength, heightCurve);
    }
}