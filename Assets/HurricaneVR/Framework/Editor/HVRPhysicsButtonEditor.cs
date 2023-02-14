using System.Diagnostics.Tracing;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Shared;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRPhysicsButton))]
    public class HVRPhysicsButtonEditor : UnityEditor.Editor
    {
        private SerializedProperty SP_StartPosition;
        private SerializedProperty SP_EndPosition;
        private SerializedProperty SP_Threshold;
        private SerializedProperty SP_UpThreshold;
        public HVRPhysicsButton component;

        private bool _setupExpanded;

        protected void OnEnable()
        {
            SP_StartPosition = serializedObject.FindProperty("StartPosition");
            SP_EndPosition = serializedObject.FindProperty("EndPosition");
            SP_Threshold = serializedObject.FindProperty("DownThreshold");
            SP_UpThreshold = serializedObject.FindProperty("ResetThreshold");
            component = target as HVRPhysicsButton;

        }

        public override void OnInspectorGUI()
        {
      
            var dir = SP_EndPosition.vector3Value - SP_StartPosition.vector3Value;
            dir.Normalize();

            _setupExpanded = EditorGUILayout.Foldout(_setupExpanded, "Setup Helpers");
            if (_setupExpanded)
            {
                EditorGUILayout.HelpBox("1. Choose the local axis the button will move on.\r\n" +
                                        "2. Save the start position of the button.\r\n" +
                                        "3. Save the end position of the button.\r\n" +
                                        "4. Save the down and reset positions.\r\n" +
                                        "5. Return the transform to start by pressing the return button.\r\n" +
                                        "6. If the Connected Body is left blank, the button will be jointed to the world and cannot be moved."
                    , MessageType.Info);



                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Save Start"))
                {
                    SP_StartPosition.vector3Value = component.transform.localPosition;
                    serializedObject.ApplyModifiedProperties();
                }

                if (GUILayout.Button("GoTo Start"))
                {
                    component.transform.localPosition = SP_StartPosition.vector3Value;
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Save End"))
                {
                    SP_EndPosition.vector3Value = component.transform.localPosition;
                    serializedObject.ApplyModifiedProperties();
                }

                if (GUILayout.Button("GoTo End"))
                {
                    component.transform.localPosition = SP_EndPosition.vector3Value;
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();


                if (GUILayout.Button("Save Down"))
                {
                    var delta = SP_StartPosition.vector3Value - component.transform.localPosition;
                    SP_Threshold.floatValue = delta.magnitude;
                    serializedObject.ApplyModifiedProperties();
                }


                if (GUILayout.Button("GoTo Down"))
                {
                    component.transform.localPosition = SP_StartPosition.vector3Value + SP_Threshold.floatValue * dir;
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Save Reset"))
                {
                    var delta = SP_StartPosition.vector3Value - component.transform.localPosition;
                    SP_UpThreshold.floatValue = delta.magnitude;
                    serializedObject.ApplyModifiedProperties();
                }



                if (GUILayout.Button("GoTo Reset"))
                {
                    component.transform.localPosition = SP_StartPosition.vector3Value + SP_UpThreshold.floatValue * dir;
                }

                GUILayout.EndHorizontal();

                
            }

            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();


        }
    }
}