#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    /// <summary>
    /// Provides a custom property drawer for fields marked with the <see cref="ReadOnlyAttribute"/>.
    /// </summary>
    /// <remarks>This drawer renders the field as a read-only label in the Unity Inspector, displaying the
    /// field's name and value. It is intended for use with fields that should be visible in the Inspector but not
    /// editable by the user.</remarks>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, property.displayName);

            string valueString;
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                int index = property.enumValueIndex;
                string enumName = (index >= 0 && index < property.enumDisplayNames.Length)
                    ? property.enumDisplayNames[index]
                    : "Unknown";
                valueString = $"{enumName} ({index})";
            }
            else valueString = InspectorHookUtilities.GetPropertyValue(property)?.ToString();

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