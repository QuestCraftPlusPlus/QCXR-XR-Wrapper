using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JNIStorage : MonoBehaviour
{
    public static AndroidJavaClass apiClass;
    public static AndroidJavaObject accountObj;
    public static AndroidJavaObject activity;
    public static AndroidJavaObject instancesObj;
    public static JNIStorage instance;
    public List<string> supportedVersions;
    public UIHandler uiHandler;
    public TMP_InputField RAMSetterField;
    public TMP_Dropdown instancesDropdown;
    [SerializeField, FormerlySerializedAs("DevToggle")]
    private Toggle _devToggle;

    private void Start()
    {
        instance = this;

        // If the user has not granted Microphone permission, don't try to start the clip.
        if (Microphone.devices.Length != 0)
        {
            Microphone.Start(Microphone.devices[0], true, 1, 44100);
        }

        apiClass = new AndroidJavaClass("pojlib.API");
        instancesObj = apiClass.CallStatic<AndroidJavaObject>("loadAll");
        apiClass.SetStatic("developerMods", _devToggle.isOn);
        UpdateInstances();
		apiClass.SetStatic("model", OpenXRFeatureSystemInfo.GetHeadsetName());
    }

    private static void FillSupportedVersions(List<string> supportedVersions, string[] supportedVersionsArray)
    {
        supportedVersions.Clear();
        supportedVersions.AddRange(supportedVersionsArray);
        AndroidJavaObject[] instances = instancesObj.Call<AndroidJavaObject[]>("toArray");
        foreach (var instance in instances)
        {
            string name = instance.Get<string>("instanceName");
            if (!supportedVersions.Contains(name))
            {
                supportedVersions.Add(name);
            }
        }
    }

    public static PojlibInstance GetInstance(string name)
    {
        AndroidJavaObject[] instances = instancesObj.Call<AndroidJavaObject[]>("toArray");
        foreach (var instance in instances)
        {
            PojlibInstance pojlibInstance = PojlibInstance.Parse(instance);
            if (pojlibInstance.instanceName.Equals(name))
            {
                return pojlibInstance;
            }
        }

        return null;
    }

    public void UpdateInstances()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        
        uiHandler.ClearDropdowns();
        string[] supportedVersionsArray = apiClass.CallStatic<string[]>("getQCSupportedVersions");
        FillSupportedVersions(supportedVersions, supportedVersionsArray);
        uiHandler.UpdateDropdowns(true, supportedVersions);
    }

    public void SetMemoryValue()
    {
        if (RAMSetterField.text != null)
        {
            apiClass.SetStatic("memoryValue", RAMSetterField.text);
        }
    }
}
