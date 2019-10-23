using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ViTiet.UnityExtension.TextureRender;

namespace ViTiet.UnityExtension.Editor
{
    public class CustomGradientEditor : EditorWindow
    {
        // generic measurement
        const int borderOffset = 10;
        const int gradientRectHeight = 25;

        // color key measurement
        const int colorKeyWidth = 10;
        const int colorKeyHeight = 20;

        Rect gradientPreviousTexture;
        Rect[] colorKeyRects;
        bool mouseClickedOnColorKey;
        int selectedColorKeyIndex;
        bool needRepaint;

        CustomGradient gradient;

        private void OnEnable()
        {
            // cusutomize editor window layout
            titleContent.text = "Custom Gradient Editor";
            position.Set(position.x, position.y, 400, 150);
            minSize = new Vector2(200, 150);
            minSize = new Vector2(1000, 150);
        }

        private void OnDisable()
        {
            // mark scene as modified
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }

        private void OnGUI()
        {
            DrawHandler();

            InputHandler();

            if (needRepaint)
            {
                Repaint();
                needRepaint = false;
            }
        }

        /// <summary>
        /// Set gradient reference
        /// </summary>
        public void SetGradient(CustomGradient gradient)
        {
            this.gradient = gradient;
        }

        /// <summary>
        /// Handle editor window inputs
        /// </summary>
        private void InputHandler()
        {
            Event guiEvent = Event.current;

            // check if mouse click
            if (guiEvent.type == EventType.MouseDown)
            {
                // check if left mouse
                if (guiEvent.button == 0)
                {
                    for (int i = 0; i < colorKeyRects.Length; i++)
                    {
                        if (colorKeyRects[i].Contains(guiEvent.mousePosition))
                        {
                            mouseClickedOnColorKey = true;
                            selectedColorKeyIndex = i;
                            needRepaint = true;
                            break;
                        }
                    }

                    if (!mouseClickedOnColorKey)
                    {
                        // get random color
                        Color randCor = new Color(Random.value, Random.value, Random.value);

                        // blend factor set at mouse position x
                        float blendFactor = Mathf.InverseLerp(gradientPreviousTexture.x, gradientPreviousTexture.xMax, guiEvent.mousePosition.x);
                        gradient.AddColorKey(randCor, blendFactor);

                        // force repaint
                        needRepaint = true;
                    }
                }
            }

            if (guiEvent.type == EventType.MouseUp)
            {
                if (guiEvent.button == 0)
                {
                    mouseClickedOnColorKey = false;
                }
            }
        }

        /// <summary>
        /// Handle editor window draw
        /// </summary>
        private void DrawHandler()
        {
            // create rect container for gradient
            gradientPreviousTexture = new Rect(borderOffset, borderOffset, position.width - borderOffset * 2, gradientRectHeight);
            GUI.DrawTexture(gradientPreviousTexture, gradient.GetTexure((int)gradientPreviousTexture.width));

            // manage color key rects
            colorKeyRects = new Rect[gradient.ColorKeysCount];
            for (int i = 0; i < gradient.ColorKeysCount; i++)
            {
                CustomGradient.ColorKey colorKey = gradient.GetColorKey(i);
                Rect colorKeyRect = new Rect(gradientPreviousTexture.x + gradientPreviousTexture.xMax * colorKey.BlendFactor - (float)colorKeyWidth / 2, gradientPreviousTexture.yMax + borderOffset, colorKeyWidth, colorKeyHeight);

                // highlight selected existed key
                if (i == selectedColorKeyIndex)
                {
                    EditorGUI.DrawRect(new Rect(colorKeyRect.x - 2, colorKeyRect.y - 2, colorKeyRect.width + 4, colorKeyRect.height + 4), Color.black);
                }

                // draw color key rect
                EditorGUI.DrawRect(colorKeyRect, colorKey.Color);
                colorKeyRects[i] = colorKeyRect;
            }
        }
    }
}