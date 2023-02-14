using System.Linq;
using HurricaneVR.Framework.Core.Stabbing;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    [CustomEditor(typeof(HVRStabber))]
    public class HVRStabberEditor : UnityEditor.Editor
    {
        public HVRStabber Stabber;
        public SerializedProperty SP_IgnoreColliders;

        protected void OnEnable()
        {
            Stabber = target as HVRStabber;
            SP_IgnoreColliders = serializedObject.FindProperty("CollidersToIgnore");
        }

        public override void OnInspectorGUI()
        {
            if (!Stabber.Settings)
            {
                EditorGUILayout.HelpBox("Settings missing. Defaults will be used at runtime.", MessageType.Warning);
            }
            
            if (!Stabber.Tip)
            {
                EditorGUILayout.HelpBox("Tip Transform Is Unassigned.", MessageType.Error);
            }

            if (!Stabber.Base)
            {
                EditorGUILayout.HelpBox("Base Transform Is Unassigned.", MessageType.Error);
            }

            if (Stabber.StabbingColliders == null || Stabber.StabbingColliders.Length == 0)
            {
                EditorGUILayout.HelpBox("Stabbing Colliders is empty.", MessageType.Error);
            }

            if (!Stabber.Tip || !Stabber.Base)
            {

                if (GUILayout.Button("Add Transforms"))
                {
                    if (!Stabber.Tip)
                    {
                        var tipt = Stabber.transform.Find("Tip");
                        GameObject tip;
                        tip = tipt?.gameObject;

                        if (!tip)
                        {
                            tip = new GameObject("Tip");
                            tip.transform.parent = Stabber.transform;
                            tip.transform.ResetLocalProps();
                        }

                        Stabber.Tip = tip.transform;
                        serializedObject.ApplyModifiedProperties();
                    }

                    if (!Stabber.Base)
                    {
                        var baseTran = Stabber.transform.Find("Base");
                        GameObject baseObj;
                        baseObj = baseTran?.gameObject;

                        if (!baseObj)
                        {
                            baseObj = new GameObject("Base");
                            baseObj.transform.parent = Stabber.transform;
                            baseObj.transform.ResetLocalProps();
                        }

                        Stabber.Base = baseObj.transform;
                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }

            if (Stabber.CollidersToIgnore == null || Stabber.CollidersToIgnore.Length == 0)
            {
                EditorGUILayout.HelpBox("CollidersToIgnore is empty. Will Populate with all colliders.", MessageType.Warning);
            }

            if (GUILayout.Button("Ignore Object Colliders"))
            {
                foreach (var c in Stabber.GetComponents<Collider>().Where(c => Stabber.CollidersToIgnore == null || !Stabber.CollidersToIgnore.Contains(c)))
                {
                    var index = SP_IgnoreColliders.arraySize;
                    SP_IgnoreColliders.InsertArrayElementAtIndex(index);
                    SP_IgnoreColliders.GetArrayElementAtIndex(index).objectReferenceValue = c;
                }

                serializedObject.ApplyModifiedProperties();
            }

            if (GUILayout.Button("Ignore Child Colliders"))
            {
                foreach (var c in Stabber.GetComponentsInChildren<Collider>().Where(c => Stabber.CollidersToIgnore == null || !Stabber.CollidersToIgnore.Contains(c)))
                {
                    var index = SP_IgnoreColliders.arraySize;
                    SP_IgnoreColliders.InsertArrayElementAtIndex(index);
                    SP_IgnoreColliders.GetArrayElementAtIndex(index).objectReferenceValue = c;
                }

                serializedObject.ApplyModifiedProperties();
            }

            base.OnInspectorGUI();

            if (GUILayout.Button("Test Lock"))
            {
                Stabber.Lock();
            }
            
            if(GUILayout.Button("Test Unlock"))
            {
                Stabber.Unlock();
            }
         
        }
    }
}