using UnityEditor;
using UnityEngine;
using ViTiet.UnityExtension.TextureRender;

namespace ViTiet.UnityExtension.Editor
{
    [CustomPropertyDrawer(typeof(CustomGradient))]
    public class CustomGradientDrawer : PropertyDrawer
    {
        const int borderOffset = 10;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Event guiEvent = Event.current;

            // gradient label
            CustomGradient gradient = (CustomGradient)fieldInfo.GetValue(property.serializedObject.targetObject);

            // calculate gradient texture rect
            float textLabelWidth = GUI.skin.label.CalcSize(label).x + borderOffset;
            Rect gradientTextureRect = new Rect(position.x + textLabelWidth, position.y, position.width - textLabelWidth, position.height);

            if (guiEvent.type == EventType.Repaint)
            {
                // text label
                GUI.Label(position, label);

                // repaint gradient
                GUIStyle gradientStyle = new GUIStyle();
                gradientStyle.normal.background = gradient.GetTexure((int)position.width - 1);
                GUI.Label(gradientTextureRect, GUIContent.none, gradientStyle);
            }
            else
            {
                // check if mouse click
                if (guiEvent.type == EventType.MouseDown)
                {
                    // check if left mouse click and mouse position is within gradient texture rect
                    if (guiEvent.button == 0 && gradientTextureRect.Contains(guiEvent.mousePosition))
                    {
                        // open custom gradient editor window if not already opened
                        // focus on the custom gradient editor window if already opened
                        CustomGradientEditor window = EditorWindow.GetWindow<CustomGradientEditor>();

                        // feed reference to editor window
                        window.SetGradient(gradient);
                    }
                }
            }

        }
    }
}