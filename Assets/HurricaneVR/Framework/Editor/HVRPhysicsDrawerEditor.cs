using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Shared;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRPhysicsDrawer))]
    public class HVRPhysicsDrawerEditor : UnityEditor.Editor
    {
        private SerializedProperty SP_StartPosition;
        private SerializedProperty SP_EndPosition;
        private SerializedProperty SP_OpenPosition;
        public HVRPhysicsDrawer component;
        private bool _setupExpanded;

        protected void OnEnable()
        {
            SP_StartPosition = serializedObject.FindProperty("StartPosition");
            SP_EndPosition = serializedObject.FindProperty("EndPosition");
            SP_OpenPosition = serializedObject.FindProperty("OpenPosition");
            component = target as HVRPhysicsDrawer;

        }

        public override void OnInspectorGUI()
        {


            var dir = SP_EndPosition.vector3Value - SP_StartPosition.vector3Value;
            dir.Normalize();

            _setupExpanded = EditorGUILayout.Foldout(_setupExpanded, "Setup Helpers");
            if (_setupExpanded)
            {
                EditorGUILayout.HelpBox("1. Choose the local axis the drawer will move on.\r\n" +
                                        "2. Save the start position of the drawer.\r\n" +
                                        "3. Save the end position of the drawer.\r\n" +
                                        "4. Save the SFX threshold position of the drawer.\r\n", MessageType.Info);



                DrawButtons("Start", SP_StartPosition);
                DrawButtons("End", SP_EndPosition);
                DrawButtons("SFX Position", SP_OpenPosition);
            }

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }

        private void DrawButtons(string label, SerializedProperty property)
        {

            GUILayout.BeginHorizontal();

            if (GUILayout.Button($"Save {label}"))
            {
                property.vector3Value = component.transform.localPosition;
            }


            if (GUILayout.Button($"GoTo {label}"))
            {
                Undo.RecordObject(component.transform, $"Goto {label}");
                component.transform.localPosition = property.vector3Value;
            }


            GUILayout.EndHorizontal();
        }
    }
}