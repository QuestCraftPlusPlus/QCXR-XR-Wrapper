using System.Collections.Generic;
using AndroidManifest;
using UnityEngine;

public class YVRSDKSettingAsset : ScriptableObject
{
    public bool ignoreSDKSetting = false;
    public bool appIDChecked = false;
    public List<ManifestTagInfo> manifestTagInfosList;
}