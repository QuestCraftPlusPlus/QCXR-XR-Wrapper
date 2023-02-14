using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Components.Creators;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRPhysicsLeverCreator))]
    public class HVRPhysicsLeverCreatorEditor : UnityEditor.Editor
    {
        protected void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("1. Add the anchor that will hold the RB to joint to..\r\n" +
                                    "2. Add the lever that will hold the lever RB and necessary components.\r\n" +
                                    "3. Press Create Lever", MessageType.Info);

            var creator = target as HVRPhysicsLeverCreator;
            var anchor = creator.Anchor;
            var leverTransform = creator.Lever;
            var axis = creator.Axis;

            if (!anchor)
            {
                EditorGUILayout.HelpBox("Populate the Anchor field.", MessageType.Error);
            }

            if (!leverTransform)
            {
                EditorGUILayout.HelpBox("Populate the Lever field.", MessageType.Error);
            }

            if (GUILayout.Button("Create Lever") && anchor && leverTransform && anchor != leverTransform)
            {
                Undo.SetCurrentGroupName("LeverCreate");

                var anchorRb = anchor.UndoEnsureComponent<Rigidbody>();
                var tracker = new SerializedObject(leverTransform.gameObject.UndoEnsureComponent<HVRRotationTracker>());
                var rb = leverTransform.UndoEnsureComponent<Rigidbody>();
                var grabbable = new SerializedObject(leverTransform.UndoEnsureComponent<HVRGrabbable>());
                var lever = new SerializedObject(leverTransform.UndoEnsureComponent<HVRPhysicsLever>());

                anchorRb.isKinematic = true;

                rb.useGravity = false;
                rb.mass = creator.Mass;

                grabbable.FindProperty("Stationary").boolValue = true;
                grabbable.FindProperty("ForceGrabbable").boolValue = false;

                tracker.FindProperty("Axis").enumValueIndex = (int)axis;

                lever.FindProperty("ConnectedBody").objectReferenceValue = anchorRb;
                lever.FindProperty("Axis").enumValueIndex = (int)axis;
                lever.FindProperty("MinAngle").floatValue = creator.MinAngle;
                lever.FindProperty("MaxAngle").floatValue = creator.MaxAngle;
                lever.FindProperty("LimitRotation").boolValue = true;
                lever.FindProperty("GrabbedDamper").floatValue = creator.GrabbedDamper;
                lever.FindProperty("Damper").floatValue = creator.Damper;

                lever.ApplyModifiedProperties();
                grabbable.ApplyModifiedProperties();
                tracker.ApplyModifiedProperties();
                EditorUtility.SetDirty(anchorRb);
                EditorUtility.SetDirty(rb);

                Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
            }

            base.OnInspectorGUI();
        }
    }
}