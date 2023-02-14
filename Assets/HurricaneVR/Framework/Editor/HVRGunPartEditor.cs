using HurricaneVR.Framework.Weapons;
using HurricaneVR.Framework.Weapons.Guns;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRGunPart), true)]
    public class HVRGunPartEditor : UnityEditor.Editor
    {
        private SerializedProperty ForwardPosition;
        private SerializedProperty BackwardPosition;
        public HVRGunPart component;

        protected void OnEnable()
        {
            ForwardPosition = serializedObject.FindProperty("ForwardPosition");
            BackwardPosition = serializedObject.FindProperty("BackwardPosition");
            component = target as HVRGunPart;

        }

        public override void OnInspectorGUI()
        {
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