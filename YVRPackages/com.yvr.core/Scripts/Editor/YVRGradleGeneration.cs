using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using System.IO;
using AndroidManifest;
using UnityEditor.Android;
using UnityEngine.Internal;


namespace YVR.Core.XR
{
    [ExcludeFromDocs]
    [InitializeOnLoad]
    public class YvrGradleGeneration : IPostGenerateGradleAndroidProject
    {
        private const string ToolName = "YVR/Tools/";
        private const string PrefName = "VR_ONLY";
        private const string MenuItemVROnly = ToolName + "VR mode";
        private static bool _vrOnly;
        private const string SaveDataPath = "Assets/XR/Resources/";
        private static readonly string SettingAssetPath = $"{SaveDataPath}{typeof(YVRSDKSettingAsset)}.asset";

        private static readonly string AssetFilePath =
            $"{Application.dataPath}/XR/Resources/{typeof(YVRSDKSettingAsset)}.asset";

        static YvrGradleGeneration()
        {
            EditorApplication.delayCall += OnDelayCall;
        }

        [MenuItem(MenuItemVROnly)]
        private static void ToggleVROnly()
        {
            _vrOnly = !_vrOnly;

            Menu.SetChecked(MenuItemVROnly, _vrOnly);

            GetVrOnlyTagInfo(out YVRSDKSettingAsset asset, _vrOnly).required = _vrOnly;

            var sourceFile = $"Assets/Plugins/Android/AndroidManifest.xml";
            if (File.Exists(sourceFile))
                ManifestPreprocessor.PatchAndroidManifest(asset.manifestTagInfosList, sourceFile);


            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("select vr only: " + _vrOnly);
        }

        private static void OnDelayCall()
        {
            var defaultValue = _vrOnly ? 0 : 1;
            _vrOnly = PlayerPrefs.GetInt(PrefName, defaultValue) != 0;
            _vrOnly = GetVrOnlyTagInfo(out YVRSDKSettingAsset asset, _vrOnly).required;
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            Menu.SetChecked(MenuItemVROnly, _vrOnly);

            Debug.Log("OnDelayCall: " + _vrOnly);
        }

        private static ManifestTagInfo GetVrOnlyTagInfo(out YVRSDKSettingAsset asset, bool required = true)
        {
            ManifestTagInfo tagInfo;
            if (File.Exists(AssetFilePath))
            {
                asset = AssetDatabase.LoadAssetAtPath<YVRSDKSettingAsset>(SettingAssetPath);
                asset.manifestTagInfosList ??= new List<ManifestTagInfo>();
                tagInfo =
                    asset.manifestTagInfosList.Find(info => info.attrValue == "com.yvr.application.mode");
                if (tagInfo == null)
                {
                    tagInfo = CreateVrOnlyInfo(required);
                    asset.manifestTagInfosList.Add(tagInfo);
                }
            }
            else
            {
                asset = ScriptableObject.CreateInstance<YVRSDKSettingAsset>();
                tagInfo = CreateVrOnlyInfo(true);
                asset.manifestTagInfosList = new List<ManifestTagInfo>() { tagInfo };
                ScriptableObjectUtility.CreateAsset(asset, SaveDataPath);
            }

            return tagInfo;
        }

        private static ManifestTagInfo CreateVrOnlyInfo(bool required)
        {
            return new ManifestTagInfo()
            {
                nodePath = "/manifest/application",
                tag = "meta-data",
                attrName = "name",
                attrValue = "com.yvr.application.mode",
                attrs = new[] { "value", "vr_only" },
                required = required,
                modifyIfFound = true
            };
        }


        private void PatchAndroidManifest(string path)
        {
            string manifestFolder = Path.Combine(path, "src/main");
            string file = manifestFolder + "/AndroidManifest.xml";

            if (File.Exists(AssetFilePath))
            {
                YVRSDKSettingAsset asset = AssetDatabase.LoadAssetAtPath<YVRSDKSettingAsset>(SettingAssetPath);
                if (asset.manifestTagInfosList != null)
                    ManifestPreprocessor.PatchAndroidManifest(asset.manifestTagInfosList, file);
            }
            else
            {
                Debug.LogError($"{AssetFilePath} is not exit");
            }
        }

        #region build callback

        int IOrderedCallback.callbackOrder => 9999;

        void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
        {
            PatchAndroidManifest(path);
        }

        #endregion build callback
    }
}