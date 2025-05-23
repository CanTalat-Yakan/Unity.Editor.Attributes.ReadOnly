#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    /// <summary>
    /// Provides a custom property drawer for fields marked with the <see cref="ShowOnlyAttribute"/>.
    /// </summary>
    /// <remarks>This drawer renders the field as a read-only label in the Unity Inspector, displaying the
    /// field's name and value. It is intended for use with fields that should be visible in the Inspector but not
    /// editable by the user.</remarks>
    [CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
    public class ShowOnlyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Renders the custom GUI for a serialized property in the Unity Inspector.
        /// </summary>
        /// <remarks>This method overrides the default behavior to display the property's name and value
        /// in a custom layout. The property's value is retrieved and displayed as a string.</remarks>
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

        /// <summary>
        /// Calculates the height required to display the content of a serialized property in the inspector.
        /// </summary>
        /// <param name="property">The serialized property whose content height is being calculated.</param>
        /// <param name="label">The label associated with the property, typically displayed in the inspector.</param>
        /// <returns>The height, in pixels, required to render the property's content with word wrapping applied.</returns>
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