
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YVR.Core.XR
{
    [CustomEditor(typeof(YVRPlatformSetting))]
    public class YVRPlatformSettingEditor : UnityEditor.Editor
    {
        private string m_StrAppId = "App ID ";
        private string m_StrHelpBox = "APPID is required for Entitlement Check";
        private string m_StrAppidTip = "The APPID is required to run an Entitlement Check. Create / Find your APPID Here:";
        private string m_StrYVRUrl = "https://developer.yvrdream.com/yvrdvcenter";

        [MenuItem("YVR" + "/Platform Settings")]
        public static void Edit()
        {
            Selection.activeObject = YVRPlatformSetting.Instance;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrAppId, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            YVRPlatformSetting.Instance.appID =
                EditorGUILayout.TextField(YVRPlatformSetting.Instance.appID, GUILayout.Width(350.0f));
            EditorGUILayout.EndHorizontal();

            YVRSDKSettingEditor.s_appID = YVRPlatformSetting.Instance.appID;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrAppidTip, GUILayout.Width(500));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUIStyle style = new GUIStyle();
            style.normal.textColor = new Color(0, 122f / 255f, 204f / 255f);
            if (GUILayout.Button("" + m_StrYVRUrl, style,
                GUILayout.Width(200)))
            {
                Application.OpenURL(m_StrYVRUrl);
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(YVRPlatformSetting.Instance);
                GUI.changed = false;
            }
        }
    }
}