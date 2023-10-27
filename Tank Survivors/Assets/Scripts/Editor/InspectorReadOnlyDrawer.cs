using UnityEditor;
using UnityEngine;

namespace Common
{
    [CustomPropertyDrawer(typeof(InspectorReadOnlyAttribute))]
    public class InspectorReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool prev_gui_state = GUI.enabled;
            GUI.enabled = false;
            _ = EditorGUI.PropertyField(position, property, label);
            GUI.enabled = prev_gui_state;
        }
    }
}
