using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class BuildInfo
{
    [MenuItem("Build/Get Build Info")]
    public static void GetBuildInfo()
    {
        string dateTime = DateTime.Now.ToString("yyyy_MMdd_HHmm");
        string versionName = Application.version;
        string versionCode = PlayerSettings.Android.bundleVersionCode.ToString();
        string identifier = Application.identifier;
        string info = $"{Application.productName}-{identifier}-{versionName}-{versionCode}-{dateTime}.apk";

        GUIUtility.systemCopyBuffer = info;
        Debug.Log(info);
    }
}
