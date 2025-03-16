using UnityEngine;
using UnityEditor;

namespace EasyTransition
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(TransitionSettings))]
    public class TransitionSettingsEditor : Editor
    {
        public Texture transitionManagerSettingsLogo;
        SerializedProperty transitionsList;

        void OnEnable()
        {
            transitionsList = serializedObject.FindProperty("transitions");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var bgTexture = new Texture2D(1, 1, TextureFormat.RGBAFloat, false);
            var style = new GUIStyle(GUI.skin.box);
            style.normal.background = bgTexture;

            GUILayout.Box(transitionManagerSettingsLogo, style, GUILayout.Width(Screen.width - 20), GUILayout.Height(Screen.height / 15));

            EditorGUILayout.Space();

            DrawDefaultInspector();
            serializedObject.ApplyModifiedProperties();
        }
    }

}

