using HurricaneVR.Framework.Components;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRButton))]
    public class HVRButtonEditor : UnityEditor.Editor
    {
        private SerializedProperty SP_StartPosition;
        private SerializedProperty SP_Threshold;
        private SerializedProperty SP_UpThreshold;

        public HVRButton Button { get; private set; }

        protected void OnEnable()
        {
            SP_StartPosition = serializedObject.FindProperty("StartPosition");
            SP_Threshold = serializedObject.FindProperty("Threshold");
            SP_UpThreshold = serializedObject.FindProperty("UpThreshold");
            Button = target as HVRButton;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (SP_UpThreshold.floatValue > SP_Threshold.floatValue)
            {
                EditorGUILayout.HelpBox("Up Threshold should not be greater than Threshold.", MessageType.Error);
            }

            var position = Button.transform.localPosition;
            position.Scale(Button.Axis);
            var startPosition = SP_StartPosition.vector3Value;
            startPosition.Scale(Button.Axis);
            var valid = true;
            if (position.x > startPosition.x || position.y > startPosition.y || position.z > startPosition.z)
            {
                valid = false;
                EditorGUILayout.HelpBox("Button position cannot be greater than the start position.", MessageType.Error);
            }

            if (GUILayout.Button("Save Initial Position"))
            {
                SP_StartPosition.vector3Value = Button.transform.localPosition;
                serializedObject.ApplyModifiedProperties();
            }

            if (GUILayout.Button("Save Threshold"))
            {
                if (valid)
                {
                    var delta = SP_StartPosition.vector3Value - Button.transform.localPosition;
                    delta.Scale(Button.Axis);
                    SP_Threshold.floatValue = delta.magnitude;
                    serializedObject.ApplyModifiedProperties();
                }
            }

            if (GUILayout.Button("Save Up Threshold"))
            {
                if (valid)
                {
                    var delta = SP_StartPosition.vector3Value - Button.transform.localPosition;
                    delta.Scale(Button.Axis);
                    SP_UpThreshold.floatValue = delta.magnitude;
                    serializedObject.ApplyModifiedProperties();
                }
            }

            if (GUILayout.Button("Return To Start Position"))
            {
                Button.transform.localPosition = SP_StartPosition.vector3Value;
            }
        }
    }
}