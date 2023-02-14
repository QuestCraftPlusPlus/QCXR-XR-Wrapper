using HurricaneVR.Framework.Weapons;
using HurricaneVR.Framework.Weapons.Guns;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRTriggerAnimator))]
    public class HVRTriggerAnimatorEditor : UnityEditor.Editor
    {
        private SerializedProperty StartRotation;
        private SerializedProperty EndRotation;
        private SerializedProperty ForwardPosition;
        private SerializedProperty BackwardPosition;
        public HVRTriggerAnimator component;

        protected void OnEnable()
        {
            StartRotation = serializedObject.FindProperty("StartRotation");
            EndRotation = serializedObject.FindProperty("EndRotation");
            ForwardPosition = serializedObject.FindProperty("ForwardPosition");
            BackwardPosition = serializedObject.FindProperty("BackwardPosition");
            component = target as HVRTriggerAnimator;

        }

        public override void OnInspectorGUI()
        {
            DrawButtons("Start Rotation", StartRotation);
            DrawButtons("End Rotation", EndRotation);

            DrawButtons2("Forward", ForwardPosition);
            DrawButtons2("Backward", BackwardPosition);


            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }

        private void DrawButtons(string label, SerializedProperty property)
        {

            GUILayout.BeginHorizontal();

            if (GUILayout.Button($"Save {label}"))
            {
                property.quaternionValue = component.transform.localRotation;
            }


            if (GUILayout.Button($"GoTo {label}"))
            {
                Undo.RecordObject(component.transform, $"Goto {label}");
                component.transform.localRotation = property.quaternionValue;
            }


            GUILayout.EndHorizontal();
        }

        private void DrawButtons2(string label, SerializedProperty property)
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