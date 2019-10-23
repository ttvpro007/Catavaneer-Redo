using System.Collections.Generic;
using UnityEngine;

namespace ViTiet.UnityExtension.TextureRender
{
    [System.Serializable]
    public class CustomGradient
    {
        public enum BlendMode { Linear, Discrete}
        [SerializeField] BlendMode blendMode;
        [SerializeField] List<ColorKey> colorKeys = new List<ColorKey>();
        [SerializeField] bool randomColor;

        public CustomGradient()
        {
            AddColorKey(Color.black, 0);
            AddColorKey(Color.white, 1);
        }

        /// <summary>
        /// Returns gradient based on blend factor param
        /// </summary>
        /// <param name="blendFactor">
        /// Float value from 0 to 1
        /// </param>
        /// <returns></returns>
        public Color Evaluate(float blendFactor)
        {
            ColorKey colorKeyLeft = colorKeys[0];
            ColorKey colorKeyRight = colorKeys[colorKeys.Count - 1];

            for (int i = 0; i < colorKeys.Count - 1; i++)
            {
                if (colorKeys[i].BlendFactor <= blendFactor)
                {
                    colorKeyLeft = colorKeys[i];
                }

                if (colorKeys[i + 1].BlendFactor >= blendFactor)
                {
                    colorKeyRight = colorKeys[i + 1];
                    break;
                }
            }

            if (blendMode == BlendMode.Linear)
            {
                float currentBlendFactor = Mathf.InverseLerp(colorKeyLeft.BlendFactor, colorKeyRight.BlendFactor, blendFactor);

                // blends colors
                return Color.Lerp(colorKeyLeft.Color, colorKeyLeft.Color, currentBlendFactor);
            }

            return colorKeyRight.Color;
        }

        /// <summary>
        /// Add new color key to keys list
        /// </summary>
        public int AddColorKey(Color color, float blendFactor)
        {
            ColorKey newColorKey = new ColorKey(color, blendFactor);

            for (int i = 0; i < colorKeys.Count; i++)
            {
                if (newColorKey.BlendFactor < colorKeys[i].BlendFactor)
                {
                    colorKeys.Insert(i, newColorKey);
                    return i;
                }
            }

            colorKeys.Add(newColorKey);
            return colorKeys.Count - 1;
        }

        /// <summary>
        /// Remove color key at index
        /// </summary>
        public void RemoveColorKey(int index)
        {
            if (colorKeys.Count >= 2)
            {
                colorKeys.RemoveAt(index);
            }
        }

        /// <summary>
        /// Update color key at index with color
        /// </summary>
        public void UpdateColorKey(int index, Color color)
        {
            colorKeys[index] = new ColorKey(color, colorKeys[index].BlendFactor);
        }

        /// <summary>
        /// Returns the current length of the color keys list
        /// </summary>
        public int ColorKeysCount
        {
            get { return colorKeys.Count; }
        }

        /// <summary>
        /// Returns the color key based on index
        /// </summary>
        public ColorKey GetColorKey(int index)
        {
            return colorKeys[index];
        }

        /// <summary>
        /// Returns gradient texture based on the width of texture
        /// </summary>
        public Texture2D GetTexure(int width)
        {
            Texture2D texture = new Texture2D(width, 1);

            Color[] colors = new Color[width];
            for (int i = 0; i < width; i++)
            {
                colors[i] = Evaluate((float)i / (width - 1));
            }
            texture.SetPixels(colors);
            texture.Apply();

            return texture;
        }

        /// <summary>
        /// A struct containing color value and blend factor
        /// </summary>
        public struct ColorKey
        {
            [SerializeField] Color color;
            [SerializeField] float blendFactor;

            public ColorKey(Color color, float blendFactor)
            {
                this.color = color;
                this.blendFactor = blendFactor;
            }

            public Color Color { get { return color; } }
            public float BlendFactor { get { return blendFactor; } }
        }
    }
}