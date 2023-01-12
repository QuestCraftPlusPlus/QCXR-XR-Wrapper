using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace YVR.Core.XR
{
    [InitializeOnLoad]
    public class YVRSDKSettingEditor : EditorWindow
    {
        public static YVRSDKSettingEditor s_window;
        public static string s_assetPath = "Assets/XR/Resources/";
        public static string s_appID = "";
        public static List<GraphicsDeviceType> recommendedGraphicsAPITarget = new List<GraphicsDeviceType>() { GraphicsDeviceType.OpenGLES3 };
        public const UIOrientation recommendedOrientation = UIOrientation.LandscapeLeft;
        public const AndroidArchitecture androidArchitecture = AndroidArchitecture.ARM64;
        private bool m_ToggleGraphicsTarget = true;
        private bool m_ToggleOrientation = true;
        private bool m_ToggleSetAppID = true;
        private bool m_ToggleArchitecture = true;
        private static bool s_AppIDCheck = false;
        private GUIContent m_GUIContent = new GUIContent();
        private static string s_StrWindowName = "YVR SDK Setting";
        private string m_StrSeparate = "______________________________________________________________________________________________________________________________________________";
        private string m_StrNoticeText = "Notice: Recommended project settings for YVR SDK";
        private string m_StrApplied = "Applied";
        private string m_StrBtnApply = "Apply";
        private string m_StrBtnClose = "Close";
        private string m_StrFirstTitle = "1. Graphics Apis Setting";
        private string m_StrSecondTitle = "2. Orientation Setting";
        private string m_StrThirdTitle = "3. App ID ";
        private string m_StrFourthTitle = "4. Target Architectures";
        private string m_StrCurrent = "     Current: ";
        private string m_StrRecommended = "     Recommended: ";

        private GUIStyle m_StyleApply;
        private GUIStyle m_StyleSlide;
        private GUIStyle m_StyleTitleMiddle;
        private GUIStyle m_StyleTitleSmall;
        private GUIStyle m_StyleBoldLabel;
        private List<GraphicsDeviceType> m_CurrentGraphicsDeviceTypes;

        static YVRSDKSettingEditor()
        {
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            s_AppIDCheck = IsAppIDChecked();
            if (s_AppIDCheck)
            {
                s_appID = YVRPlatformSetting.Instance.appID;
            }

            bool allApplied = IsAllApplied();
            bool showWindow = !IsIgnoreWindow() && !allApplied;

            if (showWindow)
            {
                ShowSettingWindow();
            }

            EditorApplication.update -= Update;
        }

        private static void ShowSettingWindow()
        {
            if (s_window == null)
            {
                s_window = (YVRSDKSettingEditor)GetWindow(typeof(YVRSDKSettingEditor), true, s_StrWindowName, true);
                s_window.autoRepaintOnSceneChange = true;
                s_window.minSize = new Vector2(710, 550);
            }
        }

        private static bool IsIgnoreWindow()
        {
            string path = s_assetPath + typeof(YVRSDKSettingAsset).ToString() + ".asset";
            if (File.Exists(path))
            {
                YVRSDKSettingAsset asset = AssetDatabase.LoadAssetAtPath<YVRSDKSettingAsset>(path);
                return asset.ignoreSDKSetting;
            }

            return false;
        }

        public static bool IsAppIDChecked()
        {
            string path = s_assetPath + typeof(YVRSDKSettingAsset).ToString() + ".asset";
            if (File.Exists(path))
            {
                YVRSDKSettingAsset asset = AssetDatabase.LoadAssetAtPath<YVRSDKSettingAsset>(path);
                return asset.appIDChecked;
            }
            return false;
        }

        public void OnGUI()
        {
            SettingLayoutStyle();
            m_GUIContent.text = s_StrWindowName;
            if (s_window != null)
            {
                s_window.titleContent = m_GUIContent;
            }

            m_CurrentGraphicsDeviceTypes = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android).ToList();
            ShowNoticeText();
            GUILayout.Label(m_StrSeparate, m_StyleSlide);
            bool graphicsAPIEquals = recommendedGraphicsAPITarget.Count == m_CurrentGraphicsDeviceTypes.Count
                && recommendedGraphicsAPITarget.Except(m_CurrentGraphicsDeviceTypes).Count() == 0;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrFirstTitle, m_StyleTitleMiddle);
            EditorConfigurations(graphicsAPIEquals, ref m_ToggleGraphicsTarget);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrCurrent, m_StyleTitleSmall);
            GUILayout.Label(GetCurrentGraphicsAPITargetStr(), m_StyleBoldLabel);
            GUILayout.Label(m_StrRecommended, m_StyleTitleSmall);
            GUILayout.Label(GetRecommendedGraphicsAPITargetStr(), m_StyleBoldLabel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrSecondTitle, m_StyleTitleMiddle);
            EditorConfigurations(PlayerSettings.defaultInterfaceOrientation == recommendedOrientation, ref m_ToggleOrientation);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrCurrent, m_StyleTitleSmall);
            GUILayout.Label(PlayerSettings.defaultInterfaceOrientation.ToString(), m_StyleBoldLabel);
            GUILayout.Label(m_StrRecommended, m_StyleTitleSmall);
            GUILayout.Label(recommendedOrientation.ToString(), m_StyleBoldLabel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrThirdTitle, m_StyleTitleMiddle);
            EditorGUILayout.EndHorizontal();

            ConfigEntitlementCheck(ref m_ToggleSetAppID);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrFourthTitle, m_StyleTitleMiddle);
            EditorConfigurations(PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android) == ScriptingImplementation.IL2CPP &&
                PlayerSettings.Android.targetArchitectures == androidArchitecture, ref m_ToggleArchitecture);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(m_StrCurrent, m_StyleTitleSmall);
            GUILayout.Label(PlayerSettings.Android.targetArchitectures.ToString(), m_StyleBoldLabel);
            GUILayout.Label(m_StrRecommended, m_StyleTitleSmall);
            GUILayout.Label(androidArchitecture.ToString(), m_StyleBoldLabel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(140));
            if (GUILayout.Button(m_StrBtnApply, m_StyleApply, GUILayout.Width(100), GUILayout.Height(30)))
            {
                EditorApplication.delayCall += OnClickApply;
            }

            EditorGUILayout.LabelField("", GUILayout.Width(200));
            if (GUILayout.Button(m_StrBtnClose, GUILayout.Width(100), GUILayout.Height(30)))
            {
                OnClickClose();
            }

            EditorGUILayout.EndHorizontal();
        }

        private string GetCurrentGraphicsAPITargetStr()
        {
            string currentGraphicsAPITargetStr = "";
            foreach (var item in m_CurrentGraphicsDeviceTypes)
            {
                currentGraphicsAPITargetStr += $"{item.ToString()};  ";
            }

            return currentGraphicsAPITargetStr;
        }

        private string GetRecommendedGraphicsAPITargetStr()
        {
            string graphicsAPITargetStr = "";
            foreach (var item in recommendedGraphicsAPITarget)
            {
                graphicsAPITargetStr += $"{item.ToString()};  ";
            }

            return graphicsAPITargetStr;
        }

        private void SettingLayoutStyle()
        {
            m_StyleSlide = new GUIStyle();
            m_StyleSlide.normal.textColor = Color.white;
            m_StyleTitleMiddle = new GUIStyle("ObjectPickerLargeStatus");
            m_StyleTitleMiddle.fontSize = 15;
            m_StyleTitleSmall = new GUIStyle("ObjectPickerLargeStatus");
            m_StyleTitleSmall.fontSize = 14;
            m_StyleTitleSmall.normal.textColor = Color.yellow;
            m_StyleBoldLabel = new GUIStyle("ObjectPickerLargeStatus");
            m_StyleBoldLabel.fontSize = 12;
            m_StyleBoldLabel.contentOffset = new Vector2(3, 2);
            m_StyleApply = new GUIStyle("LargeButton");
            m_StyleApply.alignment = TextAnchor.MiddleCenter;
        }

        public void OnClickApply()
        {
            YVRPlatformSetting.Instance.appID = s_appID;
            if (m_ToggleSetAppID)
            {
                s_AppIDCheck = true;
                SaveAssetAppIDChecked();
            }

            if (m_ToggleOrientation && PlayerSettings.defaultInterfaceOrientation != recommendedOrientation)
            {
                PlayerSettings.defaultInterfaceOrientation = recommendedOrientation;
            }

            List<GraphicsDeviceType> currentGraphicsDeviceTypes = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android).ToList();
            bool graphicsAPIEquals = recommendedGraphicsAPITarget.Count == currentGraphicsDeviceTypes.Count && recommendedGraphicsAPITarget.Except(currentGraphicsDeviceTypes).Count() == 0;
            if (m_ToggleGraphicsTarget && !graphicsAPIEquals)
            {
                PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, recommendedGraphicsAPITarget.ToArray());
            }

            if (m_ToggleArchitecture && PlayerSettings.Android.targetArchitectures != androidArchitecture)
            {
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
                PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            }

            Close();
        }

        public void OnClickClose()
        {
            bool allApplied = IsAllApplied();
            if (allApplied)
            {
                Close();
            }
            else
            {
                YVRSettingMessageBoxEditor.Init();
            }
        }

        public static bool IsAllApplied()
        {
            List<GraphicsDeviceType> currentGraphicsDeviceTypes = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android).ToList();
            bool graphicsAPIEquals = recommendedGraphicsAPITarget.Count == currentGraphicsDeviceTypes.Count && recommendedGraphicsAPITarget.Except(currentGraphicsDeviceTypes).Count() == 0;
            bool notApplied = !graphicsAPIEquals || !s_AppIDCheck || (PlayerSettings.defaultInterfaceOrientation != recommendedOrientation);
            return !notApplied;
        }

        private void EditorConfigurations(bool enable, ref bool toggle)
        {
            GUIStyle styleApplied = new GUIStyle();
            styleApplied.contentOffset = new Vector2(300, 8);
            styleApplied.fontSize = 14;
            styleApplied.normal.textColor = Color.green;
            if (enable)
            {
                GUILayout.Label(m_StrApplied, styleApplied);
            }
            else
            {
                EditorGUILayout.LabelField("", GUILayout.Width(300));
                toggle = EditorGUILayout.Toggle(toggle, GUILayout.Height(30));
            }
        }

        private void ConfigEntitlementCheck(ref bool toggle)
        {
            EditorGUILayout.BeginHorizontal();
            GUIStyle styleApplied = new GUIStyle();
            styleApplied.normal.textColor = Color.green;
            if (toggle)
            {
                APPIDFiledEditorConfigurations();
                if (s_appID != "" && s_appID != YVRPlatformSetting.Instance.appID)
                {
                    YVRPlatformSetting.Instance.appID = s_appID;
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        private void APPIDFiledEditorConfigurations()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(10));
            s_appID = EditorGUILayout.TextField(s_appID, GUILayout.Width(350.0f), GUILayout.Height(25));
            EditorGUILayout.EndHorizontal();
        }

        private void ShowNoticeText()
        {
            EditorGUILayout.BeginHorizontal();
            GUIStyle styleNoticeText = new GUIStyle("NotificationText");
            styleNoticeText.alignment = TextAnchor.UpperCenter;
            styleNoticeText.fontSize = 20;
            GUILayout.Label(m_StrNoticeText, styleNoticeText, GUILayout.Height(110));
            EditorGUILayout.EndHorizontal();
        }

        private void SaveAssetAppIDChecked()
        {
            YVRSDKSettingAsset asset;
            string assetPath = s_assetPath + typeof(YVRSDKSettingAsset).ToString() + ".asset";
            if (File.Exists(assetPath))
            {
                asset = AssetDatabase.LoadAssetAtPath<YVRSDKSettingAsset>(assetPath);
            }
            else
            {
                asset = CreateInstance<YVRSDKSettingAsset>();
                ScriptableObjectUtility.CreateAsset(asset, s_assetPath);
            }

            asset.appIDChecked = true;
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }
}
