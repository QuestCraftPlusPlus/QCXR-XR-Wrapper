using HurricaneVR.Framework.Core.Sockets;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRSocketableTags))]
    public class HVRSocketableTagsEditor : UnityEditor.Editor
    {
        private HVRSocketableTags tags;

        private void OnEnable()
        {
            tags = target as HVRSocketableTags;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Space(5);

            for (int i = 0; i < 32; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField($"{i}", GUILayout.Width(50));
                tags[i] = EditorGUILayout.TextField(tags[i]);

                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(5);
            
            EditorGUILayout.EndVertical();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(tags);
            }
        }
    }
}