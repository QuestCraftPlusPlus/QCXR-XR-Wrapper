using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Weapons;
using HurricaneVR.Framework.Weapons.Guns;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRCockingHandle))]
    public class HVRCockingHandleEditor : UnityEditor.Editor
    {
        private SerializedProperty ForwardPosition;
        private SerializedProperty BackwardPosition;
        private SerializedProperty EjectPosition;
        private SerializedProperty ChamberRoundPosition;
        public HVRCockingHandle component;

        protected void OnEnable()
        {
            ForwardPosition = serializedObject.FindProperty("ForwardPosition");
            BackwardPosition = serializedObject.FindProperty("BackwardPosition");
            EjectPosition = serializedObject.FindProperty("EjectPosition");
            ChamberRoundPosition = serializedObject.FindProperty("ChamberRoundPosition");
            component = target as HVRCockingHandle;

        }

        public override void OnInspectorGUI()
        {
            //EditorGUILayout.HelpBox("1. Choose the local axis the drawer will move on.\r\n" +
            //                        "2. Save the start position of the drawer.\r\n" +
            //                        "3. Save the end position of the drawer.\r\n"
            //    //"4. Save the down and reset positions.\r\n" +
            //    //"5. Return the transform to start by pressing the return button.\r\n" +
            //    //"6. If the Connected Body is left blank, the button will be jointed to the world and cannot be moved."
            //    , MessageType.Info);

            DrawButtons("Forward", ForwardPosition);
            DrawButtons("Backward", BackwardPosition);
            DrawButtons("Eject", EjectPosition);
            DrawButtons("ChamberRound", ChamberRoundPosition);

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