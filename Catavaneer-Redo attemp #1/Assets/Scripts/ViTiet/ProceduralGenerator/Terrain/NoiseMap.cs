using System;
using ViTiet.UnityExtension.Math;
using UnityEngine;

namespace ViTiet.ProceduralGenerator.Terrain
{
    /// <summary>
    /// Collections of noise map generation methods
    /// </summary>
    public static class NoiseMap
    {
        /// <summary>
        /// Generate  2D array of Perlin noise values
        /// </summary>
        public static float[,] Generate(int width, int height, NoiseProperties noiseProperties)
        {
            float[,] noiseMapValues = new float[width, height];
            float minNoiseValue, maxNoiseValue;

            // register octaves offsets to random seeds
            System.Random prng = new System.Random(noiseProperties.seed);
            Vector2[] octaveOffsets = new Vector2[noiseProperties.octaves];
            for (int i = 0; i < noiseProperties.octaves; i++)
            {
                float offsetX = prng.Next(-10000, 10000) + noiseProperties.offset.x;
                float offsetY = prng.Next(-10000, 10000) + noiseProperties.offset.y;
                octaveOffsets[i] = new Vector2(offsetX, offsetY);
            }

            // get noise height values
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // density
                    float frequency = 1;

                    // maximum output
                    float amplitude = 1;

                    // map heigth value
                    float noiseHeigth = 0;

                    for (int o = 0; o < noiseProperties.octaves; o++)
                    {
                        float sampleX = (x - width / 2) / noiseProperties.scale * frequency + octaveOffsets[o].x /* Mathf.Sin(x * Mathf.PI / 2)*/;
                        float sampleY = (y - height / 2) / noiseProperties.scale * frequency + octaveOffsets[o].y /* Mathf.Cos(y * Mathf.PI / 2)*/;

                        // perlin noise return value between 0 and 1. value * 2 - 1 return values between -1 and 1
                        float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                        noiseHeigth += perlinValue * amplitude;

                        // won't be used if octave = 1
                        frequency *= noiseProperties.lacunarity;
                        amplitude *= noiseProperties.persistance;
                    }

                    noiseMapValues[x, y] = noiseHeigth;
                }
            }

            minNoiseValue = MathExtended.Min(noiseMapValues);
            maxNoiseValue = MathExtended.Max(noiseMapValues);

            // normalize the previous noise height values range from (-1 to 1) to (0 to 1)
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    noiseMapValues[x, y] = Mathf.InverseLerp(minNoiseValue, maxNoiseValue, noiseMapValues[x, y]);
                }
            }

            return noiseMapValues;
        }

        /// <summary>
        /// Render texture to target renderer
        /// </summary>
        public static void Render(float[,] noiseMapValues, Renderer targetRenderer)
        {
            int width = noiseMapValues.GetLength(0);
            int height = noiseMapValues.GetLength(1);

            Texture2D texture = new Texture2D(width, height);

            Color[] colors = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // lerp between black and white
                    colors[x + y * width] = Color.Lerp(Color.black, Color.white, noiseMapValues[x, y]);
                }
            }

            // set pixels colors and apply
            texture.SetPixels(colors);
            texture.Apply();

            // set texture and scale to renderer
            targetRenderer.sharedMaterial.mainTexture = texture;
            targetRenderer.transform.localScale = new Vector3(width, 1, height);
        }

        /// <summary>
        /// Render texture to target renderer
        /// </summary>
        public static void Render(float[,] noiseMapValues, Gradient gradient, Renderer targetRenderer)
        {
            int width = noiseMapValues.GetLength(0);
            int height = noiseMapValues.GetLength(1);

            Texture2D texture = new Texture2D(width, height);

            Color[] colors = new Color[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // get golor from gradient
                    colors[x + y * width] = gradient.Evaluate(noiseMapValues[x, y]);
                }
            }

            // set pixels colors and apply
            texture.SetPixels(colors);
            texture.Apply();

            // set texture and scale to renderer
            targetRenderer.sharedMaterial.mainTexture = texture;
            targetRenderer.transform.localScale = new Vector3(width, 1, height);
        }
    }
}