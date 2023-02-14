using System;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Shared;
using UnityEditor;
using UnityEngine;

namespace HurricaneVR.Editor
{
    public static class HVREditorExtensions
    {
        public static void Toggle(string label, ref bool value)
        {
            value = EditorGUILayout.Toggle(label, value);
        }

        public static void IntField(string label, ref int value)
        {
            value = EditorGUILayout.IntField(label, value);
        }

        public static void FloatField(string label, ref float value)
        {
            value = EditorGUILayout.FloatField(label, value);
        }

        public static void EnumField<T>(string label, ref T value) where T : Enum
        {
            value = (T)EditorGUILayout.EnumPopup(label, value);
        }

        public static void ObjectField(string label, ref GameObject value, bool allowSceneObjects = true)
        {
            value = EditorGUILayout.ObjectField(label, value, typeof(GameObject), allowSceneObjects) as GameObject;
        }

        public static void ObjectField<T>(string label, ref T value, bool allowSceneObjects = true) where T : UnityEngine.Object
        {
            value = EditorGUILayout.ObjectField(label, value, typeof(T), allowSceneObjects) as T;
        }

        public static void JointField(string label, ref HVRJointSettings value)
        {
            value = EditorGUILayout.ObjectField(label, value, typeof(HVRJointSettings), false) as HVRJointSettings;
        }

        public static void StrengthField(string label, ref PDStrength value)
        {
            value = EditorGUILayout.ObjectField(label, value, typeof(PDStrength), false) as PDStrength;
        }

        public static void Header(string label)
        {
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        }

        public static void FloatRange(string label, ref float value, float min = 0f, float max = 1f)
        {
            var position = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
            position = EditorGUI.PrefixLabel(position, new GUIContent(label));
            value = EditorGUI.Slider(position, value, min, max);
        }
    }
}