using UnityEngine;

namespace ViTiet.ProceduralGenerator.Terrain
{
    /// <summary>
    /// Noise details
    /// </summary>
    [System.Serializable]
    public class NoiseProperties
    {
        [Range(0, 1)]
        public float strength = 0;
        [Range(0.001f, 100f)]
        public float scale;
        [Range(1, 16)]
        public int octaves;
        [Range(0f, 1f)]
        public float persistance;
        [Range(0f, 20f)]
        public float lacunarity;
        public Vector2 offset;
        public int seed;
    }
}
