using HurricaneVR.Framework.Weapons;
using HurricaneVR.Framework.Weapons.Guns;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRGunBolt))]
    public class HVRGunBoltEditor : UnityEditor.Editor
    {
        private SerializedProperty ForwardPosition;
        private SerializedProperty BackwardPosition;
        public HVRGunBolt component;

        protected void OnEnable()
        {
            ForwardPosition = serializedObject.FindProperty("ForwardPosition");
            BackwardPosition = serializedObject.FindProperty("BackwardPosition");
            component = target as HVRGunBolt;

        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("1. Save the forward position of the bolt.\r\n" +
                                    "2. Save the backward position of the bolt.\r\n", MessageType.Info);

            DrawButtons("Forward", ForwardPosition);
            DrawButtons("Backward", BackwardPosition);

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