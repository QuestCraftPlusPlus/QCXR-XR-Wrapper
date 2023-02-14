using System.IO;
using UnityEditor;
using UnityEngine;

namespace YVR.Core.XR
{

    public class YVRSettingMessageBoxEditor : EditorWindow
    {
        private static YVRSettingMessageBoxEditor s_Window;
        private static string s_StrWindowName = "Ignore the recommended configuration";
        private string m_StrTipInfo = "                                   No more prompted \n" + "             You can get recommended configuration from  \n" + "                            Development documentation.";
        private string m_StrBtnIgnore = "Ignore";
        private string m_StrBtnCancel = "Cancel";

        public static void Init()
        {
            s_Window = (YVRSettingMessageBoxEditor)GetWindow(typeof(YVRSettingMessageBoxEditor), true, s_StrWindowName, true);
            s_Window.autoRepaintOnSceneChange = true;
            s_Window.minSize = new Vector2(360, 200);
            s_Window.Show(true);
            Rect pos;
            if (YVRSDKSettingEditor.s_window != null)
            {
                Rect frect = YVRSDKSettingEditor.s_window.position;
                pos = new Rect(frect.x + 300, frect.y + 200, 200, 140);
            }
            else
            {
                pos = new Rect(700, 400, 200, 140);
            }

            s_Window.position = pos;
        }

        private void OnGUI()
        {
            for (int i = 0; i < 10; i++)
            {
                EditorGUILayout.Space();
            }

            GUILayout.Label(m_StrTipInfo);
            for (int i = 0; i < 3; i++)
            {
                EditorGUILayout.Space();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(20));
            if (GUILayout.Button(m_StrBtnIgnore, GUILayout.Width(100), GUILayout.Height(30)))
            {
                OnClickIgnore();
            }

            EditorGUILayout.LabelField("", GUILayout.Width(50));
            if (GUILayout.Button(m_StrBtnCancel, GUILayout.Width(130), GUILayout.Height(30)))
            {
                Close();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void OnClickIgnore()
        {
            SaveAssetDataBase();
            YVRSDKSettingEditor.s_window.Close();
            Close();
        }

        private void SaveAssetDataBase()
        {
            YVRSDKSettingAsset asset;
            string assetPath = YVRSDKSettingEditor.s_assetPath + typeof(YVRSDKSettingAsset).ToString() + ".asset";
            if (File.Exists(assetPath))
            {
                asset = AssetDatabase.LoadAssetAtPath<YVRSDKSettingAsset>(assetPath);
            }
            else
            {
                asset = CreateInstance<YVRSDKSettingAsset>();
                ScriptableObjectUtility.CreateAsset(asset, YVRSDKSettingEditor.s_assetPath);
            }

            asset.ignoreSDKSetting = true;
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}