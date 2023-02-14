using HurricaneVR.Framework.Components;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRPhysicsDial), true)]
    public class HVRPhysicsDialEditor : UnityEditor.Editor
    {
        public SerializedProperty SP_StartRotation;
        public HVRPhysicsDial Dial;

        protected void OnEnable()
        {
            SP_StartRotation = serializedObject.FindProperty("JointStartRotation");
            Dial = target as HVRPhysicsDial;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Save Joint Start Rotation"))
            {
                SP_StartRotation.quaternionValue = Dial.transform.localRotation;

            }

            EditorGUILayout.HelpBox("Joints limits are based on their starting rotation and have a maximum limit of 177." +
                                    "\r\n1. Rotate the transform to where you want the joint limits to start from and press 'Save Joint Start Rotation'. " +
                                    "\r\n2. Rotate the transform to where you want it start at, making sure it's within your defined limit." +
                                    "\r\n3. Choose the local axis of rotation." +
                                    "\r\n4. If you needs limits larger than 177 degrees in either direction. Use HVRRotationTracker + HVRRotationLimiter."
                , MessageType.Info);

            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();


        }
    }
}