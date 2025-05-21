#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    [CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
    public class ShowOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, property.displayName);

            var valueString = InspectorHookUtilities.GetPropertyValue(property).ToString();
            var valuePosition = new Rect(position)
            {
                x = string.IsNullOrEmpty(property.displayName) ? 16 : 20 + EditorGUIUtility.labelWidth,
                width = position.width,
            };
            EditorGUI.LabelField(valuePosition, valueString, EditorStyles.wordWrappedLabel);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float indent = EditorGUI.indentLevel * 16;
            float width = EditorGUIUtility.currentViewWidth;

            float textHeight = EditorStyles.wordWrappedLabel.CalcHeight(new GUIContent(InspectorHookUtilities.GetPropertyValue(property).ToString()), width);

            return textHeight;
        }
    }
}
#endif