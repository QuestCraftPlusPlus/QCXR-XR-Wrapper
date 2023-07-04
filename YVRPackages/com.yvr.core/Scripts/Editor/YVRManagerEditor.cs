using System.Collections.Generic;
using System.IO;
using AndroidManifest;
using UnityEditor;
using UnityEngine;

namespace YVR.Core.XR
{
    [CustomEditor(typeof(YVRManager))]
    public class YVRManagerEditor : UnityEditor.Editor
    {
        private const string k_SaveDataPath = "Assets/XR/Resources/";
        private static readonly string s_SettingAssetPath = $"{k_SaveDataPath}{typeof(YVRSDKSettingAsset)}.asset";

        public override void OnInspectorGUI()
        {
            GUI.changed = false;
            DrawDefaultInspector();

            SetHandTrackingInfo(out YVRSDKSettingAsset asset);
            var sourceFile = $"Assets/Plugins/Android/AndroidManifest.xml";
            if (File.Exists(sourceFile))
                ManifestPreprocessor.PatchAndroidManifest(asset.manifestTagInfosList, sourceFile);
        }

        private void SetHandTrackingInfo(out YVRSDKSettingAsset asset)
        {
            ManifestTagInfo permissionTagInfo = null;
            ManifestTagInfo handTrackingTagInfo = null;
            if (File.Exists(s_SettingAssetPath))
            {
                asset = AssetDatabase.LoadAssetAtPath<YVRSDKSettingAsset>(s_SettingAssetPath);
                permissionTagInfo =
                    asset.manifestTagInfosList.Find(info => info.attrValue == "com.yvr.permission.HAND_TRACKING");
                handTrackingTagInfo =
                    asset.manifestTagInfosList.Find(info => info.attrValue == "yvr.software.handtracking");

                asset.manifestTagInfosList ??= new List<ManifestTagInfo>();
                if (permissionTagInfo == null)
                {
                    permissionTagInfo = CreateHandTrackingPermission();
                    asset.manifestTagInfosList.Add(permissionTagInfo);
                }

                permissionTagInfo.required = YVRManager.instance.handTrackingSupport !=
                                             YVRManager.HandTrackingSupport.ControllersOnly;
                bool isHandsOnly = YVRManager.instance.handTrackingSupport == YVRManager.HandTrackingSupport.HandsOnly;
                if (handTrackingTagInfo == null)
                {
                    handTrackingTagInfo = CreateHandTrackingInfo(isHandsOnly);
                    asset.manifestTagInfosList.Add(handTrackingTagInfo);
                }

                handTrackingTagInfo.attrs = new[] { "required", isHandsOnly.ToString().ToLower() };
                handTrackingTagInfo.required = YVRManager.instance.handTrackingSupport !=
                                               YVRManager.HandTrackingSupport.ControllersOnly;
            }
            else
            {
                asset = CreateInstance<YVRSDKSettingAsset>();
                if (YVRManager.instance.handTrackingSupport != YVRManager.HandTrackingSupport.ControllersOnly)
                {
                    permissionTagInfo = CreateHandTrackingPermission();
                    handTrackingTagInfo = CreateHandTrackingInfo(YVRManager.instance.handTrackingSupport ==
                                                                 YVRManager.HandTrackingSupport.HandsOnly);
                }

                asset.manifestTagInfosList = new List<ManifestTagInfo>() { permissionTagInfo, handTrackingTagInfo };
                ScriptableObjectUtility.CreateAsset(asset, k_SaveDataPath);
            }
        }

        private ManifestTagInfo CreateHandTrackingPermission()
        {
            return new ManifestTagInfo()
            {
                nodePath = "/manifest",
                tag = "uses-permission",
                attrName = "name",
                attrValue = "com.yvr.permission.HAND_TRACKING",
                modifyIfFound = true
            };
        }

        private ManifestTagInfo CreateHandTrackingInfo(bool required)
        {
            Debug.Log(required.ToString().ToLower());
            return new ManifestTagInfo()
            {
                nodePath = "/manifest",
                tag = "uses-feature",
                attrName = "name",
                attrValue = "yvr.software.handtracking",
                attrs = new[] { "required", required.ToString().ToLower() },
                modifyIfFound = true
            };
        }
    }
}