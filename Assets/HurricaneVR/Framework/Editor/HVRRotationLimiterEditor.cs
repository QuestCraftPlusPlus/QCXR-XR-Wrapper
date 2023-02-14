using System;
using HurricaneVR.Framework.Components;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRRotationLimiter))]
    public class HVRRotationLimiterEditor : UnityEditor.Editor
    {

        protected void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {

            EditorGUILayout.HelpBox("Used in conjunction with the HVRRotationTracker." +
                                    $"{Environment.NewLine}Joint limits will be placed on the axis of rotation assigned on the tracker " +
                                    "when the AngleFromStart reported by the tracker becomes in range of that limit.", MessageType.Info);


            base.OnInspectorGUI();
        }
    }
}